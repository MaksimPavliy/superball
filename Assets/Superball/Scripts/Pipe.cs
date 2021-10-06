using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Ball ball;

    [SerializeField] private float speed;
    [SerializeField] private Transform pipe;

    private SplineNode[] nodes;
    private Vector3[] nodesStartingPosition;
    private Vector3[] nodeStartingDirection;
    private bool changed = true;
    private GameObject currentSpline;
    [SerializeField] private GameObject[] splines;
    private int counter;

    private void Start()
    {   
        GeneratePipe();
    }

    private void Update()
    {
        ChangePipe();
    }

    private void OnMouseDrag()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.x = clickPosition.x > 1 ? 1 : clickPosition.x;
        clickPosition.x = clickPosition.x < -1 ? -1 : clickPosition.x;

        pipe.position = Vector2.MoveTowards(pipe.position, new Vector2(clickPosition.x, pipe.position.y), speed * Time.deltaTime);

        for (int i = 0; i < nodes.Length; i++)
        {
          /*  transform.TransformPoint(nodes[i].Position);*/
          /*  transform.TransformDirection(nodes[i].Direction);*/
            nodes[i].Position = Vector3.MoveTowards(nodes[i].Position, new Vector3(pipe.position.x + nodesStartingPosition[i].x, nodes[i].Position.y, nodes[i].Position.z), speed * Time.deltaTime);
            nodes[i].Direction = Vector3.MoveTowards(nodes[i].Direction, new Vector3(pipe.position.x + nodeStartingDirection[i].x, nodes[i].Direction.y, nodes[i].Direction.z), speed * Time.deltaTime);
        }
    }

    public void GeneratePipe()
    {
        currentSpline = Instantiate(splines[counter], new Vector3(0f, 0f, 0f), Quaternion.Euler(90f, 0f, 0f));
        ball.spline = currentSpline.GetComponentInChildren<Spline>();

        GetNewSplineNodes();       
    }

    public void ChangePipe()
    {
        if (ball.jumpCounter % 3 != 0)
        {
            changed = false;
        }

        if(ball.jumpCounter % 3 == 0 && changed == false)
        {
            counter++;
            counter = counter > splines.Length - 1 ? 0 : counter;
            changed = true;

            Destroy(currentSpline);

            currentSpline = Instantiate(splines[counter], new Vector3(0f, splines[counter].transform.position.y, 0f), Quaternion.Euler(90f, 0f, 0f));
            ball.spline = currentSpline.GetComponentInChildren<Spline>();

            GetNewSplineNodes();
        }
    }

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
    