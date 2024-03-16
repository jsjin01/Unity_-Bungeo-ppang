using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // 积己且 利 橇府普
    private int numberOfEnemies = 5; // 积己且 利狼 荐
    private float spawnInterval = 1f; // 利 积己 埃拜

    void Start()
    {
        Invoke("SpawnEnemies", spawnInterval);      //1檬付促 利 积己
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);
            
            GameObject newEnemy = Instantiate(enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
            
            newEnemy.AddComponent<EnemyMoveControl>();
        }

        Invoke("SpawnEnemies", spawnInterval);
    }
}
