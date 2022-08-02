using Tools;

namespace GameModule
{
    public interface IBallLauncher
    {
        void Initialize(Cue cue, Ball ball);
        void Activate(float normalizedStrengthMultiplier);
        void Deactivate();
    }
}
