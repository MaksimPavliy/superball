using FriendsGamesTools;
using SplineMesh;
using Superball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{

    public class Pipe : MonoBehaviour
    {
        public Ball ball;

        [SerializeField] private float speed;
        [SerializeField] private Transform pipe;

        private SplineNode[] nodes;
        private Vector3[] nodesStartingPosition;
        private Vector3[] nodeStartingDirection;
        private GameObject currentSpline;
        [SerializeField] private GameObject[] splines;
        [SerializeField] private Material[] materials;
        private int counter;

        private SuperballGeneralConfig _config => SuperballGeneralConfig.instance;
        private bool _randomSpline => _config.randomSpline;
        private float _sensitivityTouch => _config.sensitivityTouch;

        public float maxOffset = 5.4f;
        private void Start()
        {
            counter = _config.indexSpline;
            GeneratePipe();
            GameManager.instance.LevelComplete.AddListener(DoLose);
            Joystick.instance.Dragged += OnDragged;
        }

        private void ClearSpline()
        {
               Destroy(currentSpline, 1f);
        }

        private void DoLose()
        {
            Joystick.instance.Dragged -= OnDragged;
        }
        private void OnDestroy()
        {
            Joystick.instance.Dragged -= OnDragged;
            if (!GameManager.instance) return;
            GameManager.instance.LevelComplete.RemoveListener(ClearSpline);
        }

        private void Update()
        {
            if (!_randomSpline) return;
         //   ChangePipe();
        }

        
        private void OnDragged(Vector2 dir)
        {
            var position = pipe.position + Vector3.right*dir.x * _sensitivityTouch*Time.deltaTime;
            position.x= Mathf.Clamp(position.x, -maxOffset/2f, maxOffset/2f);
            pipe.position = position;
            UpdatePipe();
        }
        private void UpdatePipe()
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Position = pipe.position + new Vector3(nodesStartingPosition[i].x, nodesStartingPosition[i].y, nodesStartingPosition[i].z);
                nodes[i].Direction = pipe.position + new Vector3(nodeStartingDirection[i].x, nodeStartingDirection[i].y, nodeStartingDirection[i].z);
            }
        }
        public void GeneratePipe()
        {
            currentSpline = Instantiate(splines[counter], new Vector3(0f, 0f, 0f), Quaternion.Euler(90f, 0f, 0f),transform.parent);
            ball.spline = currentSpline.GetComponentInChildren<Spline>();

            GetNewSplineNodes();
        }

        public void OnThemeChanged(int index)
        {
            counter = index % materials.Length;// counter > splines.Length - 1 ? 0 : counter;
            Destroy(currentSpline);

            var prefab = Utils.RandomElement(splines);
            currentSpline = Instantiate(prefab, new Vector3(0f, prefab.transform.position.y, 0f), Quaternion.Euler(90f, 0f, 0f), transform.parent);
            currentSpline.GetComponentInChildren<SplineMeshTiling>().material = materials[counter];

            ball.spline = currentSpline.GetComponentInChildren<Spline>();

            GetNewSplineNodes();
            UpdatePipe();
        }
        //public void ChangePipe()
        //{
        //    if (ball.jumpCounter % 3 != 0)
        //    {
        //        changed = false;
        //    }

        //    if (ball.jumpCounter % 3 == 0 && changed == false)
        //    {
        //        counter++;
        //        counter = counter > splines.Length - 1 ? 0 : counter;
        //        changed = true;

        //        Destroy(currentSpline);

        //        currentSpline = Instantiate(splines[counter], new Vector3(0f, splines[counter].transform.position.y, 0f), Quaternion.Euler(90f, 0f, 0f));
        //        ball.spline = currentSpline.GetComponentInChildren<Spline>();

        //        GetNewSplineNodes();
        //    }
        //}

        public void GetNewSplineNodes()
        {
            nodes = currentSpline.GetComponentInChildren<Spline>().nodes.ToArray();

            nodesStartingPosition = new Vector3[nodes.Length];
            nodeStartingDirection = new Vector3[nodes.Length];

            for (int i = 0; i < nodes.Length; i++)
            {
                nodesStartingPosition[i] = nodes[i].Position;
                nodeStartingDirection[i] = nodes[i].Direction;
            }
        }
    }
}   