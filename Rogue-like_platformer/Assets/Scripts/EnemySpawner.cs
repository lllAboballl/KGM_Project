using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject[] enemy;

    Vector3 spawnPosition;

    int randomIndex;

    public void SpawnEnemy(int enemyIndex)
    {
        GeneratePosition();
        Instantiate(enemy[enemyIndex], spawnPosition, Quaternion.identity);
    }

    void GeneratePosition()
    {
        List<Vector3> positionList = new();

        for (int i = 0; i < spawnPositions.Length;) 
        { 
            positionList.Add(spawnPositions[i].position);
            i++;
        }

        randomIndex = Random.Range(0, positionList.Count);

        spawnPosition = positionList[randomIndex];
    }
} 