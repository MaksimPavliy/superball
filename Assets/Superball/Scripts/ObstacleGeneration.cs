using FriendsGamesTools;
using Superball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class ObstacleGeneration : MonoBehaviourHasInstance<ObstacleGeneration>
    {
        [SerializeField] private GameObject[] obstacles;
        private int randomOption;
        private GameObject firstObstaclePos;
        private int previousRandomOption;
        private float counter;
        private IEnumerator _spawn;
        private List<GameObject> _objects = new List<GameObject>();
        private float spawnDelay => SuperballGeneralConfig.instance.obstacleSpawnDelay;
        private float delayDeviation = 0.1f;
        private float maxSpawnX = 3f;
        void Start()
        {
            _spawn = Spawn();
            EventSignature();
            for (int i = 0; i < _objects.Count; i++)
            {
                Destroy(_objects[i]);
            }
            _objects.Clear();
        }

        public void AddObstacles(GameObject obstacle)
        {
            _objects.Add(obstacle);
        }

        public void RemoveObstacles(GameObject obstacle)
        {
            _objects.Remove(obstacle);
        }

        private void EventSignature()
        {
            GameManager.instance.PlayPressed.AddListener(OnPlay);
            GameManager.instance.LevelComplete.AddListener(DoLose);
        }

        private void OnPlay()
        {
            /*StartCoroutine(_spawn);*/
        }

        private void DoLose()
        {
            StopCoroutine(_spawn);
        }

        private void OnDestroy()
        {
            if (!GameManager.instance) return;
            GameManager.instance.PlayPressed.RemoveListener(OnPlay);
            GameManager.instance.LevelComplete.RemoveListener(DoLose);
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                counter = Random.Range(0, 6);
                if (counter != 2 && counter != 5)
                    TopSpawn();

                if (counter == 2f)
                    SideSpawn(-9f, -6.5f);

                if (counter == 5f)
                    SideSpawn(9f, 6.5f);

                yield return new WaitForSeconds(Utils.Random(1-delayDeviation,1+delayDeviation)*spawnDelay);
            }
        }

        private void TopSpawn()
        {
            float startY = GameSettings.instance.SpawnPos.y;
            randomOption = Random.Range(0, obstacles.Length);
            previousRandomOption = randomOption;

            firstObstaclePos = Instantiate(obstacles[randomOption], new Vector3(Random.Range(-maxSpawnX, maxSpawnX), startY, 2f), Quaternion.identity);
            firstObstaclePos.transform.SetParent(transform);

            while (randomOption == previousRandomOption)
                randomOption = Random.Range(0, obstacles.Length);

            //if (Random.value < 0.5f)
            //{
            //    var obs = Instantiate(obstacles[randomOption], new Vector3(firstObstaclePos.transform.position.x + 2.5f, startY, 2f), Quaternion.identity);
            //    obs.transform.SetParent(transform);
            //}
            //else
            //{
            //    var obs = Instantiate(obstacles[randomOption], new Vector3(firstObstaclePos.transform.position.x - 2.5f, startY, 2f), Quaternion.identity);
            //    obs.transform.SetParent(transform);
            //}
        }

        private void SideSpawn(float firstObstX, float secondObstX)
        {
            randomOption = Random.Range(0, obstacles.Length);
            previousRandomOption = randomOption;

            firstObstaclePos = Instantiate(obstacles[randomOption], new Vector3(firstObstX, Random.Range(2f, 3f), 2f), Quaternion.identity);
            firstObstaclePos.transform.SetParent(transform);

            while (randomOption == previousRandomOption)
                randomOption = Random.Range(0, obstacles.Length);

            //var obs = Instantiate(obstacles[randomOption], new Vector3(secondObstX, firstObstaclePos.transform.position.y, 2f), Quaternion.identity);
            //obs.transform.SetParent(transform);

        }
    }
}