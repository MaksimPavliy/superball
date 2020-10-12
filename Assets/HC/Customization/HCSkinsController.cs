using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace HC
{
    public struct HCSkin : IBufferElementData
    {
        public int index;
        public bool locked;
        public bool bought;
        public int price;
    }

    public struct HCSkinsData : IComponentData
    {
        public int activeIndex;
        public bool inited;
        public int unlockPrice;
        public int skinsCount;
        public bool allUnlocked;
    }

    public abstract class HCSkinsController<T> : HCSkinsController where T: struct,IComponentData
    {
        public override Entity entity => ECSUtils.GetSingleEntity<T>();
        public override HCSkinsManager skinsManager => GetManager<T>();
        public override void InitDefault()
        {
            var e = ECSUtils.CreateEntity(new T());
            base.InitDefault();
        }
    }
        public abstract class HCSkinsController : Controller
    {
        public virtual Entity entity => Entity.Null;
        public HCSkinsData skinsData => entity.GetComponentData<HCSkinsData>();
        HCSkinsManager skinsManagersParent => HCRoot.instance.skin1Manager;
        public virtual HCSkinsManager skinsManager => null;
        public override int updateEvery => 2;
        bool skinSet = false;
        protected virtual int priceIncrease => 50;//SkinsConfig.instance.increasePricePerUnlock;
        public override void InitDefault()
        {
            base.InitDefault();
            entity.AddComponent(new HCSkinsData { activeIndex = 0, inited = false, unlockPrice = -1, allUnlocked = false });
            InitSkins();
        }
        public override void OnInited()
        {
            base.OnInited();
            skinSet = false;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (skinsData.unlockPrice == -1) entity.ModifyComponent((ref HCSkinsData skins) => { skins.unlockPrice = 5;/* SkinsConfig.instance.baseUnlockPrice; */});
        }
        public void InitSkins()
        {
            entity.AddBuffer<HCSkin>();
            var buff = entity.GetBuffer<HCSkin>();
            var manager = skinsManager;
            foreach (var skin in manager.SkinAssets)
            {
                buff.Add(new HCSkin { index = skin.Index, locked = skin.Locked, bought = skin.Index == 0, price = skin.price });
            }
            entity.ModifyComponent((ref HCSkinsData skins) => { skins.inited = true; skins.skinsCount = manager.SkinAssets.Length; });
            if (!AnySkinLocked()) entity.ModifyComponent((ref HCSkinsData skins) => { skins.allUnlocked = true; });
        }
        public DynamicBuffer<HCSkin> GetSkins() => entity.GetBuffer<HCSkin>();

        public bool AnySkinLocked()
        {
            var buff = entity.GetBuffer<HCSkin>();
            foreach (HCSkin skin in buff)
            {
                if (skin.locked) return true;
            }
            return false;
        }
        public void BuySkin(int index)
        {
            if (!AnySkinLocked()) return;
            var buff = entity.GetBuffer<HCSkin>();
            var skin = buff[index];
            if (HCRoot.instance.money.amount < skin.price) return;
            skin.bought = true;
            buff[index] = skin;
            HCRoot.instance.money.PayMoney(skin.price);
        }

        public int UnlockRandom(bool priceless = false)
        {
            if (!AnySkinLocked()) return -1;
            if (!priceless && HCRoot.instance.money.amount < skinsData.unlockPrice) return -1;
            var buff = entity.GetBuffer<HCSkin>();
            int unlockIndex = 0;

            while (!buff[unlockIndex].locked)
            {
                unlockIndex = Utils.Random(0, skinsData.skinsCount - 1);
            }
            var skin = buff[unlockIndex];
            skin.locked = false;
            buff[unlockIndex] = skin;
            HCRoot.instance.money.PayMoney(skinsData.unlockPrice);
            entity.ModifyComponent((ref HCSkinsData skins) => { skins.unlockPrice += priceIncrease; });
            TakeSkin(unlockIndex);
            entity.ModifyComponent((ref HCSkinsData skins) => { skins.allUnlocked = !AnySkinLocked(); });
            return unlockIndex;
        }
        public void TakeSkin(int index)
        {
            entity.ModifyComponent((ref HCSkinsData skins) => { skins.activeIndex = index; });
        }

        protected HCSkinsManager<T> GetManager<T>() where T:struct,IComponentData
        {
            var skinsParent = skinsManagersParent;
          var managers = skinsParent.GetComponents<HCSkinsManager>();
            foreach (var manager in managers)
            {
                if (manager is HCSkinsManager<T>) return manager as HCSkinsManager<T>;
            }
            return null;
        }
    }
}