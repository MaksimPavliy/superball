using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using FriendsGamesTools;

namespace Superball
{
    public class SuperballLevelGenerator: MonoBehaviour
    {
        [SerializeField] private List<Pipe> pipePrefabs;
        [SerializeField] private Vector2 pipeSize;

        [SerializeField] private GameObject finishPrefab;

        [SerializeField] private SpriteRenderer _groundSprite;
        [SerializeField] private SpriteRenderer _groundSpikes;
        private SuperballLevelConfig levelConfig => SuperballLevelConfig.instance;

        private float _groundAdditionalSpace = 20f;

        [SerializeField] private GameObject[] _layerHillsPrefabs;

        [SerializeField] private Transform[] _layerParents;
        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            ApplyGround();
            SpawnPipes();
            SpawnObstacles();
            SpawnFinish();
        }

        private void ApplyGround()
        {
            float levelLength = levelConfig.GetLevelLength();
            float groundLength= levelLength+_groundAdditionalSpace * 2f;
            _groundSprite.size = new Vector2(_groundSprite.size.x, groundLength);
            var groundPosition = _groundSprite.transform.position;
            groundPosition.x = levelLength / 2f;
            _groundSprite.transform.position=groundPosition;

            _groundSpikes.size= new Vector2(groundLength, _groundSpikes.size.y);
            _groundSpikes.transform.position = new Vector3(levelLength / 2f, 0, 0);
            var collider = _groundSpikes.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(groundLength, collider.size.y);
        }
        private void SpawnPipes()
        {
            int pipeCount = levelConfig.GetPipesCount();
            Pipe pipePrefab = null;

            for (int i = 0; i < pipeCount; i++)
            {

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