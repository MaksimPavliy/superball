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
        public override string shownText => base.shownText + "\nCOMPLETED!";
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private WatchAdButtonView _doubleRewardButton;
        [SerializeField] Image progressBar;
        [SerializeField] TextMeshProUGUI progressText;
        protected override void Awake()
        {
            base.Awake();
            _nextLevelButton.Safe(() => _nextLevelButton.onClick.AddListener(() => root.levels.ChangeLocation()));
            _doubleRewardButton.Safe(() => _doubleRewardButton.SubscribeAdWatched(() => root.levels.GiveWinProgress(2)));
        }
        protected override async void OnEnable()
        {
            base.OnEnable();
            var startValue = root.progressSkinManager.progress;
            var startSkinInd = root.progressSkinManager.skinIndToUnlock;
            root.levels.GiveWinProgress(1);
            var afterX1Value = root.progressSkinManager.progress;
            var afterSkinInd = root.progressSkinManager.skinIndToUnlock;
            var showUnlockSkin = startSkinInd < afterSkinInd;
            if (showUnlockSkin) afterX1Value = 1;
            await AsyncUtils.SecondsWithProgress(0.5f, progress => {
                var value = Mathf.Lerp(startValue, afterX1Value, Mathf.SmoothStep(0, 1, progress));
                UpdateFillValue(value);
            });
            if (showUnlockSkin)
            {
                UpdateFillValue(0);
                
            }
        }
        private void UpdateFillValue(float value)
        {
            progressBar.fillAmount = value;
            progressText.text = value.ToShownPercents();
        }
    }
}