using FriendsGamesTools;
using FriendsGamesTools.ECSGame;

namespace HC
{
    public class HCProgressSkinController : ProgressSkinController
    {
        HCProgressSkinConfig config => HCProgressSkinConfig.instance;
        protected override int GetPercentsPerLevel() => Utils.Random(config.percentsPerLevelMin, config.percentsPerLevelMax);
    }
}