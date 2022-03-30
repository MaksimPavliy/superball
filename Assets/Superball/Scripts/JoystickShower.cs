using FriendsGamesTools;
using UnityEngine;
using UnityEngine.UI;

namespace Superball
{
    public class JoystickShower: MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private Joystick _joystick;
        private void Update()
        {
            _joystick.maxDistance = BallConfig.instance.joystickMaxDistance;
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = BallConfig.instance.showJoystick;
            }
        }
    }
}