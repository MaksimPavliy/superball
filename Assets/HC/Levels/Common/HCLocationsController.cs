using FriendsGamesTools.ECSGame;
using FriendsGamesTools.ECSGame.Locations;
using Unity.Entities;
using UnityEngine;

namespace HC
{
    public struct Level : IComponentData
    {
        public enum State { inMenu, playing, win, lose }
        public State state;
        public int levelsPlayed;
    }
    public class HCLocationsController : LocationsController, ILocationSet
    {
        HCLocationsView view => HCLocationsView.instance;
        public override int locationsCount => view.locations.Count;
        protected override bool canChangePrivate => data.state == Level.State.lose || data.state == Level.State.win;
        Level data => e.GetComponentData<Level>();
        public Level.State state => data.state;
        public int levelsPlayed => data.levelsPlayed;
        public override void InitDefault()
        {
            base.InitDefault();
            e.AddComponent(new Level { });
        }
        public void OnLocationSet(int newLocationInd)
        {
            e.AddOrModifyComponent((ref Level l) => l.state = Level.State.playing);
        }
        public void GoToMenu() => e.ModifyComponent((ref Level l) => l.state = Level.State.inMenu);
        HCLocationView currLevelView => HCLocationsView.instance.currLocationView;
        new HCRoot root => HCRoot.instance;
        protected virtual (bool win, bool lose) CheckWinLose() => (false, false);
        void UpdateWinLose()
        {
            if (state != Level.State.playing) return;
            var (win, lose) = CheckWinLose();
            if (!lose && win)
                DoWin();
            else if (lose)
                DoLose();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            UpdateWinLose();
        }
        private void DoWin()
        {
            if (state != Level.State.playing) return;
            var maxLevel = loop ? int.MaxValue : locationsCount - 1;
            var levelToUnlock = Mathf.Min(maxLevel, currLocationInd + 1);
            e.ModifyComponent((ref Level l) => {
                l.state = Level.State.win;
                l.levelsPlayed++;
            });
        }
        private void DoLose()
        {
            if (state != Level.State.playing) return;
            e.ModifyComponent((ref Level l) => {
                l.state = Level.State.lose;
                l.levelsPlayed++;
            });
        }
        public void DebugWin() => DoWin();
        public void DebugLose() => DoLose();
        public void GiveWinMoney(int multiplier)
        {
            var money = HСMoneyConfig.instance.levelWinMoney * multiplier;
            root.money.AddMoneySoaked(money);
            e.ModifyComponent((ref Level l) =>
            {
                if (root.progressSkin.anySkinLocked)
                    l.state = Level.State.win;
                else
                    l.state = Level.State.inMenu;
            });
        }
        public void GiveWinProgress(int multiplier)
        {
            root.progressSkin.GiveWinProgress(multiplier);
            e.ModifyComponent((ref Level l) => l.state = Level.State.inMenu);
        }
        public void Play()
        {
            if (state == Level.State.playing) return;
            if (state == Level.State.inMenu || state == Level.State.lose)
                RestartLocation();
            else
                ChangeLocation();
        }
    }
}