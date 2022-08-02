using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace GameModule
{
    public class ClassicPlayerControllerCanvas : MonoBehaviour
    {
        [SerializeField]
        private Button _horizontalChooseButton;

        [SerializeField]
        private Button _hitPointChooseButton;

        [SerializeField]
        private Button _launchButton;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private RectTransform _content;

        private event Action _onHorizontalChooseClicked;
        private event Action _onHitPointChooseClicked;
        private event Action _onLaunchButtonClicked;
        private event Action<float> _onSliderValueChanged;

        public RectTransform Content => _content;

        public void OnHorizontalChooseClicked(Action action)
        {
            _onHorizontalChooseClicked += action;
        }

        public void OnHitPointChooseClicked(Action action)
        {
            _onHitPointChooseClicked += action;
        }

        public void OnLaunchButtonClicked(Action action)
        {
            _onLaunchButtonClicked += action;
        }

        public void OnSliderValueChanged(Action<float> action)
        {
            _onSliderValueChanged += action;
        }

        private void Awake()
        {
            InitializeButton(_hitPointChooseButton, HitPointChooseButton_OnClicked);
            InitializeButton(_horizontalChooseButton, HorizontalChooseButton_OnClicked);
            InitializeButton(_launchButton, LaunchButton_OnClicked);

            _slider.onValueChanged.AddListener(Slider_OnValueChanged);
        }

        private void InitializeButton(Button button, UnityAction action)
        {
            if (button == null)
            {
                Debug.LogError("Button is null");
            }

            button.onClick.AddListener(action);
        }

        private void HorizontalChooseButton_OnClicked()
        {
            _onHorizontalChooseClicked?.Invoke();
        }

        private void HitPointChooseButton_OnClicked()
        {
            _onHitPointChooseClicked?.Invoke();
        }

        private void LaunchButton_OnClicked()
        {
            _onLaunchButtonClicked?.Invoke();
        }

        private void Slider_OnValueChanged(float value)
        {
            _onSliderValueChanged?.Invoke(value);
        }
    }
}
