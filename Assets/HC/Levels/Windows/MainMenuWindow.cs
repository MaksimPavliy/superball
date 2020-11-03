using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class MainMenuWindow : HCWindow
    {
        public static void Show()
        {
            HCRoot.instance.levels.GoToMenu();
            Show<MainMenuWindow>();
        }
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _bottonCustomization;
        private void Awake() {
            _buttonPlay.onClick.AddListener(OnPlayPressed);
            _bottonCustomization.onClick.AddListener(SkinsWindow.Show);
        }
        private void OnPlayPressed()
        {
            shown = false;
            root.levels.Play();
        }
    }
}