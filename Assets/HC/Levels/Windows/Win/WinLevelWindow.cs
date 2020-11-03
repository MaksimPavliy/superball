using FriendsGamesTools;
using FriendsGamesTools.Ads;
using FriendsGamesTools.ECSGame.Player.Money;
using FriendsGamesTools.Haptic;
using FriendsGamesTools.Share;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class WinLevelWindow: EndLevelWindow
    {
        public static void Show() => ShowWithDelay<WinLevelWindow>(0.5f);
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button shareButton;
        [SerializeField] private WatchAdButtonView xAdRewardForButton;
        [SerializeField] GainedRewardView rewardView;
        [SerializeField] Transform starsParent;
        StarView[] stars;
        double levelReward => HСMoneyConfig.instance.levelWinMoney;
        public override string shownText => base.shownText + "\nCOMPLETED!";
        
        protected override void Awake()
        {
            base.Awake();
            nextLevelButton.Safe(() => nextLevelButton.onClick.AddListener(NextPressed));
            shareButton.Safe(()=> shareButton.onClick.AddListener(() => ShareManager.Share("share text stub")));
            AwakeAdReward();
        }

        #region Money multiplying
        int xRewardForAd;
#if ADS
        async void OnXRewardAdWatched()
        {
            buttonsParent.SetActive(false);
            xAdRewardForButton.gameObject.SetActive(false);
            rewardView.PlayTween();
            await Awaiters.Seconds(0.25f);
            rewardView.SetReward(levelReward * xRewardForAd);
            await Awaiters.Seconds(1f);
            ShowNextWindow(xRewardForAd);
        }
        void AwakeAdReward()
        {
            xAdRewardForButton.Safe(() => xAdRewardForButton.SubscribeAdWatched(OnXRewardAdWatched));
        }
        void InitRewardForAd()
        {
            xRewardForAd = Utils.Chance(HСMoneyConfig.instance.levelWinX3Chance) ? 3 : 2;
            xAdRewardForButton.Safe(() => xAdRewardForButton.gameObject.SetActive(true));
        }
#else
        void AwakeAdReward(){}
        void InitRewardForAd()
        {
            xRewardForAd = 2;
            xAdRewardForButton.Safe(() => xAdRewardForButton.gameObject.SetActive(false));
        }
#endif
        #endregion

        void InitStars() => stars = starsParent.GetComponentsInChildren<StarView>();
        protected override async void OnEnable()
        {
            base.OnEnable();
            InitRewardForAd();
            rewardView.SetReward(levelReward);
            buttonsParent.SetActive(false);

            const int starCount = 3;
            if (stars.CountSafe() != starCount)
                InitStars();
            stars.ForEach(s => s.SetState(false));

            await Awaiters.Seconds(1);

            Haptic.Vibrate(HapticType.Light);
            const float ratio = 0.5f;
            float[] thresholds = { 0f, 0.4f, 1f };
            for (int i = 0; i < starCount; i++)
            {
                stars[i].SetState(ratio >= thresholds[i]);
                await Awaiters.Seconds(0.5f);
                Haptic.Vibrate(HapticType.Medium);
            }
            buttonsParent.SetActive(true);
        }

        void NextPressed() {
            ShowNextWindow(1);
        }

        bool moneySoakIsPlaying => MoneySoakEffect.instance.isPlaying;
        public async void ShowNextWindow(int moneyMultiplier)
        {
            buttonsParent.SetActive(false);
            root.levels.GiveWinMoney(moneyMultiplier);
            await Awaiters.Until(() => moneySoakIsPlaying);
            await Awaiters.Until(() => !moneySoakIsPlaying);
            await AsyncUtils.FramesCount(3);
            shown = false;
            if (root.progressSkin.anySkinLocked)
                RewardProgressWindow.Show();
            else
                root.levels.ChangeLocation();
        }
    }
}