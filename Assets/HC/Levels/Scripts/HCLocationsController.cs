using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using Unity.Entities;

namespace HC
{
    public class HCLevelsController : LevelBasedController
    {
        protected override (bool win, bool lose) CheckWinLose() => (false, false);
        HСMoneyConfig moneyConfig => HСMoneyConfig.instance;
        public override int levelWinMoney => moneyConfig.levelWinMoney;
        public override float levelWinX3Chance => moneyConfig.levelWinX3Chance;
        public override int winStarsCount => 2;

        public override void RestartLocation()
        {
            base.RestartLocation();
            HCLevelsView.instance?.Reset();
        }

        public override void Play()
        {
            base.Play();
            HCAnalyticsManager.LevelStart(currLocationInd);
        }
        protected override void OnWin()
        {
            base.OnWin();
            HCAnalyticsManager.LevelFinish(currLocationInd);
        }
        protected override void OnLose()
        {
            base.OnLose();
            HCAnalyticsManager.LevelFailed(currLocationInd);
        }
    }
}