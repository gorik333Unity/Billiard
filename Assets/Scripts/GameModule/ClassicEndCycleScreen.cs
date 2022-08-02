using System;
using UI_Module.EndScreenModule;
using UnityEngine;

namespace GameModule
{
    [System.Serializable]
    public class ClassicEndCycleScreen
    {
        [SerializeReference, SubclassSelector]
        private IWinEndScreen _winEndScreen;

        [SerializeReference, SubclassSelector]
        private ILoseEndScreen _loseEndScreen;

        private event Action _onMainMenuClicked;
        private event Action _onRestartClicked;
        private event Action _onNextLevelClicked;

        public void Initialize()
        {
            InitializeWinEndScreen();
            InitializeLoseEndScreen();
        }

        public void EndCycle(bool isWin)
        {
            if (isWin)
            {
                _winEndScreen.Activate();
            }
            else
            {
                _loseEndScreen.Activate();
            }
        }

        public void OnNextLevelClicked(Action action)
        {
            _onNextLevelClicked += action;
        }

        public void OnRestartClicked(Action action)
        {
            _onRestartClicked = action;
        }

        public void OnMainMenuClicked(Action action)
        {
            _onMainMenuClicked = action;
        }

        private void DeactivateScreens()
        {
            _winEndScreen.Deactivate();
            _loseEndScreen.Deactivate();
        }

        private void InitializeWinEndScreen()
        {
            _winEndScreen.Initialize();
            _winEndScreen.OnExitToMainMenu(EndScreen_OnMainMenuClicked);
            _winEndScreen.OnNextLevel(WinEndScreen_OnNextLevelClicked);
        }

        private void InitializeLoseEndScreen()
        {
            _loseEndScreen.Initialize();
            _loseEndScreen.OnExitToMainMenu(EndScreen_OnMainMenuClicked);
            _loseEndScreen.OnRestarted(LoseEndScreen_OnRestartClicked);
        }

        private void WinEndScreen_OnNextLevelClicked()
        {
            _onNextLevelClicked?.Invoke();
            DeactivateScreens();
        }

        private void LoseEndScreen_OnRestartClicked()
        {
            _onRestartClicked?.Invoke();
            DeactivateScreens();
        }

        private void EndScreen_OnMainMenuClicked()
        {
            _onMainMenuClicked?.Invoke();
            DeactivateScreens();
        }
    }
}
