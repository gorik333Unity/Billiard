using System;

namespace UI_Module.EndScreenModule
{
    [System.Serializable]
    public class ClassicLoseEndScreen : ILoseEndScreen
    {
        private event Action _onRestarted;
        private event Action _onExitedToMainMenu;
        private event Action _onOpened;
        private event Action _onClosed;

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void Initialize()
        {
        }

        public void OnOpened(Action action)
        {
            _onOpened += action;
        }

        public void OnClosed(Action action)
        {
            _onClosed += action;
        }

        public void OnRestarted(Action action)
        {
            _onRestarted += action;
        }

        public void OnExitToMainMenu(Action action)
        {
            _onExitedToMainMenu += action;
        }
    }
}
