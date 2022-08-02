using UnityEngine;
using UnityEngine.UI;

namespace UI_Module.CommonMenu
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private Button _playButton;

        public Button PlayButton => _playButton;
        public RectTransform Content => _content;
    }
}
