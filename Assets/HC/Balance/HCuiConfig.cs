using FriendsGamesTools.DebugTools;

public class HCuiConfig : BalanceSettings<HCuiConfig>
{
    public bool HasAds =>
#if ADS
        true;
#else
        false;
#endif
    public bool HasItemProgress = true;
    public bool HasShareOnWin = true;
}
