using EnviromentModule;
using Tools;
using UnityEngine;
using Cinemachine;
using System.Collections;
using Base.Interfaces;
using DG.Tweening;
using Object = UnityEngine.Object;

namespace GameModule
{
    public class ClassicPlayerController : IPlayerController, IInjectionReceiver<ClassicPlayerControllerCanvas>
    {
        private readonly string _containerName = "ClassicPlayerControllerContainer";
        private readonly Vector3 _cuePosition = new Vector3(0f, 0.21f, 0f);
        private readonly Vector3 _boardPosition = new Vector3(0f, 1.5f, 0.54f);
        private readonly float _switchSpeed = 4f;
        private readonly float _checkInterval = 0.3f;
        private readonly float _moveTimeCueToBall = 0.75f;

        private ClassicPlayerControllerCanvas _controllerCanvasPrefab;
        private ClassicPlayerControllerCanvas _controllerCanvas;
        private CinemachineVirtualCamera _virtualCamera;
        private Board _board;
        private Cue _cue;
        private Ball _mainBall;
        private Transform _container;
        private float _sliderValue;
        private IBallLauncher _ballLauncher;
        private IBallTrajectoryShower _ballTrajectoryShower;
        private ICueRotator _cueRotator;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _fromCueToBoardC;
        private Coroutine _fromBoardToCueC;
        private Coroutine _checkBallsMovingC;
        private Coroutine _moveCueToMainBall;

        public void Initialize(CinemachineVirtualCamera virtualCamera, Board board, Cue cue, Ball mainBall, CoroutineRunner coroutineRunner)
        {
            _virtualCamera = virtualCamera;
            _board = board;
            _cue = cue;
            _mainBall = mainBall;
            _coroutineRunner = coroutineRunner;

            _virtualCamera.Follow = _cue.FollowPoint;
            _virtualCamera.LookAt = _cue.FollowPoint;

            InitializeContainer();
            InitializeCue();
            InitializeClassicPlayerControllerCanvas();
            InitializeBallLauncher();
        }

        public void Initialize()
        {
        }

        public void Inject(ClassicPlayerControllerCanvas classicPlayerControllerCanvas)
        {
            _controllerCanvasPrefab = classicPlayerControllerCanvas;
        }

        public void Activate()
        {
            MoveCueToMainBall(DefaulSettings);
            _cue.SetFollowedBall(_mainBall);
        }

        private void InitializeBallLauncher()
        {
            var ballLaucher = new ClassicBallLaucher();
            ballLaucher.Initialize(_cue, _mainBall);
            _ballLauncher = ballLaucher;
        }

        private void DefaulSettings()
        {
            //_fromBoardToCueC = _coroutineRunner.StartCoroutine(FromBoardToCue());
            ClassicPlayerControllerCanvas_OnHorizontalChooseClicked();
            _controllerCanvas.Content.gameObject.SetActive(true);
        }

        private void InitializeContainer()
        {
            _container = new GameObject().transform;
            _container.name = _containerName;
        }

        private void InitializeCue()
        {

        }

        private void InitializeBallTrajectoryShower()
        {
            var trajectoryShower = new ClassicBallTrajectoryShower();
        }

        private void InitializeClassicPlayerControllerCanvas()
        {
            _controllerCanvas = Object.Instantiate(_controllerCanvasPrefab, _container);
            _controllerCanvas.OnHitPointChooseClicked(ClassicPlayerControllerCanvas_OnHitPointChooseClicked);
            _controllerCanvas.OnHorizontalChooseClicked(ClassicPlayerControllerCanvas_OnHorizontalChooseClicked);
            _controllerCanvas.OnLaunchButtonClicked(ControllerCanvas_OnLaunchBall);
            _controllerCanvas.OnSliderValueChanged(ClassicControllerCanvas_OnSliderValueChanged);
            _controllerCanvas.Content.gameObject.SetActive(false);
        }

        private void Cue_OnStartedHit()
        {

        }

        private void ClassicControllerCanvas_OnSliderValueChanged(float value)
        {
            _sliderValue = value;
        }

        private void ControllerCanvas_OnLaunchBall()
        {
            _ballLauncher.Activate(_sliderValue);
            _controllerCanvas.Content.gameObject.SetActive(false);
            //_fromCueToBoardC = _coroutineRunner.StartCoroutine(FromCueToBoard());
            _checkBallsMovingC = _coroutineRunner.StartCoroutine(CheckBallsMoving());
        }

        private void ClassicPlayerControllerCanvas_OnHitPointChooseClicked()
        {
            StopCueRotator();

            var rotator = new HitPointCueRotator();
            rotator.Initialize(_cue, _coroutineRunner);
            rotator.Activate();
            _cueRotator = rotator;
        }

        private void ClassicPlayerControllerCanvas_OnHorizontalChooseClicked()
        {
            StopCueRotator();

            var rotator = new HorizontalCueRotator();
            rotator.Initialize(_cue, _coroutineRunner);
            rotator.Activate();
            _cueRotator = rotator;
        }

        private void StopCueRotator()
        {
            if (_cueRotator != null)
            {
                _cueRotator.Deactivate();
                _cueRotator = null;
            }
        }

        private void MoveCueToMainBall(TweenCallback onComplete)
        {
            Transform follower = _cue.transform;
            Vector3 target = _mainBall.Position;

            follower.DOMove(target, _moveTimeCueToBall).OnComplete(onComplete);
        }

        private void OnEndMovingBalls()
        {
            MoveCueToMainBall(DefaulSettings);
            //_fromBoardToCueC = _coroutineRunner.StartCoroutine(FromBoardToCue());
        }

        private IEnumerator CheckBallsMoving()
        {
            while (true)
            {
                if (!_board.AreAnyBallMoving())
                {
                    OnEndMovingBalls();
                    _checkBallsMovingC = null;
                    yield break;
                }

                yield return new WaitForSeconds(_checkInterval);
            }
        }

        private IEnumerator FromCueToBoard()
        {
            var transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _virtualCamera.LookAt = _board.transform;
            float time = 0f;

            while (true)
            {
                time += Time.deltaTime * _switchSpeed;
                var lerp = Vector3.Lerp(_cuePosition, _boardPosition, time);
                transposer.m_FollowOffset = lerp;

                if (time > 1f)
                {
                    _fromCueToBoardC = null;
                    yield break;
                }

                yield return null;
            }
        }

        private IEnumerator FromBoardToCue()
        {
            var transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            float time = 0f;

            while (true)
            {
                time += Time.deltaTime * _switchSpeed;
                var lerp = Vector3.Lerp(_boardPosition, _cuePosition, time);
                transposer.m_FollowOffset = lerp;

                if (time > 1f)
                {
                    _fromBoardToCueC = null;
                    _virtualCamera.LookAt = _cue.FollowPoint;
                    yield break;
                }

                yield return null;
            }
        }
    }
}
