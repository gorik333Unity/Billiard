using Tools;
using UnityEngine;

namespace GameModule
{
    public class ClassicBallLaucher : IBallLauncher
    {
        private readonly float _strength = 1f;

        private Cue _cue;
        private Ball _ball;

        public void Activate(float normalizedStrengthMultiplier)
        {
            if (normalizedStrengthMultiplier > 1f)
            {
                normalizedStrengthMultiplier = 1f;
            }
            if (normalizedStrengthMultiplier < 0f)
            {
                normalizedStrengthMultiplier = 0f;
            }

            var strength = _strength * normalizedStrengthMultiplier;
            Launch(strength);
        }

        public void Deactivate()
        {
        }

        public void Initialize(Cue cue, Ball ball)
        {
            _cue = cue;
            _ball = ball;
        }

        private void Launch(float strength)
        {
            var direction = DefineDirection();
            _ball.Rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }

        private Vector3 DefineDirection()
        {
            Vector3 direction = Vector3.zero;
            direction = (_ball.Position - _cue.Cap.position).normalized;

            return direction;
        }
    }
}
