using FriendsGamesTools;
using FriendsGamesTools.Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class RewardProgressWindow : EndLevelWindow
    {
        public static void Show() => Show<RewardProgressWindow>();
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private WatchAdButtonView _doubleRewardButton;
        [SerializeField] Image progressBar;
        [SerializeField] TextMeshProUGUI progressText;
        float progressValue => 0.5f;
        protected override void Awake()
        {
            base.Awake();
            _nextLevelButton.Safe(() => _nextLevelButton.onClick.AddListener(() => root.levels.ChangeLocation()));
            _doubleRewardButton.Safe(() => _doubleRewardButton.SubscribeAdWatched(() => root.levels.GiveWinProgress(2)));
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateView();
        }
        public void UpdateView()
        {
            progressBar.fillAmount = progressValue;
            progressText.text = $"{progressValue}%";
        }
    }
}