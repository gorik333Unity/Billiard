using UnityEngine;
using UI_Module;

namespace GameModule
{
    [System.Serializable]
    public class MainGameInitializer : IGameInitializer
    {
        [SerializeReference, SubclassSelector]
        private IMainMenuUI _mainMenuUI;

        [SerializeReference, SubclassSelector]
        private IGameCycle _gameCycle;

        public void Initialize()
        {
            InitializeMainMenu();
            InitializeGameCycle();
        }

        private void InitializeMainMenu()
        {
            _mainMenuUI.Initialize();
            _mainMenuUI.OnGameStarted(MainMenu_OnGameStarted);
        }

        private void InitializeGameCycle()
        {
            _gameCycle.Initialize();
            _gameCycle.OnCycleEnded(GameCycle_OnCycleEnded);
        }

        private void MainMenu_OnGameStarted()
        {
            _mainMenuUI.Deactivate();
            _gameCycle.StartCycle();
        }

        private void GameCycle_OnCycleEnded()
        {
            _mainMenuUI.Activate();
        }
    }
}
