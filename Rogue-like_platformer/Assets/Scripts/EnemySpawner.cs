using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    
    Vector3 spawnPosition;

    int randomIndex;

    public void SpawnEnemy() 
    { 
        GenerateList(); 
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
 
    void GenerateList()
    {
        List<Vector3> positionList = new()
        {
            new Vector3(51, -1.8f, 0),
            new Vector3(52, -11.4f, 0),
            new Vector3(82, 11.4f, 0),
        };

        randomIndex = Random.Range(0, positionList.Count);

        spawnPosition = positionList[randomIndex];

    }
    

}