using FriendsGamesTools.ECSGame;
using UnityEngine;

namespace HC
{
    public class HCWinLevelWindow : WinLevelWindow
    {
        [SerializeField]
        private HCAnalyticsSender adSender;

        private void Start()
        {
            adSender.onPressed += SendX3AnalyticsPressed;
            adSender.onFinished += success => {
                if (success)
                {
                    SendX3AnalyticsWatched("watched");
                }
                else { SendX3AnalyticsWatched("canceled");
                }
            };
        }

        public void SendX3AnalyticsPressed() => HCAnalyticsManager.OnAdPressed(FriendsGamesTools.Ads.AdType.RewardedVideo, "get_x3_coins");
        public void SendX3AnalyticsWatched(string result) => HCAnalyticsManager.OnAdShowingFinished(FriendsGamesTools.Ads.AdType.RewardedVideo, "get_x3_coins", result);

    }
}