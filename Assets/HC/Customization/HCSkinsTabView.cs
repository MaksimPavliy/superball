using System.Collections.Generic;
using FriendsGamesTools;
using Unity.Entities;
using UnityEngine;


namespace HC
{
    public abstract class HCSkinsTabView : MonoBehaviour
    {
        HCRoot root => HCRoot.instance;
        protected abstract HCSkinsController controller { get; }
        protected abstract IReadOnlyList<SkinViewConfig> viewConfigs { get; }
        /// ///
        [SerializeField] Transform slotsParent;
        [SerializeField] HCSkinSlotView slotPrefab;
        public virtual string TabName => "Skins";
        public virtual string TabHint => "Unlock random skin.";
        List<HCSkinSlotView> slots;

        public void UpdateView()
        {
            if (slots == null) CreateSlots();
            foreach (var slot in slots)
                slot.UpdateView();
        }

        
        public void TakeSkin(int index)
        {
            controller.ActivateSkin(index);
            UpdateView();
        }
        void CreateSlots() {
            slots = new List<HCSkinSlotView>();
            viewConfigs.ForEachWithInd((skinConfig, ind) => {
                var slot = Instantiate(slotPrefab, slotsParent);
                slot.Init(ind, skinConfig.ico, this);
                slots.Add(slot);
            });
        }
    }
    public abstract class HCSkinsTabView<TData> : HCSkinsTabView
        where TData : struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<TData>>();
    }
}