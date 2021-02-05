using FriendsGamesTools.DebugTools;

namespace HC
{
    public class HCMoneyConfig : BalanceSettings<HCMoneyConfig>
    {
        public int levelWinMoneyBase = 50;
        public int levelWinMoneyCoef = 0;
        public float levelWinX3Chance => 0.25f;
    }
}