using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class ObstacleFall : MonoBehaviour
    {
        private float fallSpeed = 2f;
        private bool spawnedLeft;
        private bool spawnedRight;
        private bool spawnedTop;

        private void Start()
        {
            ObstacleGeneration.instance.AddObstacles(this.gameObject);

            if (transform.position.x <= -3.5f)
            {
                spawnedLeft = true;
            }

            if (transform.position.x >= 3.5f)
            {
                spawnedRight = true;
            }

            if (transform.position.y == 9f)
            {
                spawnedTop = true;
            }
        }

        private void OnDestroy()
        {
            ObstacleGeneration.instance.RemoveObstacles(this.gameObject);
        }

        void Update()
        {
            if (spawnedLeft)
            {
                RightMovement();
            }

            if (spawnedRight)
            {
                LeftMovement();
            }

            if (spawnedTop)
            {
                BottomMovement();
            }
        }

        private void RightMovement()
        {
            transform.position += new Vector3(fallSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x >= 6f)
                Destroy(gameObject);
        }

        private void LeftMovement()
        {
            transform.position -= new Vector3(fallSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x <= -6f)
                Destroy(gameObject);
        }

        private void BottomMovement()
        {
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0f);

            if (transform.position.y < -2.5f)
                Destroy(gameObject);
        }
    }
}