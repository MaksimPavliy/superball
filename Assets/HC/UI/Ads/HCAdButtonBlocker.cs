namespace HcUtils
{
    public class HCAdButtonBlocker : AdButtonBlocker
    {
#if ADS
        protected override bool GameLoaded => HCAdsManager.instance;
        protected override bool RewardedAvailable => HCAdsManager.instance.rewarded.available;
#endif
    }
}