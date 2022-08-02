using System;

namespace UI_Module.EndScreenModule
{
    [System.Serializable]
    public class ClassicWinEndScreen : IWinEndScreen
    {
        private event Action _onNextLevel;
        private event Action _onExitedToMainMenu;
        private event Action _onOpened;
        private event Action _onClosed;

        public void Initialize()
        {
        }

        public void Activate()
        {
        }

        public void Deactivate()
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

        public void OnNextLevel(Action action)
        {
            _onNextLevel += action;
        }

        public void OnExitToMainMenu(Action action)
        {
            _onExitedToMainMenu += action;
        }
    }
}
