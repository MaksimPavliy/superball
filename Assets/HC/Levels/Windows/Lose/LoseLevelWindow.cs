using FriendsGamesTools;
using FriendsGamesTools.Ads;
using FriendsGamesTools.UI;
using UnityEngine;


namespace HC
{
    public class LoseLevelWindow : EndLevelWindow
    {
        [SerializeField] private WatchAdButtonView continueForAdButton;
        public override string shownText => base.shownText + "\nFAILED!";
        protected override void Awake()
        {
            base.Awake();
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