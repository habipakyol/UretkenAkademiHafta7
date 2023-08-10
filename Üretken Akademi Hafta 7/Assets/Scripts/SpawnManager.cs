using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject CoinPrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawnPosCoin = new Vector3(25, 5, 0);
    public float start;
    public float repeat;
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private List<GameObject> spawnedCoins = new List<GameObject>();
    private PlayerController playerControllerScript;

    private void Start()
    {
        InvokeRepeating("SpawnObstacle", start, repeat);
        InvokeRepeating("SpawnCoin", start, repeat);
        playerControllerScript = FindObjectOfType<PlayerController>();
    }
    private void SpawnCoin()
    {
        if (!playerControllerScript.gameover)
        {
            GameObject newCoin = Instantiate(CoinPrefab, spawnPosCoin, CoinPrefab.transform.rotation);
            spawnedCoins.Add(newCoin);
        }
    }

    private void SpawnObstacle()
    {
        if (!playerControllerScript.gameover)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            GameObject newObstacle = Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
            spawnedObstacles.Add(newObstacle);
        }
    }

    private void Update()
    {
        CheckSpawnedObjects(); 
    }

    private void CheckSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObstacles)
        {
            if (obj == null)
            {
                spawnedObstacles.Remove(obj);
                break;
            }
        }

        foreach (GameObject obj in spawnedCoins)
        {
            if (obj == null)
            {
                spawnedCoins.Remove(obj);
                break;
            }
        }
    }
}
