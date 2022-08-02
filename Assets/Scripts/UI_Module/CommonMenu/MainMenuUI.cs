using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI_Module.CommonMenu
{
    [System.Serializable]
    public class MainMenuUI : IMainMenuUI
    {
        private readonly string _containerName = "MainMenuUI_Container";

        [SerializeField]
        private MainMenuCanvas _mainMenuCanvasPrefab;

        private MainMenuCanvas _mainMenuCanvas;
        private Transform _container;
        private event Action _onGameStarted;

        public void Initialize()
        {
            InitializeContainer();
            InitializeCanvas();
            InitializePlayButton();
        }

        public void Activate()
        {
            _mainMenuCanvas.Content.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _mainMenuCanvas.Content.gameObject.SetActive(false);
        }

        public void OnGameStarted(Action action)
        {
            _onGameStarted += action;
        }

        private void InitializeContainer()
        {
            _container = new GameObject().transform;
            _container.name = _containerName;
        }

        private void InitializeCanvas()
        {
            _mainMenuCanvas = Object.Instantiate(_mainMenuCanvasPrefab, _container);
        }

        private void InitializePlayButton()
        {
            _mainMenuCanvas.PlayButton.onClick.AddListener(PlayButton_OnClick); 
        }

        private void PlayButton_OnClick()
        {
            _onGameStarted?.Invoke();
        }
    }
}
