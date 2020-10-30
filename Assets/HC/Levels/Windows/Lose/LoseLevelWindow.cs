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
        public static async void Show()
        {
            var instance = Windows.Get<LoseLevelWindow>();
            instance.continueForAdButton?.gameObject.SetActive(instance.proposeAds);
            const float showDelay = 1.5f;
            await Awaiters.Seconds(showDelay);
            instance.shown = true;
        }
        private void OnContinueAdWatched()
        {
            shown = false;
            root.levels.RestartLocation();
        }
    }
}