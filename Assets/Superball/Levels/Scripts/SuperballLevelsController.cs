using FriendsGamesTools;
using FriendsGamesTools.ECSGame;

namespace Superball
{
    public class SuperballLevelsController : LevelBasedController
    {
        protected override (bool win, bool lose) CheckWinLose() => (false, false);
        SuperballMoneyConfig moneyConfig => SuperballMoneyConfig.instance;
        public override int levelWinMoney => moneyConfig.levelWinMoneyBase + currLocationInd * moneyConfig.levelWinMoneyCoef;
#if ADS
        public override float levelWinX3Chance => moneyConfig.levelWinX3Chance;
#endif
        public override int winStarsCount => Utils.Random(0, 1f) <= 0.7f ? 3 : 2;
        public bool LastGameIsWin { private set; get; } = false;
        public override void RestartLocation()
        {
            base.RestartLocation();

            SuperballLevelsView.instance?.Reset();
            GameManager.instance.OnReset();
        }

        public override void Play()
        {
            base.Play();

            GameManager.instance.OnPlay();
            ScoreManager.instance.ActiveScoreText(true);
            HCAnalyticsManager.LevelStart(currLocationInd.ToString());
        }

        protected override void OnWin()
        {
            base.OnWin();
            LastGameIsWin = true;
            HCAnalyticsManager.LevelFinish(currLocationInd.ToString());
        }

        protected override void OnLose()
        {
            base.OnLose();
            LastGameIsWin = false;
            ScoreManager.instance.ActiveScoreText(false);
            HCAnalyticsManager.LevelFailed(currLocationInd.ToString());
        }

        public override void DebugChangeLocation(int newLocationInd)
        {
            base.DebugChangeLocation(newLocationInd);
            GoToMenu();
        }
    }
}