using FriendsGamesTools.Analytics;
using System.Collections.Generic;

namespace HC
{
    public class HCAnalyticsManager
    {
        public enum EventType
        {
            FB,
            AZUR,
            SUPERSONIC
        }
        public abstract class AnalyticEvent
        {
            public string name;
            protected virtual string[] keys => null;
            public List<(string key, object value)> parameters=new List<(string key, object value)>();
            public void AddParam(string key, object value)
            {
                parameters.Add((key, value));
            }
            protected void InitParams()
            {
                foreach (var key in keys)
                {
                    AddParam(key, null);
                }
            }
            public void ModifyParam(string key, object value)
            {
                var param = parameters.Find(((string key, object value) x) => x.key == key);
                param.value = value;

            }
            public virtual void SetParams(object[] values)
            {
                foreach (var key in keys)
                {
                    ModifyParam(key, values);
                }
            }
            public (string key, object value)[] Params() => parameters.ToArray();

            public void SendAll()
            {
                AnalyticsManager.Send(name, Params());
            }
        }

        public class LevelEndAnalyticsEvent : AnalyticEvent
        {
            protected override string[] keys => new string[]
            {
             "level_number",
             "deaths_count",
            };
            public void SetParams(int levelNumber, int deathsCount) => SetParams(new object[] { levelNumber, deathsCount });

        }
     
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