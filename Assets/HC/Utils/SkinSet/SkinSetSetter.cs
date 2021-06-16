using UnityEngine;
namespace HcUtils
{
    public abstract class SkinSetSetter : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }
        protected virtual void Init()
        {
            SetSkin(SkinSet.activeSet);
            SkinSet.SkinSetChanged += OnSkinSetChanged;
        }
        private void OnSkinSetChanged(int ind) => SetSkin(ind);
        protected abstract void SetSkin(int ind);

        protected void UpdateView() => SetSkin(SkinSet.activeSet);
        private void OnDestroy()
        {
            SkinSet.SkinSetChanged -= OnSkinSetChanged;
        }
    }
}