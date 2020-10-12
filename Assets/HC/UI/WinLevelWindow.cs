using FriendsGamesTools;
using FriendsGamesTools.Ads;
using FriendsGamesTools.ECSGame.Player.Money;
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
        double levelReward => HCLocationsView.instance.shownLocationView.winMoney;
        string shareText => "share text";
        public override string shownText => base.shownText + "Completed!";
        private void Awake()
        {
            nextLevelButton?.onClick.AddListener(NextPressed);
            replayButton?.onClick.AddListener(RestartLevel);
            shareButton?.onClick.AddListener(delegate { UIView.instance.Share(shareText); });
            adRewardButton.GetComponent<WatchAdButtonView>().SubscribeAdWatched(DoubleRewardPressed);
        }
        bool moneySoakIsPlaying => MoneySoakEffect.instance.isPlaying;

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

        public override void Show(bool show = true)
        {
            base.Show(show);
            rewardView.SetReward(levelReward);
            adRewardButton.gameObject.SetActive(true);
        }
    }
}