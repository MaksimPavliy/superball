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
        [SerializeField] private Button replayButton;
        [SerializeField] private Button shareButton;
        [SerializeField] private Button adRewardButton;
        [SerializeField] GainedRewardView rewardView;
        [SerializeField] Transform starsParent;
   
        StarView[] stars;
        double levelReward => HCLocationsView.instance.shownLocationView.winMoney;
        string shareText => "share text";
        public override string shownText => base.shownText + "Completed!";
        private void Awake()
        {
            nextLevelButton?.onClick.AddListener(NextPressed);
            replayButton?.onClick.AddListener(RestartLevel);
            shareButton?.onClick.AddListener(delegate { UIView.instance.Share(shareText); });
          //  adRewardButton?.GetComponent<WatchAdButtonView>().SubscribeAdWatched(DoubleRewardPressed);
        }
        void InitStars() =>
            stars = starsParent.GetComponentsInChildren<StarView>();
        bool moneySoakIsPlaying => MoneySoakEffect.instance.isPlaying;
        int starCount = 3;
        float showDelay => 1f;
        async void DoubleRewardPressed()
        {
            adRewardButton.gameObject.SetActive(false);
            rewardView.PlayTween();
            await Awaiters.Seconds(0.25f);
            rewardView.SetReward(levelReward * 3);
            await Awaiters.Seconds(1f);
            NextWindowAwaited(3);
          
        }
        void NextPressed()
        {
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
            ShowReward();
        }
        bool closed = false;
        public async override void Show(bool show = true)
        {

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