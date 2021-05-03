using HC;

namespace HcUtils
{
    public class HCAdButtonBlocker : AdButtonBlocker
    {
        protected override bool GameLoaded => HCAdsManager.instance;
        protected override bool RewardedAvailable => HCAdsManager.instance.rewarded.available;
    }
}