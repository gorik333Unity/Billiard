using System;

namespace UI_Module
{
    public interface IMainMenuUI
    {
        void OnGameStarted(Action action);
        void Initialize();
        void Activate();
        void Deactivate();
    }
}
