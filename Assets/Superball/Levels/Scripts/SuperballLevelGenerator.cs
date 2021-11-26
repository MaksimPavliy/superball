using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Superball
{
    public class SuperballLevelGenerator: MonoBehaviour
    {
        [SerializeField] private List<Pipe> pipePrefabs;
        [SerializeField] private Vector2 pipeSize;

        [SerializeField] private GameObject finishPrefab;

        private SuperballLevelConfig levelConfig => SuperballLevelConfig.instance;

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            SpawnPipes();
            SpawnObstacles();
            SpawnFinish();
        }

        private void SpawnPipes()
        {
            int pipeCount = levelConfig.GetPipesCount();
            Pipe pipePrefab = null;

            for (int i = 0; i < pipeCount; i++)
            {
                // Берём рандомный префаб, исключая последний установленый, либо можно просто pipePrefabs[Random.Range(0, pipePrefabs.Count)]
                pipePrefab = pipePrefabs.Where(p => p != pipePrefab).OrderBy(p => Random.value).First();
                Vector3 pos = new Vector2(levelConfig.pipesStartSpawnFrom + i * levelConfig.metersPerPipe + Random.Range(-levelConfig.metersPerPipeThreshold, levelConfig.metersPerPipeThreshold),
                    Random.Range(levelConfig.bottomBound + pipeSize.y * 0.5f, levelConfig.topBound - pipeSize.y * 0.5f));

                Pipe pipe = Instantiate(pipePrefab, transform);
                pipe.Spawn(pos);
            }
        }

        private void SpawnObstacles()
        {
            //Тоже самое, что с трубами
        }

        private void SpawnFinish()
        {
            GameObject finish = Instantiate(finishPrefab, transform);
            finish.transform.position = new Vector2(levelConfig.GetLevelLength(), 0);
        }
    }
}