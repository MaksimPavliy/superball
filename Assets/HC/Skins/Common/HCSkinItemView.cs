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
        [SerializeField] protected Image ico;
        [SerializeField] Button activateButton;
        [SerializeField] GameObject selectionParent;
        [SerializeField] protected GameObject unlockedParent;
        [SerializeField] protected GameObject lockedParent;
        [SerializeField] ParticleSystem unlockParticles;
        public SkinViewConfig config { get; private set; }
        public int skinInd { private set; get; } = -1;
        protected abstract HCSkinsController controller { get; }
        bool locked => controller.skins[skinInd].locked;
        bool selected => controller.activeSkinInd == skinInd;
        protected virtual void Awake() => activateButton.onClick.AddListener(ActivateSkin);
        public virtual void UpdateView()
        {
            ico.sprite = config.ico;
            selectionParent.SetActive(selected);
            unlockedParent.SetActive(!locked);
            lockedParent.SetActive(locked);
        }
        public void Show(SkinViewConfig config)
        {
            this.config = config;
            skinInd = controller.viewConfigs.IndexOf(config);
            UpdateView();
        }
        void ActivateSkin()
        {
            controller.ActivateSkin(skinInd);
            Windows.Get<SkinsWindow>().UpdateView();
        }
        public void PlayUnlockEffect()
        {
            if (unlockParticles != null)
                unlockParticles.gameObject.SetActive(true);
        }
    }
}