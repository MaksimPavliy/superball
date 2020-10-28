using FriendsGamesTools.Haptic;
using TMPro;
using UnityEngine;

namespace HC
{
    public abstract class EndLevelWindow : HCWindow
    {
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] protected GameObject buttonsParent;
        protected bool proposeAds => HCAdsManager.AdsProposingEnabled;
        public virtual string shownText => $"LEVEL {HCRoot.instance.locations.currLocationInd+1} ";
        public virtual void Show(bool show = true)
        {
            shown = show;
            if (!show) return;
           if(levelText!=null) levelText.text = shownText;
            Haptic.Vibrate(HapticType.Medium);
        }
        public void StartGame() => HCRoot.instance.StartLevel();
        public void OpenCustomization() => HCRoot.instance.OpenCustomization();
        public void RestartLevel() => HCRoot.instance.RestartLevel();
        public void NextLevel() => HCRoot.instance.NextLevel();
        public void ShowItemProgress() => HCRoot.instance.ShowItemProgress();
        public void GoToMain() => HCRoot.instance.GoToMain();
        public void Continue() => HCRoot.instance.Continue();

    }
}