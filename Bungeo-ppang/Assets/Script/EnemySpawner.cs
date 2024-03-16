using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // ������ �� ������
    private int numberOfEnemies = 5; // ������ ���� ��
    private float spawnInterval = 1f; // �� ���� ����

    void Start()
    {
        Invoke("SpawnEnemies", spawnInterval);      //1�ʸ��� �� ����
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
