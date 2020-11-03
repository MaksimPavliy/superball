using System.Threading.Tasks;
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
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private WatchAdButtonView multiplyRewardButton;
        [SerializeField] private WatchAdButtonView restartLevelButton;
        [SerializeField] Image skinIco, skinIcoFilled;
        [SerializeField] TextMeshProUGUI progressText;
        ProgressSkinController controller => root.progressSkin;
        protected override void Awake()
        {
            base.Awake();
            nextLevelButton.Safe(() => nextLevelButton.onClick.AddListener(() => OnNextLevelPressed()));
            restartLevelButton.Safe(() => restartLevelButton.SubscribeAdWatched(OnRestartLevelPressed));
            multiplyRewardButton.Safe(() => multiplyRewardButton.SubscribeAdWatched(OnMultiplyRewardPressed));
        }
        protected override async void OnEnable()
        {
            base.OnEnable();
            restartLevelButton.SetActiveSafe(false);
            multiplyRewardButton.SetActiveSafe(false);
            nextLevelButton.SetActiveSafe(false);
            await GivingRewardAnimated(1);
            multiplyRewardButton.SetActiveSafe(true);
            await Awaiters.Seconds(1f);
            await Awaiters.While(() => showingAnim);
            restartLevelButton.SetActiveSafe(!controller.unlockAvailable);
            nextLevelButton.SetActiveSafe(true);
        }
        async void OnMultiplyRewardPressed()
        {
            multiplyRewardButton.SetActiveSafe(false);
            await GivingRewardAnimated(4);
        }
        bool showingAnim;
        async Task GivingRewardAnimated(int multiplier)
        {
            await Awaiters.While(() => showingAnim);
            showingAnim = true;
            var startValue = controller.progress;
            root.levels.GiveWinProgress(multiplier);
            var endValue = controller.progress;
            await AsyncUtils.SecondsWithProgress(0.5f, progress => {
                var value = Mathf.Lerp(startValue, endValue, Mathf.SmoothStep(0, 1, progress));
                UpdateFillValue(value);
            });
            if (controller.unlockAvailable)
            {
                var unlocked = await UnlockProgressSkinWindow.Showing();
                controller.UnlockOrLooseSkin(unlocked);
                root.levels.ChangeLocation();
            }
            showingAnim = false;
        }
        private void UpdateFillValue(float value)
        {
            ProgressSkinItemView.Show(skinIco, skinIcoFilled, controller.skinIndToUnlock, value);
            progressText.text = value.ToShownPercents();
        }
        protected void OnNextLevelPressed()
        {
            shown = false;
            root.levels.ChangeLocation();
        }
    }
}