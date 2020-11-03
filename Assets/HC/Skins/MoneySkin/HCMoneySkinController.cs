using FriendsGamesTools;
using FriendsGamesTools.ECSGame;

namespace HC
{
    public class HCMoneySkinController : MoneySkinController
    {
        HCMoneySkinConfig config => HCMoneySkinConfig.instance;
        public override int GetSkinPrice(int unlockedSkinsCount)
            => config.priceBase * Utils.Pow(config.priceMul, unlockedSkinsCount - 1);
    }
}