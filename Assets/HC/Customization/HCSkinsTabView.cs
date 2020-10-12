using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


namespace HC
{
    public abstract class HCSkinsTabView<T> : HCSkinsTabView where T : struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<T>>();
        protected override HCSkinsManager skinsManager => controller.skinsManager;
    }
    public abstract class HCSkinsTabView:MonoBehaviour
    {
        [SerializeField] Transform slotsParent;
        [SerializeField] HCSkinSlotView slotPrefab;
        protected virtual HCSkinsManager skinsManager => null;
        public virtual string TabName => "Skins";
        public virtual string TabHint => "Unlock random skin.";
        List<HCSkinSlotView> slots;
        protected virtual HCSkinsController controller => null;
        public double unlockPrice => controller.skinsData.unlockPrice;
        public bool allUnlocked => controller.skinsData.allUnlocked;
        public bool enoughMoney => HCRoot.instance.money.amount >= controller.skinsData.unlockPrice;
        public void UpdateView()
        {
            if (slots == null) CreateSlots();
            foreach (var slot in slots)
            {
                slot.UpdateView();
            }
        }

        public void UnlockRandom()
        {
            var unlockIndex = controller.UnlockRandom();
            UpdateView();
            if (unlockIndex != -1)
            {
                slots.Find(slot => slot.index == unlockIndex).Unlock();
            }
        }
        public void TakeSkin(int index)
        {
            controller.TakeSkin(index);
            UpdateView();
        }
        void CreateSlots()
        {
            slots = new List<HCSkinSlotView>();
            var manager = skinsManager;
            foreach (var skin in manager.SkinAssets)
            {
                var slot = Instantiate(slotPrefab, slotsParent);
                slot.Init(skin.Index, skin.SkinImage, skin.price, this);
                slots.Add(slot);
            }
        }

    }
}
  
