using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace EnviromentModule
{
    public class Board : MonoBehaviour
    {
        private readonly float _minVelocity = 0.1f;

        [SerializeField]
        private Transform _pyramidBallPlacePoint;

        [SerializeField]
        private Transform _mainBallPlacePoint;

        [SerializeField]
        private Transform _cuePlacePoint;

        [SerializeField]
        private BoardPocket _boardPocket;

        private readonly List<Ball> _boardBall = new List<Ball>();

        public Vector3 PyramidBallPlacePosition => _pyramidBallPlacePoint.position;
        public Vector3 MainBallPlacePosition => _mainBallPlacePoint.position;
        public Vector3 CuePlacePosition => _cuePlacePoint.position;

        public void SetBoardBalls(Ball[] balls)
        {
            _boardBall.Clear();
            _boardBall.AddRange(balls);
        }

        public bool AreAnyBallMoving()
        {
            foreach (var item in _boardBall)
            {
                if (item.Rigidbody.velocity.magnitude > _minVelocity)
                {
                    return true;
                }
            }

            return false;
        }

        private void Start()
        {
            InitializeBoardPocket();
        }

        private void InitializeBoardPocket()
        {
            _boardPocket.OnBallDestroyed(BoardPocket_OnBallDestroyed);
        }

        private void BoardPocket_OnBallDestroyed(Ball ball)
        {
            if (_boardBall.Contains(ball))
            {
                _boardBall.Remove(ball);
            }
        }
    }
}
