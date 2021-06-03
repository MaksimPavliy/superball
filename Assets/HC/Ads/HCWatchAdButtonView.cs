using FriendsGamesTools.Ads;
using UnityEngine;

namespace HC
{
    public class HCWatchAdButtonView : WatchAdButtonView
    {
        public NameInAnalytics nameInAnalytics;
        public GameObject adLoadingParent;
#if ADS
        protected override void InterstitialShow()
        {
            HCAdsManager.instance.ShowInterstitial(nameInAnalytics.ToString(),
                () => onWatchFinished.Invoke(true),
                () => onWatchFinished.Invoke(false));
        }

        protected override void RewardedShow()
        {
            HCAdsManager.instance.ShowRewarded(nameInAnalytics.ToString(),
                 () => onWatchFinished.Invoke(true));
        }

        private void Update()
        {
            if (adLoadingParent != null)
                adLoadingParent.gameObject.SetActive(!available);
        }
#endif
    }
}