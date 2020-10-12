using FriendsGamesTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _bottonCustomization;
        [SerializeField] private Button _buttonVibro;
        private void Awake()
        {
            _buttonPlay.onClick.AddListener(delegate{HCRoot.instance.StartLevel();});
            _bottonCustomization.onClick.AddListener(delegate {HCRoot.instance.OpenCustomization();});
            _buttonVibro.onClick.AddListener(delegate {/*toggle vibro*/});
    }
        public void Show(bool show=true)=> shown = show;

    }
}