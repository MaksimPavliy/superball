using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Superball
{
    public class PipeRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationInterval = 1;
        [SerializeField] private float _rotationTime = 0.3f;
        [SerializeField] private float _rotationAngle = 90;
        [SerializeField] private float _timeOffset = 0;
        public void Init(float interval,float angle,float rotationTime,float timeOffset=0)
        {
            _rotationInterval=interval;
            _rotationAngle = angle;
            _rotationTime = rotationTime;
             _timeOffset =timeOffset;
        }
        private void Start()
        {
            StartCoroutine(RotationRoutine());
        }
        private IEnumerator RotationRoutine()
        {
            yield return new WaitForSeconds(_timeOffset * _rotationInterval);
            while (true)
            {
                yield return new WaitForSeconds(_rotationInterval);
                transform.DORotateQuaternion(transform.rotation * Quaternion.AngleAxis(_rotationAngle, Vector3.up), _rotationTime);
            }
        }
    }
}