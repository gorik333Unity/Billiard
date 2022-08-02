using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperTest : MonoBehaviour
{
    [SerializeField]
    private LaunchBall _launchBall;

    [SerializeField]
    private TakeBall _takeBall;

    [SerializeField]
    private CueStick _cueStick;

    [SerializeField]
    private int _index;

    [SerializeField]
    private bool _showNormal;

    [SerializeField]
    private Transform _visual;

    private Vector3 _normal;
    private float _distance;
    private bool _stopUpdateDraw;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _index++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _index--;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _stopUpdateDraw = true;
        }

        if (_showNormal)
        {
            var ob = new GameObject();
            ob.transform.position = _normal;
            _showNormal = false;
        }

        //BallToBallRay();
        if (!_stopUpdateDraw)
        {
            _distance = _takeBall.Position.z - _cueStick.Position.z;

        }
        ReloadGame();
        CueDrawRay();
    }

    private void ReloadGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CueDrawRay()
    {
        var cueT = _cueStick.transform;
        //cueT.LookAt(_launchBall.transform);

        var ray = new Ray(cueT.position, cueT.forward);
        var hits = Physics.SphereCastAll(cueT.position, 0.025f, cueT.forward);

        if (Physics.SphereCast(cueT.position, 0.025f, cueT.forward, out RaycastHit hit, 100f/*, LayerMask.NameToLayer("Default")*/))
        {
            //Debug.Log(hit.collider.name);
            //var from = hit.collider.col - _cueStick.Position;
            //var to = _cueStick.transform.up;

            var sphereCenter = _cueStick.Position + _cueStick.transform.forward * _distance;
            var ballCenter = _takeBall.Position;
            var result = (sphereCenter + ballCenter) / 2;

            _normal = hit.normal;

            //var testPosition = 
            //var offset = result - sphereCenter;

            var pos = SphereOrCapsuleCastCenterOnCollision(cueT.position, cueT.forward, hit.distance);
            _visual.position = pos;

            var cueDirection = (result - cueT.position).normalized;
            var direction = (ballCenter - hit.point).normalized;
            var reflect = Vector3.Reflect(cueDirection, hit.normal);
            //Debug.DrawRay(sphereCenter, direction, Color.white);

            var perpendecular = Vector3.Reflect(direction, Vector3.Reflect(reflect, Vector3.Cross(reflect, Vector3.up)));

            Debug.DrawRay(_visual.position, perpendecular, Color.red);
            Debug.DrawRay(_visual.position, reflect, Color.magenta);
            Debug.DrawRay(_visual.position, direction, Color.blue);
            Debug.DrawLine(cueT.position, pos, Color.white);

            //Debug.Log(Vector3.Angle(from, to));
        }
    }

    private Vector3 SphereOrCapsuleCastCenterOnCollision(Vector3 origin, Vector3 directionCast, float hitInfoDistance)
    {
        return origin + (directionCast.normalized * hitInfoDistance);
    }

    private void BallToBallRay()
    {
        var takeBallPos = _takeBall.Position;
        var launchBallPos = _launchBall.Position;
        //takeBallPos.y = launchBallPos.y;

        var direction = (takeBallPos - launchBallPos).normalized;

        Debug.DrawRay(_launchBall.Position, direction, Color.red);
    }
}
