using System.Collections;
using Tools;
using UnityEngine;

namespace GameModule
{
    public class HitPointCueRotator : ICueRotator
    {
        private readonly float _sensivity = 6f;

        private Cue _cue;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _hitPointRotateC;

        public void Activate()
        {
            _hitPointRotateC = _coroutineRunner.StartCoroutine(HitPointRotate());
        }

        public void Deactivate()
        {
            StopHitPointRotate();
        }

        public void Initialize()
        {
        }

        public void Initialize(Cue cue, CoroutineRunner coroutineRunner)
        {
            _cue = cue;
            _coroutineRunner = coroutineRunner;
        }

        private void StopHitPointRotate()
        {
            if (_hitPointRotateC != null)
            {
                _coroutineRunner.StopCoroutine(_hitPointRotateC);
                _hitPointRotateC = null;
            }
        }

        private IEnumerator HitPointRotate()
        {
            var ball = _cue.MainBall;

            while (true)
            {
                yield return null;
            }
        }
    }
}
