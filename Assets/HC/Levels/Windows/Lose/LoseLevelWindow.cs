using FriendsGamesTools;
using FriendsGamesTools.Ads;
using FriendsGamesTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class LoseLevelWindow : EndLevelWindow
    {
        [SerializeField] private WatchAdButtonView continueForAdButton;
        [SerializeField] protected Button restartLevelButton;
        public override string shownText => base.shownText + "\nFAILED!";
        protected override void Awake()
        {
            base.Awake();
            restartLevelButton.Safe(() => restartLevelButton.onClick.AddListener(OnRestartLevelPressed));
#if ADS
            continueForAdButton.Safe(()=> continueForAdButton.SubscribeAdWatched(OnContinueAdWatched));
#endif
        }
        private void OnContinueAdWatched()
        {
            shown = false;
            root.levels.RestartLocation();
        }
        public static void Show() => ShowWithDelay<LoseLevelWindow>(0.5f);
        protected override void OnEnable()
        {
            base.OnEnable();
            continueForAdButton?.gameObject.SetActive(proposeAds);
        }
    }
}