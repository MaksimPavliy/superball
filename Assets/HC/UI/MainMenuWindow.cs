using FriendsGamesTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _bottonCustomization;
        private void Awake()
        {
            _buttonPlay.onClick.AddListener(delegate{HCRoot.instance.StartLevel();});
            _bottonCustomization.onClick.AddListener(delegate {HCRoot.instance.OpenCustomization();});
    }
        public void Show(bool show=true)=> shown = show;

    }
}