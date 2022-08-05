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

                    //Debug.Log(hit.collider.name);
                    //var from = hit.collider.col - _cueStick.Position;
                    //var to = _cueStick.transform.up;

                    var sphereCenter = _cue.Position + _cue.transform.forward * 3f;
                    var ballCenter = ball.Position;
                    var result = (sphereCenter + ballCenter) / 2;

                    //var testPosition = 
                    //var offset = result - sphereCenter;

                    var pos = SphereCastCenterOnCollision(cueT.position, cueT.forward, hit.distance);
                    _visual.transform.position = pos;

                    var cueDirection = (result - cueT.position).normalized;
                    var direction = (ballCenter - hit.point).normalized;
                    var reflect = Vector3.Reflect(cueDirection, hit.normal);
                    //Debug.DrawRay(sphereCenter, direction, Color.white);

                    var perpendecular = Vector3.Reflect(direction, Vector3.Reflect(reflect, Vector3.Cross(reflect, Vector3.up)));

                    Debug.DrawRay(_visual.transform.position, perpendecular, Color.red);
                    Debug.DrawRay(_visual.transform.position, reflect, Color.magenta);
                    Debug.DrawRay(_visual.transform.position, direction, Color.blue);
                    Debug.DrawLine(cueT.position, pos, Color.white);

                    //Debug.Log(Vector3.Angle(from, to));
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
