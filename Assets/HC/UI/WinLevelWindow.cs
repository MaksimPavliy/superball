using FriendsGamesTools;
using FriendsGamesTools.Ads;
using FriendsGamesTools.ECSGame.Player.Money;
using FriendsGamesTools.Haptic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class WinLevelWindow: EndLevelWindow
    {
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button shareButton;
        [SerializeField] private Button adRewardButton;
        [SerializeField] GainedRewardView rewardView;
        [SerializeField] Transform starsParent;
        StarView[] stars;
        double levelReward => HCLocationsView.instance.shownLocationView.winMoney;
        bool hasItemProgress => HCuiConfig.instance.HasItemProgress;
        bool hasShare => HCuiConfig.instance.HasShareOnWin;
        float x3Chance => 0.25f;
        int xRewardForAd = 2;
        int GetXRewardForAd()=> Utils.Random(0, 1f) < x3Chance ? 3 : 2;
        protected string shareText => "share text";
        public override string shownText => base.shownText + "COMPLETED!";
        
        private void Awake()
        {
            nextLevelButton?.onClick.AddListener(NextPressed);
            shareButton?.onClick.AddListener(delegate { UIView.instance.Share(shareText); });
#if ADS
            if(hasAds) adRewardButton?.GetComponent<WatchAdButtonView>()?.SubscribeAdWatched(XRewardPressed);
#endif
        }
        void InitStars() =>
            stars = starsParent.GetComponentsInChildren<StarView>();
        bool moneySoakIsPlaying => MoneySoakEffect.instance.isPlaying;
        int starCount = 3;
        float showDelay => 1f;
        async void XRewardPressed()
        {
            adRewardButton.gameObject.SetActive(false);
            rewardView.PlayTween();
            await Awaiters.Seconds(0.25f);
            rewardView.SetReward(levelReward * xRewardForAd);
            await Awaiters.Seconds(1f);
            NextWindowAwaited(3);
          
        }
        void NextPressed()
        {
            if (closed) return;
            closed = true;
            NextWindowAwaited(1);
            rewardView.SetReward(levelReward);
        }
        public async void NextWindowAwaited(float multiplier=1 )
        {
            HCRoot.instance.money.AddWinMoney(levelReward*multiplier);
            await Awaiters.Until(() => moneySoakIsPlaying);
            await Awaiters.Until(() => !moneySoakIsPlaying);
            await Awaiters.EndOfFrame;
            await Awaiters.EndOfFrame;
            await Awaiters.EndOfFrame;
            if (hasItemProgress) ShowItemProgress(); else NextLevel();
        }
        bool closed = false;
        public async override void Show(bool show = true)
        {
            if (!show)
            {
                base.Show(show);
                return;
            }
            shareButton?.gameObject.SetActive(hasShare);
            adRewardButton?.gameObject.SetActive(hasAds);
            xRewardForAd = GetXRewardForAd();
            rewardView.SetReward(levelReward);
            buttonsParent?.SetActive(false);

            if (stars == null || stars.Length != starCount) InitStars();
            foreach (StarView star in stars)
            {
                star.SetState(false);
            }
            await Awaiters.Seconds(showDelay);
            closed = false;
            Haptic.Vibrate(HapticType.Light);
            base.Show(show);

            float ratio = 0.5f;
        
            float[] thresholds = { 0f, 0.4f, 1f };
            for (int i = 0; i < starCount; i++)
            {
                stars[i].SetState(ratio >= thresholds[i]);
                await Awaiters.Seconds(0.5f);
                Haptic.Vibrate(HapticType.Medium);
            }
            buttonsParent.SetActive(true);
        }
    }
}