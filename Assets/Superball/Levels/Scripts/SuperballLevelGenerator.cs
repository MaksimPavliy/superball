using FriendsGamesTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Superball
{
    public class SuperballLevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _finish;

        [SerializeField] private List<Pipe> pipePrefabs;
        [SerializeField] private Transform _pipesParent;

        [SerializeField] private GameObject finishPrefab;

        [SerializeField] private SpriteRenderer _groundSprite;
        [SerializeField] private SpriteRenderer _groundSpikes;
        private SuperballLevelConfig levelConfig => SuperballLevelConfig.instance;

        private float _groundAdditionalSpace = 20f;

        private Vector3 _lastPipePostion;

        [SerializeField] private Coin _coinPrefab;

        private List<Pipe> _pipes;
        [SerializeField] private List<Obstacle> _obstaclePrefabs;

        [SerializeField] private bool _setFinish = true;
        [SerializeField] private bool _spawnPipes = true;
        [SerializeField] private bool _spawnObstacles = true;
        [SerializeField] private bool _spawnCoins = true;
        [SerializeField] private Transform _ceiling;
        [SerializeField] private Transform _leftWall;
        private float _levelLength = 0;
        private float _groundLength = 0;
        private float _highestPipeHeight = 0;

        public void Generate()
        {
            _pipes = _pipesParent.GetComponentsInChildren<Pipe>().ToList();

            if (_setFinish)
            {
                SetFinish();
            }

            _levelLength = _finish.transform.position.x;

            ApplyBounds();
            if (_spawnPipes)
            {
                SpawnPipes();
            }

            if (_spawnCoins)
            {
                SpawnCoins();
            }
            ApplyCeiling();
        }

        private void ApplyBounds()
        {
            float levelLength = _levelLength;
            _groundLength = levelLength + _groundAdditionalSpace * 2f;
            _groundSprite.size = new Vector2(_groundSprite.size.x, _groundLength);
            var groundPosition = _groundSprite.transform.position;
            groundPosition.x = levelLength / 2f;
            _groundSprite.transform.position = groundPosition;

            _groundSpikes.size = new Vector2(_groundLength, _groundSpikes.size.y);
            _groundSpikes.transform.position = new Vector3(levelLength / 2f, 0, 0);
            var collider = _groundSpikes.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(_groundLength, collider.size.y);

            _leftWall.transform.position -= Vector3.right * (_groundAdditionalSpace);
        }

        private void ApplyCeiling()
        {
            _highestPipeHeight = 0;
            foreach (var p in _pipes)
            {
                if (p.transform.position.y > _highestPipeHeight)
                {
                    _highestPipeHeight = p.transform.position.y;
                }
            }
            var ceilingHeight = Mathf.Clamp(_highestPipeHeight + 13f, 0, levelConfig.maxLevelHeight);
            _ceiling.transform.position = _groundSpikes.transform.position + Vector3.up * ceilingHeight;
            _ceiling.transform.localScale = new Vector3(_groundLength - _groundAdditionalSpace, _ceiling.transform.localScale.y, 1);
        }
        private void SpawnPipes()
        {
            _lastPipePostion = new Vector3(1, 0, 0);
            float levelLength = _levelLength;
            int levelIndex = levelConfig.GetLevelIndex();
            Pipe pipePrefab = null;

            Vector2 minPipeDistanceRandom = new Vector2(levelConfig.pipeBiasX_Base_min, levelConfig.pipeBiasY_Base_min);
            Vector2 maxPipeDistanceRandom = new Vector2(levelConfig.pipeBiasX_Base_max, levelConfig.pipeBiasY_Base_max);

            Vector2 perPipeIncreaseMin = new Vector2(levelConfig.pipeBiasX_perPipeIncrement_min, levelConfig.pipeBiasY_perPipeIncrement_min);
            Vector2 perPipeIncreaseMax = new Vector2(levelConfig.pipeBiasX_perPipeIncrement_max, levelConfig.pipeBiasY_perPipeIncrement_max);

            Vector2 perLevelIncreaseMin = new Vector2(levelConfig.pipeBiasX_perPipeIncrement_min, levelConfig.pipeBiasY_perPipeIncrement_min);
            Vector2 perLevelIncreaseMax = new Vector2(levelConfig.pipeBiasX_perPipeIncrement_max, levelConfig.pipeBiasY_perPipeIncrement_max);

            Vector2 maxPipeDistance = new Vector2(levelConfig.pipeBiasX_limit, levelConfig.pipeBiasY_limit);

            float _minObtacleSpeed = levelConfig.obstaclePatrolSpeed_min;
            float _maxObstacleSpeed = levelIndex * levelConfig.obstaclePatrolSpeed_max_coef;
            float _obstacleChance = levelConfig.GetObstacleChance(levelIndex);

            int pipesCount = 0;

            int startRotatorLevel = 7;


            while (pipesCount < 100000)
            {

                var offset = new Vector3(Utils.Random(minPipeDistanceRandom.x, maxPipeDistanceRandom.x), Utils.Random(minPipeDistanceRandom.y, maxPipeDistanceRandom.y));
                offset += new Vector3(Utils.Random(perPipeIncreaseMin.x, perPipeIncreaseMax.x * pipesCount), Utils.Random(perPipeIncreaseMin.y, perPipeIncreaseMax.y) * pipesCount);
                offset += new Vector3(Utils.Random(perLevelIncreaseMin.x, perLevelIncreaseMax.x * levelIndex), Utils.Random(perLevelIncreaseMin.y, perLevelIncreaseMax.y) * levelIndex);
                offset.x = Mathf.Clamp(offset.x, -1000, maxPipeDistance.x);
                offset.x = Mathf.Clamp(offset.y, -1000, maxPipeDistance.y);


                Vector3 pos = _lastPipePostion + offset;
                pos.x = Mathf.Clamp(pos.x, _lastPipePostion.x + minPipeDistanceRandom.x, 1000);
                pos.y = Mathf.Clamp(pos.y, levelConfig.pipeBound_bottom, levelConfig.pipeBound_top);
                if (pipesCount == 0)
                {
                    pos.y = Mathf.Clamp(pos.y, _lastPipePostion.y + 5f, 10000);
                }
                if (_spawnObstacles)
                {
                    bool createObstacle = Random.value < _obstacleChance;
                    if (createObstacle)
                    {
                        var obsOffset = Vector3.right * 3f + Vector3.up * (-5f);
                        pos.x += obsOffset.x;
                        var obsPos = _lastPipePostion + (pos - _lastPipePostion) / 2f;
                        var prefab = Utils.RandomElement(_obstaclePrefabs);
                        var obs = Instantiate(prefab, transform);
                        obs.transform.position = obsPos;
                        var patrol = obs.GetComponent<PatrolBehaviour>();
                        patrol.SetPatrolOffset(new Vector3(0, Mathf.Clamp((pos - _lastPipePostion).y + Random.value * 4f, 4f, 1000), 0));
                        patrol.SetSpeed(Utils.Random(_minObtacleSpeed, _maxObstacleSpeed));
                        patrol.SetProgressOffset(Random.value);
                    }
                }

                if (pos.x >= levelLength - minPipeDistanceRandom.x / 2f)
                {
                    return;
                }

                pipePrefab = Utils.RandomElement(pipePrefabs);
                pipesCount++;
                // Vector3 pos = new Vector2(levelConfig.pipesStartSpawnFrom + i * levelConfig.metersPerPipe + Random.Range(-levelConfig.metersPerPipeThreshold, levelConfig.metersPerPipeThreshold),
                //    Random.Range(levelConfig.bottomBound + pipeSize.y * 0.5f, levelConfig.topBound - pipeSize.y * 0.5f));

                Pipe pipe = Instantiate(pipePrefab, _pipesParent);
                //float rotatorChance = 0.0f + levelIndex * 0.05f;
                //if(addRotator && Utils.Random(0,1f) <= rotatorChance)
                //{
                //    var rotator=pipe.gameObject.GetComponent<PipeRotator>();
                //    rotator.enabled = true;
                //    rotator.Init(5 + Utils.Random(-1f, 1f), (Utils.Random(0,1f)< 0.5f ? 1 : -1) * 90, 0.3f, Utils.Random(0, 1f));
                //}
                pipe.Spawn(pos);
                _lastPipePostion = pos;
                _pipes.Add(pipe);


            }
            bool addRotator = levelIndex >= startRotatorLevel;
            if (addRotator)
            {
                var rotatorPart = _pipes.Where(x => _pipes.IndexOf(x) > 0).ToList().RandomElement();
                if (rotatorPart)
                {
                    var rotator = rotatorPart.gameObject.GetComponent<PipeRotator>();
                    rotator.enabled = true;
                    rotator.Init(5 + Utils.Random(-1f, 1f), (Utils.Random(0, 1f) < 0.5f ? 1 : -1) * 90, 0.3f, Utils.Random(0, 1f));
                }

            }
        }
        private void SpawnCoins()
        {
            for (int i = 1; i < _pipes.Count; i++)
            {
                var pt1 = _pipes[i - 1].Entrances[1];
                var pt2 = _pipes[i].Entrances[0];
                float _distance = 0;
                Vector3 _lastPos = Vector3.zero;
                int count = 10;
                List<Coin> _coins = new List<Coin>();
                float jumpMultiplier = 1.7f;
                for (int j = 1; j < count; j++)
                {

                    float t = j * jumpMultiplier / count;
                    var pos = GameSettings.instance.GetTrajectoryPoint(pt1.transform.position, pt2.transform.position, t, jumpMultiplier);
                    if (j > 1)
                    {
                        if ((_lastPos - pos).magnitude < 0.7f)
                        {
                            continue;
                        }
                    }
                    var coin = Instantiate(_coinPrefab, transform);
                    _coins.Add(coin);
                    coin.transform.position = pos;

                    _lastPos = pos;
                }


            }
        }
        private void SetFinish()
        {
            _finish.transform.position = new Vector2(levelConfig.GetLevelLength(), 0);
        }
    }
}