using UnityEngine;
using UnityEngine.UI;

namespace Superball
{
    public class JoystickShower: MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        private void Update()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = SuperballGeneralConfig.instance.showJoystick;
            }
        }
    }
}