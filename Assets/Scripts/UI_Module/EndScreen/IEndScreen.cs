using System;

namespace UI_Module.EndScreenModule
{
    public interface IEndScreen
    {
        void Initialize();
        void Activate();
        void Deactivate();
        void OnOpened(Action action);
        void OnClosed(Action action);
    }
}
