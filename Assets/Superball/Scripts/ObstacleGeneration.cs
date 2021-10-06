using Superball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private int randomOption;
    private GameObject firstObstaclePos;
    private int previousRandomOption;
    private float counter;
    private IEnumerator _spawn;

    // Start is called before the first frame update
    void Start()
    {
        _spawn = Spawn();
        EventSignature();
    }

    private void EventSignature()
    {
        GameManager.instance.PlayPressed.AddListener(OnPlay);
        GameManager.instance.Lose.AddListener(DoLose);
    }

    private void OnPlay()
    {
        StartCoroutine(_spawn);
    }

    private void DoLose()
    {
        StopCoroutine(_spawn);
    }

    private void OnDestroy()
    {
        if (!GameManager.instance) return;
        GameManager.instance.PlayPressed.RemoveListener(OnPlay);
        GameManager.instance.Lose.RemoveListener(DoLose);
    }

    IEnumerator Spawn()
    {
        for (; ;)
        {
            if (counter !=2 && counter!=5)
                TopSpawn();

            if (counter == 2f)
                SideSpawn(-6f, -3.5f);

            if (counter == 5f)
                SideSpawn(6f, 3.5f);

            counter++;
            counter = counter >= 6 ? 0 : counter;

            yield return new WaitForSeconds(3.2f);
        }
    }

    private void TopSpawn()
    {
        randomOption = Random.Range(0, obstacles.Length);
        previousRandomOption = randomOption;

        firstObstaclePos = Instantiate(obstacles[randomOption], new Vector3(Random.Range(-0.4f, 0.4f), 6f, 2f), Quaternion.identity);

        while (randomOption == previousRandomOption)
            randomOption = Random.Range(0, obstacles.Length);

        if (Random.value < 0.5f)
        {
            Instantiate(obstacles[randomOption], new Vector3(firstObstaclePos.transform.position.x + 2.5f, 6f, 2f), Quaternion.identity);
        }
        else
        {
            Instantiate(obstacles[randomOption], new Vector3(firstObstaclePos.transform.position.x - 2.5f, 6f, 2f), Quaternion.identity);
        }
    }

    private void SideSpawn(float firstObstX, float secondObstX)
    {
        randomOption = Random.Range(0, obstacles.Length);
        previousRandomOption = randomOption;

        firstObstaclePos = Instantiate(obstacles[randomOption], new Vector3(firstObstX, Random.Range(2f, 3f), 2f), Quaternion.identity);

        while (randomOption == previousRandomOption)
            randomOption = Random.Range(0, obstacles.Length);

        Instantiate(obstacles[randomOption], new Vector3(secondObstX, firstObstaclePos.transform.position.y, 2f), Quaternion.identity);
    }
}
