using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace EnviromentModule
{
    public class Board : MonoBehaviour
    {
        private readonly float _minVelocity = 0.004f;

        [SerializeField]
        private Transform _pyramidBallPlacePoint;

        [SerializeField]
        private Transform _mainBallPlacePoint;

        [SerializeField]
        private Transform _cuePlacePoint;

        [SerializeField]
        private BoardPocket _boardPocket;

        private Ball _mainBall;
        private readonly List<Ball> _boardBall = new List<Ball>();

        public Vector3 PyramidBallPlacePosition => _pyramidBallPlacePoint.position;
        public Vector3 MainBallPlacePosition => _mainBallPlacePoint.position;
        public Vector3 CuePlacePosition => _cuePlacePoint.position;

        public void SetBoardBalls(Ball[] balls, Ball mainBall)
        {
            _mainBall = mainBall;
            _boardBall.Clear();
            _boardBall.AddRange(balls);
        }

        public bool AreAnyBallMoving()
        {
            bool result = false;

            foreach (var item in _boardBall)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.Rigidbody.velocity.magnitude > _minVelocity)
                {
                    return true;
                }
            }

            if (_mainBall.Rigidbody.velocity.magnitude > _minVelocity)
            {
                result = true;
            }

            return result;
        }

        private void Start()
        {
            InitializeBoardPocket();
        }

        private void InitializeBoardPocket()
        {
            _boardPocket.OnBallFallen(BoardPocket_OnBallFallen);
        }

        private void BoardPocket_OnBallFallen(Ball ball)
        {
            if (ball == _mainBall)
            {
                OnMainBallFallen(ball);
                return;
            }

            Destroy(ball.gameObject);

            if (_boardBall.Contains(ball))
            {
                _boardBall.Remove(ball);
            }
        }

        private void OnMainBallFallen(Ball ball)
        {
            ball.transform.position = _mainBallPlacePoint.position;
            ball.Rigidbody.velocity = Vector3.zero;
            ball.Rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
