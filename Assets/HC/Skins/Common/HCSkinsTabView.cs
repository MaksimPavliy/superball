using System.Collections.Generic;
using FriendsGamesTools;
using Unity.Entities;
using UnityEngine;


namespace HC
{
    public abstract class HCSkinsTabView : MonoBehaviour
    {
        [SerializeField] Transform slotsParent;
        [SerializeField] HCSkinItemView slotPrefab;
        HCRoot root => HCRoot.instance;
        public abstract string TabName { get; }
        public abstract string TabHint { get; }
        protected abstract HCSkinsController controller { get; }
        protected abstract IReadOnlyList<SkinViewConfig> viewConfigs { get; }
        protected virtual void Awake() { }
        List<HCSkinItemView> shownItems = new List<HCSkinItemView>();
        public virtual void UpdateView()
            => Utils.UpdatePrefabsList(shownItems, viewConfigs, slotPrefab, slotsParent, (viewConfig, view) => view.Show(viewConfig));
        public bool isActiveTab { get; private set; }
        public void SetActiveTab(bool isActiveTab) {
            this.isActiveTab = isActiveTab;
            UpdateView();
        }
        protected virtual void Update() { }
    }
    public abstract class HCSkinsTabView<TData> : HCSkinsTabView
        where TData : struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<TData>>();
    }
}