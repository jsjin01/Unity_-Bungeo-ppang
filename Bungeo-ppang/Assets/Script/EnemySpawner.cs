using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // 생성할 적 프리팹
    [SerializeField]
    private int numberOfEnemies = 5;
    private float spawnInterval = 3f; // 적 생성 간격
    //int initEnemyCount = 20;
    //private float time;     //경과 시간
    public List<GameObject> activeEnemies = new List<GameObject>(); // 활성화된 적을 저장하는 리스트


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
            // 적이 일정 위치 이하로 내려왔다면 비활성화하고 리스트에서 제거
            if (activeEnemies[i].transform.position.y < -6f)
            {
                activeEnemies[i].SetActive(false);
                activeEnemies.RemoveAt(i);
                i--; // 리스트 요소가 하나 제거되었으므로 인덱스를 하나 줄여줌
            }
            
            if (activeEnemies.Count == 0)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemyBatch()
    {
        // 5마리씩 생성
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
            activeEnemies.Add(newEnemy); // 리스트에 추가
        
    }


}


