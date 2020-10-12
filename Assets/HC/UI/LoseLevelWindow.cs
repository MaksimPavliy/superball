using FriendsGamesTools.Ads;
using UnityEngine;
using UnityEngine.UI;


namespace HC
{
    public class LoseLevelWindow: EndLevelWindow
    {
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _continueForAdButton;
        public override string shownText => base.shownText + "Failed!";
        private void Awake()
        {
            _restartLevelButton?.onClick.AddListener(delegate { HCRoot.instance.RestartLevel();});
            _continueForAdButton.GetComponent<WatchAdButtonView>().SubscribeAdWatched(Continue);
        }
        
        public void Continue()
        {
            HCRoot.instance.RestartLevel();
        }
        public override void Show(bool show = true)
        {
            base.Show(show);
        }
    }
}