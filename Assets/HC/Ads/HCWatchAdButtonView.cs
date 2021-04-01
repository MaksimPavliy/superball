using FriendsGamesTools.Ads;
using System;
namespace HC
{
    public class HCWatchAdButtonView : WatchAdButtonView
    {
        private string nameInAnalytics;
        public void SubscribeAdWatched(string nameInAnalytics, Action onWatched)
        {
            SubscribeAdWatched(onWatched);
            this.nameInAnalytics = nameInAnalytics;
        }
        public void SubscribeAdWatched(string nameInAnalytics, Action<bool> onWatchFinished)
        {
            SubscribeAdWatched(onWatchFinished);
            this.nameInAnalytics = nameInAnalytics;
        }
        public void SubscribeWatchAdPressed(string nameInAnalytics, Action onWatchAdPressed)
        {
            SubscribeWatchAdPressed(onWatchAdPressed);
            this.nameInAnalytics = nameInAnalytics;
        }

        protected override void InterstitialShow()
        {
            HCAdsManager.instance.ShowInterstitial(nameInAnalytics,
                delegate { onWatchFinished.Invoke(true); },
                delegate { onWatchFinished.Invoke(false); });
        }
        protected override void RewardedShow()
        {

            HCAdsManager.instance.ShowRewarded(nameInAnalytics,
                delegate { onWatchFinished.Invoke(true); },
                delegate { onWatchFinished.Invoke(false); });
        }
    }
}