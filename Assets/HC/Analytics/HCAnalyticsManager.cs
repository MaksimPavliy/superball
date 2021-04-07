using FriendsGamesTools.Ads;
using FriendsGamesTools.Analytics;
using UnityEngine;

namespace HC
{
    public class HCAnalyticsManager
    {
        static void Send(string eventName, params (string key, object value)[] parameters) => AnalyticsManager.Send(eventName, parameters);

        public static void LevelStart(int levelNumber)
        {
            Send("Level started", ("level number", levelNumber.ToString()));
        }

        public static void LevelFailed(int levelNumber)
        {
            Send("Level failed", ("level number", levelNumber.ToString()));
        }

        public static void LevelFinish(int levelNumber)
        {
            Send("Level finished", ("level number", levelNumber.ToString()));
        }

        public static void SkinBought(int ind) =>
            Send("Skin bought", ("skin index", ind));

        public static void OnAdPressed(AdType type, string placement)
        {

            Send("video_ads_started",
                ("ad_type", type.ToString()),
                ("placement", placement),
                ("result", "start"));
            Debug.Log("ad watch pressed");
        }

        public static void OnAdShowingFinished(AdType type, string placement, string result)
        {

            Send("video_ads_watch",
                ("ad_type", type.ToString()),
                ("placement", placement),
                ("result", result));

            Debug.Log($"ad watch finished with {result}");
        }
    }
}
