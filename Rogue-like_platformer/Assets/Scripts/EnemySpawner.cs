using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawnPositions;

    Vector3 spawnPosition;

    int randomIndex;

    public void SpawnEnemy()
    { 
        GenerateList(); 
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void GenerateList()
    {

        List<Vector3> positionList = new();
        /*{

            spawnPositions[1].position, 
            
        };*/
        for (int i = 0; i < spawnPositions.Length;) 
        { 
            positionList.Add(spawnPositions[i].position);
            i++;
        }

        randomIndex = Random.Range(0, positionList.Count);

        spawnPosition = positionList[randomIndex];
    }
} 