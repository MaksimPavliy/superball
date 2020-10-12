using FriendsGamesTools.Haptic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
namespace HC
{
    public class HCSkinSlotView<T>: HCSkinSlotView where T:struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<T>>();
    }
    public abstract class HCSkinSlotView : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] GameObject availableParent;
        [SerializeField] GameObject unAvailableParent;
        HCSkinsTabView tabParent;
        [SerializeField] GameObject selectionParent;
        [SerializeField] ParticleSystem unlockParticles;
        protected virtual HCSkinsController controller => null;
        bool locked => controller.GetSkins()[index].locked;

        public void Unlock()  =>unlockParticles?.gameObject.SetActive(true);
        public void Select(bool select = true) => selectionParent.SetActive(select);
        public void Take() => tabParent.TakeSkin(index);
        public void UpdateView()
        {
            availableParent.SetActive(!locked);
            unAvailableParent.SetActive(locked);
            selectionParent.SetActive(controller.skinsData.activeIndex == index);
        }
        public int index { private set; get; } = -1;
        public void Init(int index, Sprite sprite, int price, HCSkinsTabView tabParent)
        {
            this.index = index;
            image.sprite = sprite;
            this.tabParent = tabParent;
            availableParent.GetComponent<Button>()?.onClick.AddListener(Take);
            Select(false);
            
        }



    }
}