using UnityEngine;
namespace HcUtils
{
    public class FogColorSkinSetter : SkinSetSetter
    {
        public Color[] colors;

        protected override void SetSkin(int ind)
        {
            RenderSettings.fogColor = colors[ind];
        }
    }
}