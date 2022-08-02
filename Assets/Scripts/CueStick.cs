using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class CueStick : MonoBehaviour
{
    [SerializeField]
    private Ball _mainBall;

    [SerializeField]
    private Transform _cueCap;

    [SerializeField]
    private float _sensivity;

    [SerializeField]
    private float _smoothEndTime;

    private Vector2 _previousTouchPosition;
    private Vector2 _startTouchPosition;

    private Coroutine _smoothContinueC;

    public Vector3 Position => transform.position;
    public Transform FollowPoint => _cueCap;

    private void Update()
    {
        HorizontalChoose();
        //HitPointChoose();
        HitBall();
    }

    private void HitBall()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _mainBall.GetComponent<Rigidbody>().AddForce((_mainBall.transform.position - _cueCap.position).normalized * 1, ForceMode.Impulse);
        }
    }

    private void HitPointChoose()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var ball = hit.collider.GetComponent<Ball>();

                if (ball == _mainBall)
                {
                    transform.LookAt(hit.point);
                }
            }
        }
    }

    private void HorizontalChoose()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPosition = touch.position;
                _previousTouchPosition = touch.position;
            }

            var delta = touch.position - _previousTouchPosition;
            transform.RotateAround(_mainBall.transform.position, Vector3.up, -delta.x * Time.deltaTime * _sensivity);

            _previousTouchPosition = touch.position;
        }
    }
}
