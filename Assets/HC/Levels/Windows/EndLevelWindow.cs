using FriendsGamesTools;
using FriendsGamesTools.Haptic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public abstract class EndLevelWindow : HCWindow
    {
        [SerializeField] protected GameObject buttonsParent;
        [SerializeField] private Button restartLevelButton;
        protected bool proposeAds => HCAdsManager.AdsProposingEnabled;
        public virtual string shownText => HCLocationsView.instance.ShownLocationName;
        protected virtual void OnEnable()
        {
            LevelBasedView.SetLevelText(shownText);
            Haptic.Vibrate(HapticType.Medium);
        }
        protected virtual void Awake()
        {
            restartLevelButton.Safe(() => restartLevelButton.onClick.AddListener(OnRestartLevelPressed));
        }
        protected virtual void OnRestartLevelPressed()
        {
            shown = false;
            root.levels.RestartLocation();
        }
    }
}