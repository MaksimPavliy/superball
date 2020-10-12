using TMPro;
using UnityEngine;
namespace HC
{
    public class CoreGameUI: MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI _levelName;
        public void SetLevelName(string name) => _levelName.text = name;
        public void Show(bool show = true) => gameObject.SetActive(show);
    }
}