using FriendsGamesTools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    [Serializable]
    public class BackgroundLayer
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _layerParent;
        [SerializeField] private float _offsetMultiplier = 1;
        [SerializeField] private float _minDelta = 5;
        [SerializeField] private float _maxDelta = 10;
        [SerializeField] private int _count = 30;
        public void Create()
        {
            Vector3 _lastPos = Vector3.zero+Vector3.left * Utils.Random(_minDelta, _maxDelta)*3f ;
            for (int i = 0; i < _count; i++)
            {
                var g = GameObject.Instantiate(_prefab, _layerParent);
                g.transform.localPosition = _lastPos;
                g.transform.localScale *= Utils.Random(0.9f, 1.3f);
                _lastPos += Vector3.right * Utils.Random(_minDelta, _maxDelta);
            }
        }
        public void Update(Vector3 offset)
        {
            _layerParent.transform.position += Vector3.right * offset.x * _offsetMultiplier;
        }
    }
    public class BackgroundGenerator: MonoBehaviour
    {
        [SerializeField] private List<BackgroundLayer> _layers;
        private void Start()
        {
            foreach (var layer in _layers)
            {
                layer.Create();
            }
        }
    }
}