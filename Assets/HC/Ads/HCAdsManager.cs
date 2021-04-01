using FriendsGamesTools.Ads;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HC
{
    public class HCAdsManager
#if ADS
    : AdsManager<HCAdsManager>
#else
    : MonoBehaviour
#endif
    {
        public bool InterstitialTimerReady => GetTotalSeconds(DateTime.UtcNow) - _lastInterstitialSecondsTimestamp >= interstitialMinimumInterval;
        private long _lastInterstitialSecondsTimestamp = -1;
        private int interstitialMinimumInterval = 40;

        private bool _adShown = false;
        Action onAdSuccess;
        Action onAdFailed;
        public bool AdShown
        {
            private set
            {
                _adShown = value;
                OnAdShownChanged.Invoke();
            }
            get
            {
                return _adShown;
            }
        }
        public UnityEvent OnAdShownChanged = new UnityEvent();


        public void ShowInterstitial(string nameInAnalytics, Action onSuccess, Action onHidden)
        {
            if (!InterstitialTimerReady) return;

            if (!interstitial.available) return;
            AdShown = true;

            onAdSuccess = onSuccess;
            onAdFailed = onHidden;

            interstitial.Show(()=>
            {
                onAdSuccess?.Invoke();

                HCAnalyticsManager.OnAdShowingFinished(AdType.Interstitial, nameInAnalytics, "watched");

                onAdSuccess = null;
                onAdFailed = null;

                AdShown = false;
            });
        }

        public void ShowRewarded(string nameInAnalytics, Action onSuccess, Action onHidden)
        {
            AdShown = true;

            onAdSuccess = onSuccess;
            onAdFailed = onHidden;
            HCAnalyticsManager.OnAdPressed(AdType.RewardedVideo, nameInAnalytics);
            rewarded.Show(success =>
            {
                AdShown = false;
                if (success)
                {
                    onAdSuccess?.Invoke();
                    HCAnalyticsManager.OnAdShowingFinished(AdType.RewardedVideo, nameInAnalytics, "watched");
                }
                onAdSuccess = null;
                   

            });
        }
        
        protected override void OnInterstitialHidden()
        {
            base.OnInterstitialHidden();

            ResetInterstitialTimer();
        }
        protected override void OnRewardedShown()
        {
            base.OnRewardedShown();
            Debug.Log("rewarded shown");
        }
        protected override void OnRewardedHidden(bool success)
        {
            base.OnRewardedHidden(success);
            Debug.Log("rewarded hidden");

            onAdFailed?.Invoke();
            AdShown = false;
            ResetInterstitialTimer();
        }

        private static long GetTotalSeconds(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
        private void ResetInterstitialTimer()
        {
            _lastInterstitialSecondsTimestamp = GetTotalSeconds(DateTime.UtcNow);
        }
    }
}