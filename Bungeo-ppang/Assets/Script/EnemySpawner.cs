using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Vector2 spawnPos;     //스폰 포인트 지정
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //스폰할때 생성되는 각도지정
    int numberOfEnemies = 5;//한번에 스폰될 양
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2, 2);
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);      //x축으로 랜덤한 부분으로 스폰
            spawnPos = new Vector2(randomX, 5.7f);
            EnemyPoolManager.i.UseEnemy(spawnPos, rotation);
        }
    }
}

