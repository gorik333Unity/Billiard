using System;
using UnityEngine;

namespace GameModule
{
    [System.Serializable]
    public class ClassicGameCycle : IGameCycle
    {
        [SerializeReference, SubclassSelector]
        private ClassicMode _classicMode;

        [SerializeReference, SubclassSelector]
        private ClassicEndCycleScreen _endCycleScreen;

        private event Action _onCycleStarted;
        private event Action _onCycleEnded;

        public void Initialize()
        {
            InitializeClassicMode();
            InitializeClassicEndScreen();
        }

        public void OnCycleStarted(Action action)
        {
            _onCycleStarted += action;
        }

        public void OnCycleEnded(Action action)
        {
            _onCycleEnded += action;
        }

        public void StartCycle()
        {
            _onCycleStarted?.Invoke();
            _classicMode.ActivateMode();
        }

        public void StopCycle()
        {
            _onCycleEnded?.Invoke();
        }

        private void RestartCycle()
        {
            _classicMode.Reset();
            _classicMode.Initialize();
            _classicMode.ActivateMode();
        }

        private void InitializeClassicEndScreen()
        {
            _endCycleScreen.Initialize();
            _endCycleScreen.OnRestartClicked(EndCycleScreen_OnRestartClicked);
            _endCycleScreen.OnNextLevelClicked(EndCycleScreen_OnNextLevelClicked);
            _endCycleScreen.OnMainMenuClicked(EndCycleScreen_OnMainMenuClicked);
        }

        private void InitializeClassicMode()
        {
            _classicMode.Initialize();
            _classicMode.OnWin(ClassicMode_OnWin);
            _classicMode.OnLose(ClassicMode_OnLose);
        }

        private void ClassicMode_OnWin()
        {
            _endCycleScreen.EndCycle(true);
        }

        private void ClassicMode_OnLose()
        {
            _endCycleScreen.EndCycle(false);
        }

        private void EndCycleScreen_OnRestartClicked()
        {
            RestartCycle();
        }

        private void EndCycleScreen_OnNextLevelClicked()
        {

        }

        private void EndCycleScreen_OnMainMenuClicked()
        {

        }
    }
}
