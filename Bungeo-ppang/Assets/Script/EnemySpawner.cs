using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Vector2 spawnPos;     //���� ����Ʈ ����
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //�����Ҷ� �����Ǵ� ��������
    int numberOfEnemies = 5;//�ѹ��� ������ ��
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2, 2);
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);      //x������ ������ �κ����� ����
            spawnPos = new Vector2(randomX, 5.7f);
            EnemyPoolManager.i.UseEnemy(spawnPos, rotation);
        }
    }
}

