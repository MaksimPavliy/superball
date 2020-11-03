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
        public abstract IReadOnlyList<SkinViewConfig> viewConfigs { get; }
        public int unlockedSkinsCount => skins.Count(skin => !skin.locked);
        public bool anySkinLocked => entity.GetBuffer<HCSkin>().Any(skin => skin.locked);
        public int activeSkinInd => entity.GetComponentData<HCSkinsData>().activeSkinInd;
        public DynamicBuffer<HCSkin> skins => entity.GetBuffer<HCSkin>();
        public bool IsLocked(int ind) => skins[ind].locked;
        public override int updateEvery => 2;
        public new HCRoot root => HCRoot.instance;
        public override void InitDefault()
        {
            base.InitDefault();
            var e = entity;
            e.AddComponent(new HCSkinsData { activeSkinInd = 0 });
            var skins = e.AddBuffer<HCSkin>();
            viewConfigs.ForEach(skinConfig =>
                skins.Add(new HCSkin { locked = !skinConfig.startUnlocked }));
        }
        public virtual void ActivateSkin(int ind)
            => entity.ModifyComponent((ref HCSkinsData skins) => skins.activeSkinInd = ind );
        protected virtual void UnlockSkin(int ind) => skins.ModifyItem(ind, (ref HCSkin itemData) => itemData.locked = false);
    }

    public abstract class HCSkinsController<T> : HCSkinsController where T : struct, IComponentData
    {
        public override Entity entity => ECSUtils.GetSingleEntity<T>();
        public override void InitDefault()
        {
            ECSUtils.CreateEntity(new T());
            base.InitDefault();
        }
    }
}