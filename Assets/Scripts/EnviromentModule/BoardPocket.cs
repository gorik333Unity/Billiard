using System;
using Tools;
using UnityEngine;

namespace EnviromentModule
{
    public class BoardPocket : MonoBehaviour
    {
        private event Action<Ball> _onBallFallen;

        public void OnBallFallen(Action<Ball> action)
        {
            _onBallFallen = action;
        }

        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponentInParent<Ball>();

            if (ball != null)
            {
                _onBallFallen?.Invoke(ball);
            }
        }
    }
}
