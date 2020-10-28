using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using System.Collections.Generic;
using Unity.Entities;

namespace HC
{
    public struct HCSkin : IBufferElementData
    {
        public bool locked;
    }

    public struct HCSkinsData : IComponentData
    {
        public int activeSkinInd;
    }

    // Some items that can be locked/unlocked, and one of them is active.
    public abstract class HCSkinsController : Controller
    {
        public abstract Entity entity { get; }
        protected abstract IReadOnlyList<SkinViewConfig> viewConfigs { get; }
        public bool anySkinLocked => entity.GetBuffer<HCSkin>().Any(skin => skin.locked);
        public int activeSkinInd => entity.GetComponentData<HCSkinsData>().activeSkinInd;
        public DynamicBuffer<HCSkin> skins => entity.GetBuffer<HCSkin>();
        public override int updateEvery => 2;
        public new HCRoot root => HCRoot.instance;
        public override void InitDefault()
        {
            base.InitDefault();
            entity.AddComponent(new HCSkinsData { activeSkinInd = 0 });
            entity.AddBuffer<HCSkin>();
            var buff = entity.GetBuffer<HCSkin>();
            viewConfigs.ForEach(skinConfig =>
                buff.Add(new HCSkin { locked = skinConfig.startUnlocked }));
        }
        public virtual void ActivateSkin(int ind)
            => entity.ModifyComponent((ref HCSkinsData skins) => skins.activeSkinInd = ind );
        public virtual void UnlockSkin(int ind) => skins.ModifyItem(ind, (ref HCSkin itemData) => itemData.locked = false);
        public int unlockedSkinsCount => skins.Count(skin => !skin.locked);
    }

    public abstract class HCSkinsController<T> : HCSkinsController where T : struct, IComponentData
    {
        public override Entity entity => ECSUtils.GetSingleEntity<T>();
        public override void InitDefault()
        {
            base.InitDefault();
            ECSUtils.CreateEntity(new T());
        }
    }
}