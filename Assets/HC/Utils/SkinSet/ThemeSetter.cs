using UnityEngine;
namespace HcUtils
{
    public abstract class ThemeSetter : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }
        protected virtual void Init()
        {
            SetTheme(ThemeSet.activeThemeIndex);
            ThemeSet.ThemeSetChanged += OnThemeChanged;
        }
        private void OnThemeChanged(int ind) => SetTheme(ind);
        protected abstract void SetTheme(int ind);

        protected void UpdateView() => SetTheme(ThemeSet.activeThemeIndex);
        private void OnDestroy()
        {
            ThemeSet.ThemeSetChanged -= OnThemeChanged;
        }
    }
}