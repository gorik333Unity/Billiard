using System;

namespace GameModule
{
    public interface IGameCycle
    {
        void Initialize();
        void StartCycle();
        void StopCycle();
        void OnCycleStarted(Action action);
        void OnCycleEnded(Action action);
    }
}
