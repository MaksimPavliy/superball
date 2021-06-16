using UnityEngine;
namespace HcUtils
{
    public class CameraSkinSetter : SkinSetSetter
    {
        private new Camera camera;
        public Color[] colors;

        protected override void Init()
        {
            camera = GetComponent<Camera>();
            base.Init();
        }


        protected override void SetSkin(int ind)
        {
            camera.backgroundColor = colors[ind];
        }
    }
}