using FriendsGamesTools.DebugTools;

namespace Superball
{
    public class SuperballGeneralConfig : BalanceSettings<SuperballGeneralConfig>
    {
        public int CameraIndex = 0;
        public bool randomSpline;
        public int indexSpline;
        public float sensitivityTouch = 2f;

    }
}