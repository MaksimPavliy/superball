using FriendsGamesTools.DebugTools;

namespace Superball
{
    public class BallConfig: BalanceSettings<BallConfig>
    {
        public float controlSensitity = 100f;
        public float samePipeSpeedMultiplier = 2f;
    }
}