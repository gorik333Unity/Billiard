using System;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public class Cue : MonoBehaviour
    {
        [SerializeField]
        private Transform _followPoint;

        [SerializeField]
        private Transform _model;

        [SerializeField]
        private Transform _cap;

        private Coroutine _hitC;
        private Coroutine _detectHitC;
        private Ball _mainBall;
        private event Action _onHit;
        private event Action _onStartedHit;

        public Transform Model => _model;
        public Transform Cap => _cap;
        public Transform FollowPoint => _followPoint;
        public Ball MainBall => _mainBall;
        public Vector3 Position => transform.position;

        public void SetFollowedBall(Ball ball)
        {
            _mainBall = ball;
        }

        public void OnHit(Action action)
        {
            _onHit += action;
        }

        public void OnStartedHit(Action action)
        {
            _onStartedHit += action;
        }

        public void Activate()
        {
            _detectHitC = StartCoroutine(DetectHit());
        }

        public void Deactivate()
        {
            StopDetectHit();
        }

        private void StopDetectHit()
        {
            if (_detectHitC != null)
            {
                StopCoroutine(_detectHitC);
                _detectHitC = null;
            }
        }

        private IEnumerator DetectHit()
        {
            while (true)
            {


                yield return null;
            }
        }

        private IEnumerator Hit()
        {
            while (true)
            {


                yield return null;
            }
        }
    }
}
