using FriendsGamesTools.Analytics;

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
    }
}