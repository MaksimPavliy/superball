using FriendsGamesTools.Haptic;
using FriendsGamesTools.UI;
using TMPro;
using UnityEngine;

namespace HC
{
    public abstract class EndLevelWindow : Window
    {
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] protected GameObject buttonsParent;
        public virtual string shownText => $"Level {HCRoot.instance.locations.currLocationInd+1} ";
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
        public void ShowReward() => HCRoot.instance.ShowReward();
        public void GoToMain() => HCRoot.instance.GoToMain();
        
    }
}