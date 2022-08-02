using System;
using Tools;
using UnityEngine;

namespace EnviromentModule
{
    public class BoardPocket : MonoBehaviour
    {
        private event Action<Ball> _onBallDestroyed;

        public void OnBallDestroyed(Action<Ball> action)
        {
            _onBallDestroyed = action;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                _onBallDestroyed?.Invoke(ball);
                Destroy(ball.gameObject);
            }
        }
    }
}
