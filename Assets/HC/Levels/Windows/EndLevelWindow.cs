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
        public virtual string shownText => $"LEVEL {HCRoot.instance.levels.currLocationInd+1} ";
        protected TextMeshProUGUI levelText => LevelBasedView.instance.levelText;
        protected virtual void OnEnable()
        {
            levelText.Safe(() => levelText.text = shownText);
            Haptic.Vibrate(HapticType.Medium);
        }
        protected virtual void Awake()
        {
            restartLevelButton.Safe(() => restartLevelButton.onClick.AddListener(root.levels.RestartLocation));

        }

        //public void StartGame() => HCRoot.instance.StartLevel();
        //public void OpenCustomization() => HCRoot.instance.OpenCustomization();
        //public void RestartLevel() => HCRoot.instance.RestartLevel();
        //public void NextLevel() => HCRoot.instance.NextLevel();
        //public void ShowItemProgress() => HCRoot.instance.ShowItemProgress();
        //public void GoToMain() => HCRoot.instance.GoToMain();
        //public void Continue() => HCRoot.instance.Continue();
    }
}