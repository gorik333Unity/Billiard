using System.Collections;
using Tools;
using UnityEngine;

namespace GameModule
{
    [System.Serializable]
    public class ClassicBallTrajectoryShower : IBallTrajectoryShower
    {
        private readonly string _containerName = "ClassicBallTrajectoryShowerContainer";

        [SerializeField]
        private CoroutineRunner _coroutineRunner;

        [SerializeField]
        private Visual _visualPrefab;

        private Cue _cue;
        private Transform _container;
        private Visual _visual;
        private Coroutine _showTrajectoryC;

        public void Activate()
        {
            StopShowTrajectory();
            _showTrajectoryC = _coroutineRunner.StartCoroutine(ShowTrajectory());
        }

        public void Deactivate()
        {
            StopShowTrajectory();
        }

        public void Initialize(Cue cue)
        {
            InitializeContainer();
            InitializeVisual();
            _cue = cue;
        }

        public void Initialize()
        {
        }

        private void InitializeContainer()
        {
            _container = new GameObject().transform;
            _container.name = _containerName;
        }

        private void InitializeVisual()
        {
            var visual = Object.Instantiate(_visualPrefab, _container);
            visual.Initialize();
            visual.gameObject.SetActive(false);
            _visual = visual;
        }

        private void StopShowTrajectory()
        {
            if (_showTrajectoryC != null)
            {
                _coroutineRunner.StopCoroutine(_showTrajectoryC);
                _showTrajectoryC = null;
            }
        }

        private IEnumerator ShowTrajectory()
        {
            while (true)
            {
                var cueT = _cue.transform;
                if (Physics.SphereCast(cueT.position, 0.025f, cueT.forward, out RaycastHit hit, 100f/*, LayerMask.NameToLayer("Default")*/))
                {
                    var ball = hit.collider.GetComponentInParent<Ball>();

                    if (ball == null)
                    {
                        yield return null;
                        continue;
                    }
                    else
                    {
                        _visual.gameObject.SetActive(true);
                    }


                    var pos = SphereCastCenterOnCollision(cueT.position, cueT.forward, hit.distance);
                    pos.y = ball.Position.y;
                    _visual.transform.position = pos;

                    var sphereCenter = ball.Position;
                    var ballCenter = ball.Position;
                    var result = (sphereCenter + ballCenter) / 2;
                    var hitPoint = hit.point;
                    hitPoint.y = ball.Position.y;

                    var cueDirection = (result - cueT.position).normalized;
                    var direction = (ballCenter - hitPoint).normalized;
                    var reflect = Vector3.Reflect(cueDirection, hit.normal);
                    var perpendecular = Vector3.Reflect(direction, Vector3.Reflect(reflect, Vector3.Cross(reflect, Vector3.up)));

                    Debug.DrawRay(_visual.transform.position, perpendecular, Color.red);
                    Debug.DrawRay(_visual.transform.position, reflect, Color.magenta);
                    Debug.DrawRay(_visual.transform.position, direction, Color.blue);
                    Debug.DrawLine(cueT.position, pos, Color.white);
                }
                else
                {
                    _visual.gameObject.SetActive(false);
                }

                yield return null;
            }
        }

        private Vector3 SphereCastCenterOnCollision(Vector3 origin, Vector3 directionCast, float hitInfoDistance)
        {
            return origin + (directionCast.normalized * hitInfoDistance);
        }
    }
}
