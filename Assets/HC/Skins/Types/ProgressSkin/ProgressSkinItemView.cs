using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class ProgressSkinItemView : HCSkinItemView<ProgressSkin>
    {
        [SerializeField] Image icoFilled;
        new ProgressSkinController controller => HCRoot.instance.progressSkin;
        public override void UpdateView()
        {
            base.UpdateView();
            var progress = controller.GetProgress(skinInd);
            //var showLocked = controller.IsLocked(skinInd) && controller.skinIndToUnlock != skinInd;
            //unlockedParent.SetActive(!showLocked);
            //lockedParent.SetActive(showLocked);
            Show(ico, icoFilled, skinInd, progress);
        }
        public static void Show(Image ico, Image icoFilled, int skinInd, float progress)
        {
            var config = ProgressSkinsViewConfig.instance.items[skinInd];
            ico.sprite = config.ico;
            icoFilled.sprite = config.icoFilled;
            icoFilled.fillAmount = progress;
        }
    }
}