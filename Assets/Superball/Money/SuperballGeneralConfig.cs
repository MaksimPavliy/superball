using FriendsGamesTools.DebugTools;

namespace Superball
{
    public class SuperballGeneralConfig : BalanceSettings<SuperballGeneralConfig>
    {
        public int CameraIndex = 0;
        public bool randomSpline;
        public int indexSpline;
        public float sensitivityTouch = 2f;
        public int jumpsPerTheme = 10;
        public float obstacleBaseSpeed = 2.5f;
        public float obstacleSpawnDelay = 4.2f;
    }
}