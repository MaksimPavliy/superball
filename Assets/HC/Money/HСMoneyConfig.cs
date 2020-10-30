using FriendsGamesTools.DebugTools;

namespace HC
{
    public class HСMoneyConfig : BalanceSettings<HСMoneyConfig>
    {
        public int startMoney = 200;
        public int levelWinMoney = 52;
        public float levelWinX3Chance => 0.25f;
    }
}