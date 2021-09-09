using FriendsGamesTools;
using FriendsGamesTools.ECSGame;

namespace HC
{
    public class GameManager: MonoBehaviourHasInstance<GameManager>
    {
        public void OnPlay()
        {
            //TODO
        }

        public void DoWin()
        {
            GameRoot.instance.Get<WinnableLocationsController>().DoWin();
        }

        public void DoLose()
        {
            GameRoot.instance.Get<WinnableLocationsController>().DoLose();
        }
    }
}