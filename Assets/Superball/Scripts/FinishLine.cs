using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class FinishLine : MonoBehaviour
    {
        public bool finished = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (finished) return;
            if (collision.CompareTag("playerBall"))
            {
                finished = true;
                GameManager.instance.OnLevelComplete();
            }
        }
    }
}
