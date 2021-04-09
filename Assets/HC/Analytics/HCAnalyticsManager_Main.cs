using FriendsGamesTools;
using FriendsGamesTools.Ads;

namespace HC
{
    public partial class HCAnalyticsManager
    {
        public static void LevelStart(string levelNumber)
        {
            SendFB("Level started", ("level number", levelNumber));

            IncAndSaveCounters(CountersType.level_count);

            LevelStartAM(levelNumber);
        }

        public static void LevelFailed(string levelNumber)
        {
            SendFB("Level failed", ("level number", levelNumber));

            LevelFinishAM(EndGameType.lose, levelNumber);
        }

        public static void LevelFinish(string levelNumber)
        {
            SendFB("Level finished", ("level number", levelNumber));

            LevelFinishAM(EndGameType.win, levelNumber);
        }

        private static void LevelStartAM(string levelNumber)
        {
            SendAM("level_start",
                ("level_count", LoadCounters(CountersType.level_count)),
                ("level_name", levelNumber),
                ("level_random", 1)
                );

            levelStartTime = GameTime.time;
        }

        private static void LevelFinishAM(EndGameType result, string levelNumber)
        {
            var duration = GameTime.time - levelStartTime;

            SendAM("level_finish",
                ("time", (int)duration),
                ("result", result.ToString()),
                ("level_count", LoadCounters(CountersType.level_count)),
                ("continue", LoadCounters(CountersType.continue_count)),
                ("level_name", levelNumber),
                ("level_random", 1),
                ("progress", 100)
                );

            if (EndGameType.win == result)
                ResetCounters(CountersType.continue_count);
        }

        public static void SkinBought(string ind) =>
            SendFB("Skin bought", ("skin index", ind));

        public static void OnAdPressed(AdType type, string placement, bool available)
        {
            SendAM("video_ads_available",
                  ("ad_type", type),
                  ("placement", placement),
                  ("result", available ? "success" : "not_available"),
                  ("connection", internet));

            if (available)
            {
                SendAM("video_ads_started",
                    ("ad_type", type),
                    ("placement", placement),
                    ("result", "start"),
                    ("connection", internet)
                    );
            }

            isWatchedSent = false;
        }

        public static void OnAdShowingFinished(AdType type, string placement, string state)
        {
            if (isWatchedSent) return;

            SendAM("video_ads_watch",
                  ("ad_type", type),
                  ("placement", placement),
                  ("result", state),
                  ("connection", internet)
                  );

            isWatchedSent = true;
        }

        public static void OnRateUs(int result)
        {
            SendAM("rate_us",
               ("show_reason", "new_version"),
               ("rate_result", result));
        }
    }
}
