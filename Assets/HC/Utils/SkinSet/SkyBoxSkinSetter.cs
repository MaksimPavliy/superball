using UnityEngine;
namespace HcUtils
{
    public class SkyBoxSkinSetter : SkinSetSetter
    {
        public Material[] skyboxes;

        protected override void SetSkin(int ind)
        {
            RenderSettings.skybox = skyboxes[ind];
        }
    }
}