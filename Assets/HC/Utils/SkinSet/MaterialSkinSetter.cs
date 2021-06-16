using UnityEngine;
namespace HcUtils
{
    public class MaterialSkinSetter : SkinSetSetter
    {
        private new Renderer renderer;
        protected override void Init()
        {
            renderer = GetComponent<Renderer>();
            base.Init();
        }
        public Material[] materials;
        protected override void SetSkin(int ind)
        {
            renderer.sharedMaterial = materials[ind];
        }
    }
}