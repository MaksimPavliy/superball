using FriendsGamesTools.ECSGame;
using Unity.Entities;

namespace HC
{
    public struct CustSkin2 : IComponentData { bool _; }
    public class HCSkin2controller : HCSkinsController<CustSkin2>
    {
        public override Entity entity => ECSUtils.GetSingleEntity<CustSkin2>();
    }
}