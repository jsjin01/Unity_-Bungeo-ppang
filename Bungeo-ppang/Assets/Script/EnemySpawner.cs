using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // ������ �� ������
    [SerializeField]
    private int numberOfEnemies = 5;
    private float spawnInterval = 3f; // �� ���� ����
    //int initEnemyCount = 20;
    //private float time;     //��� �ð�
    public List<GameObject> activeEnemies = new List<GameObject>(); // Ȱ��ȭ�� ���� �����ϴ� ����Ʈ


    void Start()
    {
        /*for(int i = 0; i < initEnemyCount; i++)
        {
            SpawnEnemies();
        }*/
        InvokeRepeating("SpawnEnemyBatch", spawnInterval, spawnInterval);
    }

    void Update()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            // ���� ���� ��ġ ���Ϸ� �����Դٸ� ��Ȱ��ȭ�ϰ� ����Ʈ���� ����
            if (activeEnemies[i].transform.position.y < -6f)
            {
                activeEnemies[i].SetActive(false);
                activeEnemies.RemoveAt(i);
                i--; // ����Ʈ ��Ұ� �ϳ� ���ŵǾ����Ƿ� �ε����� �ϳ� �ٿ���
            }
            
            if (activeEnemies.Count == 0)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemyBatch()
    {
        // 5������ ����
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        
            float randomX = Random.Range(-2.3f, 2.3f);

            GameObject newEnemy = Instantiate(enemy, new Vector3(randomX, 5.7f, 0f), Quaternion.identity);

            //newEnemy.AddComponent<EnemyMoveControl>();
            activeEnemies.Add(newEnemy); // ����Ʈ�� �߰�
        
    }


}


