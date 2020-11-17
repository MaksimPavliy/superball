using FriendsGamesTools.Analytics;

namespace HC
{
    public class HCAnalyticsManager
    {
        static void Send(string eventName, params (string key, object value)[] parameters) => AnalyticsManager.Send(eventName, parameters);

        public static void LevelStart(string levelNumber)
        {
            Send("Level started", ("level number", levelNumber));
        }

        public static void LevelFailed(string levelNumber)
        {
            Send("Level failed", ("level number", levelNumber));
        }

        public static void LevelFinish(string levelNumber)
        {
            Send("Level finished", ("level number", levelNumber));
        }

        public static void SkinBought(int ind) =>
            Send("Skin bought", ("skin index", ind));
    }
}