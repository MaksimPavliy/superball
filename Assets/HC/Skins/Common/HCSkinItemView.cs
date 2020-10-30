using FriendsGamesTools;
using FriendsGamesTools.UI;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
namespace HC
{
    public abstract class HCSkinItemView<T> : HCSkinItemView where T : struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<T>>();
    }
    public abstract class HCSkinItemView : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Button activateButton;
        [SerializeField] GameObject selectionParent;
        [SerializeField] GameObject unlockedParent;
        [SerializeField] GameObject lockedParent;
        [SerializeField] ParticleSystem unlockParticles;
        public SkinViewConfig config { get; private set; }
        public int index { private set; get; } = -1;
        protected abstract HCSkinsController controller { get; }
        bool locked => controller.skins[index].locked;
        bool selected => controller.activeSkinInd == index;
        protected virtual void Awake() => activateButton.onClick.AddListener(ActivateSkin);
        public void UpdateView()
        {
            image.sprite = config.ico;
            selectionParent.SetActive(selected);
            unlockedParent.SetActive(!locked);
            lockedParent.SetActive(locked);
        }
        public void Show(SkinViewConfig config)
        {
            this.config = config;
            index = controller.viewConfigs.IndexOf(config);
            UpdateView();
        }
        void ActivateSkin()
        {
            controller.ActivateSkin(index);
            Windows.Get<SkinsWindow>().UpdateView();
        }
        public void PlayUnlockEffect()
        {
            if (unlockParticles != null)
                unlockParticles.gameObject.SetActive(true);
        }
    }
}