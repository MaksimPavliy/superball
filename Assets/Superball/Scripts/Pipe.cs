using SplineMesh;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Superball
{
    public class Pipe : MonoBehaviour
    {
        public Ball ball;

        [SerializeField] private PipeEntrance[] entrances;

        [SerializeField] private Vector3 position;
        private GameObject currentSpline;
        [SerializeField] private Material[] materials;
        [SerializeField] private Spline _spline;
        private int counter;

        private SuperballGeneralConfig _config => SuperballGeneralConfig.instance;

        public CurveSample GetSampleAtDistance(float distance)=>_spline.GetSampleAtDistance(distance);
        public Vector3 GetSampleWorldPosition(CurveSample sample) =>transform.TransformPoint(sample.location);
        public Vector3 GetSampleWorldDirection(CurveSample sample) => transform.TransformDirection(sample.tangent.normalized);
        public float Length=>_spline.Length;

        public void HideEntrance()
        {
            foreach (var item in entrances)
            {

            }
        }

        public void ShowEntrance(float delay = 0)
        {

        }
    
        private void Start()
        {
            GetComponentInChildren<SplineMeshTiling>().material = materials[Random.Range(0, materials.Length)];
            /*counter = _config.indexSpline;*/
            /*Joystick.instance.Dragged += OnDragged;*/
        }

        private void OnValidate()
        {
            if (_spline == null) _spline = GetComponentInChildren<Spline>();

        }
        private void ClearSpline()
        {
            Destroy(currentSpline, 1f);
        }
        private void Update()
        {
        }

        public void Spawn(Vector3 position)
        {
            transform.position = new Vector3(position.x, 0, -position.y);

        }

       
    }
}