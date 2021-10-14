using FriendsGamesTools;
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
        private float rotationSpeed;
        private float maxRotation = 60f;
        private void Start()
        {
            ObstacleGeneration.instance.AddObstacles(this.gameObject);
            rotationSpeed = Utils.Random(-maxRotation, maxRotation);

            if (transform.position.x <= -GameSettings.instance.SpawnPos.x)
            {
                spawnedLeft = true;
            }

            if (transform.position.x >= GameSettings.instance.SpawnPos.x)
            {
                spawnedRight = true;
            }

            if (transform.position.y == GameSettings.instance.SpawnPos.y)
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
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        private void RightMovement()
        {
            transform.position += new Vector3(fallSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x >= 10f)
                Destroy(gameObject);
        }

        private void LeftMovement()
        {
            transform.position -= new Vector3(fallSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x <= -10f)
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