using FriendsGamesTools;
using SplineMesh;
using Superball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Superball
{

    public class Pipe : MonoBehaviour
    {
        public Ball ball;

        [SerializeField] private Transform leftTube;
        [SerializeField] private Transform rightTube;
        [SerializeField] private Vector3 position;

        private SplineNode[] nodes;
        private Vector3[] nodesStartingPosition;
        private Vector3[] nodeStartingDirection;
        private GameObject currentSpline;
        [SerializeField] private Material[] materials;
        private int counter;

        private SuperballGeneralConfig _config => SuperballGeneralConfig.instance;
        private bool _randomSpline => _config.randomSpline;
        


        private void Start()
        {
            GetComponentInChildren<SplineMeshTiling>().material = materials[Random.Range(0, materials.Length)];
            /*counter = _config.indexSpline;*/
            GameManager.instance.LevelComplete.AddListener(DoLose);
            GetNewSplineNodes();
            UpdatePipe(position);
            /*Joystick.instance.Dragged += OnDragged;*/
        }

        private void ClearSpline()
        {
            Destroy(currentSpline, 1f);
        }

        private void DoLose()
        {
           /* Joystick.instance.Dragged -= OnDragged;*/
        }

        /*private void OnDestroy()
        {
            Joystick.instance.Dragged -= OnDragged;
            if (!GameManager.instance) return;
            GameManager.instance.LevelComplete.RemoveListener(ClearSpline);
        }*/

        private void Update()
        {
           /* if (!_randomSpline) return;*/
         //   ChangePipe();
        }

        /*private void OnDragged(Vector2 dir)
        {
            var position = pipe.position + Vector3.right*dir.x * _sensitivityTouch*Time.deltaTime;
            position.x= Mathf.Clamp(position.x, -maxOffset/2f, maxOffset/2f);
            pipe.position = position;
            UpdatePipe();
        }*/

        private void UpdatePipe(Vector3 position)
        {
            leftTube.position += new Vector3(position.x, -position.z, leftTube.position.z);
            rightTube.position += new Vector3(position.x, -position.z, leftTube.position.z);

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Position = position + new Vector3(nodesStartingPosition[i].x, nodesStartingPosition[i].y, nodesStartingPosition[i].z);
                nodes[i].Direction = position + new Vector3(nodeStartingDirection[i].x, nodeStartingDirection[i].y, nodeStartingDirection[i].z);
            }
        }

        /* public void GeneratePipe()
         {
             currentSpline = Instantiate(splines[counter], new Vector3(0f, 0f, 0f), Quaternion.Euler(90f, 0f, 0f),transform.parent);
             ball.spline = currentSpline.GetComponentInChildren<Spline>();

             GetNewSplineNodes();
         }*/

        /*public void OnThemeChanged(int index)
        {
            counter = index % materials.Length;// counter > splines.Length - 1 ? 0 : counter;
            Destroy(currentSpline);

            var prefab = Utils.RandomElement(splines);
            currentSpline = Instantiate(prefab, new Vector3(0f, prefab.transform.position.y, 0f), Quaternion.Euler(90f, 0f, 0f), transform.parent);
            currentSpline.GetComponentInChildren<SplineMeshTiling>().material = materials[counter];

            ball.spline = currentSpline.GetComponentInChildren<Spline>();

            GetNewSplineNodes();
            UpdatePipe();
        }*/
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

        private void GetNewSplineNodes()
        {
            nodes = GetComponentInChildren<Spline>().nodes.ToArray();

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