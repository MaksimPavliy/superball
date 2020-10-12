using FriendsGamesTools.ECSGame;
using Unity.Entities;

namespace HC
{
    public struct CustSkin1 : IComponentData { bool _; }
    public class HCSkin1controller: HCSkinsController<CustSkin1>
    {
        public override Entity entity => ECSUtils.GetSingleEntity<CustSkin1>();
    }
}