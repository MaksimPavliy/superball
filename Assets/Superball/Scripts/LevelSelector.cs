using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] GameObject[] backgrounds;
        [SerializeField] Ball ball;
        [SerializeField] GameObject background;
        private bool changed = true;

        private int counter;

        private void Update()
        {
            ChangeBackground();
        }

        private void ChangeBackground()
        {

            if (ball.jumpCounter % 3 != 0)
            {
                changed = false;
            }

            if (ball.jumpCounter % 3 == 0 && changed == false)
            {
                counter++;
                counter = counter > backgrounds.Length - 1 ? 0 : counter;

                changed = true;
                Destroy(background);
                background = Instantiate(backgrounds[counter], new Vector3(0f, -0.28f, 2f), Quaternion.identity);
            }
        }
    }
}