using EnviromentModule;
using ShapeBuilder;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;
using Cinemachine;

namespace GameModule
{
    [System.Serializable]
    public class ClassicMode
    {
        private readonly string _containerName = "ClassicModeContainer";
        private readonly int _pyramidBallCount = 15;

        [SerializeField]
        private Cue _cuePrefab;

        [SerializeField]
        private Ball _ballPrefab;

        [SerializeField]
        private Board _boardPrefab;

        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField]
        private CoroutineRunner _coroutineRunner;

        [SerializeField]
        private ClassicPlayerControllerCanvas _classicPlayerCanvasPrefab;

        [SerializeReference, SubclassSelector]
        private ClassicPlayerController _classicPlayerController;

        private IShapeBuilder _shapeBuilder = new PyramidShapeBuilder();
        private IPlayerController _playerController;

        private Cue _cue;
        private Board _board;
        private Ball _mainBall;
        private Ball[] _otherBall;
        private Transform _container;

        private event Action _onWin;
        private event Action _onLose;

        public void Initialize()
        {
            InitializeContainer();
            var board = SpawnBoard();
            var cue = SpawnCue();
            InitializeBoard(board);
            InitializeCue(cue, board);
            InitializePlayerController(board, cue);

            _board = board;
            _cue = cue;
        }

        public void ActivateMode()
        {
            _playerController.Activate();
        }

        public void Reset()
        {
            Object.Destroy(_cue.gameObject);
            Object.Destroy(_mainBall.gameObject);
            Object.Destroy(_container.gameObject);
            Object.Destroy(_board.gameObject);

            foreach (var item in _otherBall)
            {
                Object.Destroy(item.gameObject);
            }

            _cue = null;
            _mainBall = null;
            _container = null;
            _board = null;
            _otherBall = null;
            _onWin = null;
            _onLose = null;
        }

        public void OnWin(Action action)
        {
            _onWin += action;
        }

        public void OnLose(Action action)
        {
            _onLose += action;
        }

        private void InitializeContainer()
        {
            _container = new GameObject().transform;
            _container.name = _containerName;
        }

        private void InitializePlayerController(Board board, Cue cue)
        {
            var controller = _classicPlayerController;
            controller.Inject(_classicPlayerCanvasPrefab);
            controller.Initialize(_virtualCamera, board, cue, _mainBall, _coroutineRunner);
            _playerController = controller;
        }

        private Board SpawnBoard()
        {
            return Object.Instantiate(_boardPrefab, _container);
        }

        private Cue SpawnCue()
        {
            var cue = Object.Instantiate(_cuePrefab, _container);

            return cue;
        }

        private void InitializeCue(Cue cue, Board board)
        {
            cue.SetFollowedBall(_mainBall);
            cue.transform.position = board.CuePlacePosition;
        }

        private void InitializeBoard(Board board)
        {
            var otherSpawnPosition = board.PyramidBallPlacePosition;
            var mainSpawnPosition = board.MainBallPlacePosition;
            var ballsForPyramid = SpawnBallsForPyramid(_ballPrefab);
            var mainBall = SpawnMainBall(mainSpawnPosition, _ballPrefab);
            _shapeBuilder.Build(otherSpawnPosition, ballsForPyramid);
            board.SetBoardBalls(ballsForPyramid, mainBall);

            _mainBall = mainBall;
            _otherBall = ballsForPyramid;
        }

        private Ball[] SpawnBallsForPyramid(Ball ballPrefab)
        {
            var balls = new List<Ball>();

            for (int i = 0; i < _pyramidBallCount; i++)
            {
                var ball = Object.Instantiate(ballPrefab, _container);
                balls.Add(ball);
            }

            return balls.ToArray();
        }

        private Ball SpawnMainBall(Vector3 spawnPosition, Ball ballPrefab)
        {
            var ball = Object.Instantiate(ballPrefab, _container);
            ball.transform.position = spawnPosition;

            return ball;
        }
    }
}
