using FriendsGamesTools.DebugTools;

namespace HC
{
    public class HCMoneyConfig : BalanceSettings<HCMoneyConfig>
    {
        public int levelWinMoney = 52;
        public float levelWinX3Chance => 0.25f;
    }
}