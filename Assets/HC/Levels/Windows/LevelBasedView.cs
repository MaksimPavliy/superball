using FriendsGamesTools;
using FriendsGamesTools.UI;
using TMPro;
using UnityEngine;

namespace HC
{
    public class LevelBasedView : MonoBehaviourHasInstance<LevelBasedView>
    {
        public CoreGameView coreGameView;
        public MainMenuWindow mainMenu => Windows.Get<MainMenuWindow>();
        public LoseLevelWindow loseWindow => Windows.Get<LoseLevelWindow>();
        public WinLevelWindow winWindow => Windows.Get<WinLevelWindow>();
        public RewardProgressWindow rewardWindow => Windows.Get<RewardProgressWindow>();
        public SkinsWindow skinsWindow => Windows.Get<SkinsWindow>();

        [SerializeField] TextMeshProUGUI levelText;
        public static void SetLevelText(string text)
        {
            if (instance != null)
                instance.levelText.text = text;
        }
    }
}