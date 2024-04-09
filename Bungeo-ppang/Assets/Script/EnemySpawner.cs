using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool boss;
    Vector2 spawnPos;                                 //스폰 포인트 지정
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //스폰할때 생성되는 각도지정
    int spawnnum = 0;                                 //한번에 스폰되는 양
    int stage = 10;                                    //현재 스테이지

    List<int> unit_1 = new List<int> {5, 8, 15, 12, 15, 18, 0, 0, 0, 0 };
    List<int> unit_2 = new List<int> { 0, 0, 0, 8, 15, 22, 40, 45, 50, 0 };

    bool isStage = false; //스테이지 시작했는지 판단 변수
    /*스테이지 진행시
     * 한번에 1~2마리씩 스폰(2초 단위)
     * 정해진 수를 다 채우면 10초 대기 후 다음 스테이지 시작
     */

    public bool isFever = false;
    bool gameEnd = false;
    IEnumerator stageCor; //스테이지 코루틴
    void Start()
    {
        PlayerManager.i.gameEnd += () =>
        {
            gameEnd = true;
            //코루틴 정지
            if(stageCor != null)
            {
                StopCoroutine(stageCor);
            }

        };
        if (boss) //보스 여부
        {
            SpawnBoss();
        }
    }
    private void Update()
    {
        if (gameEnd)
        {
            return;
        }
        //스테이지 관리
        if (boss)
        {
            isStage = true;
        }
        else if (!isStage)
        {
            isStage = true;
            if(stage != 10)
            {
                stageCor = StageSpawn();
                StartCoroutine(stageCor);
            }
            else if (stage == 10)
            {
                UIManager.i.SetStage(stage);
                SpawnBoss();
            }
               
        }

    }


    void SpawnEnemies(int num, int t)
    {
        for (int i = 0; i < num; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);      //x축으로 랜덤한 부분으로 스폰
            spawnPos = new Vector2(randomX, 5.7f);
            if(t == 1)
            {
                EnemyPoolManager.i.UseEnemy1(spawnPos, rotation);
            }
            else if(t == 2)
            {
                EnemyPoolManager.i.UseEnemy2(spawnPos, rotation);
            }
        }
    }
    IEnumerator StageSpawn() //스테이지
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "스테이지 시작");
        int e1num = unit_1[stage - 1];
        int e2num = unit_2[stage - 1];
        Debug.Log(e1num);
        if(stage == 10)
        {
            SpawnBoss();
            yield return new WaitForSeconds(0) ;
        }
        while (true)
        {
            if(e1num != 0) //몬스터 수가 0이 아닐때만
            {
                while (true)
                {
                    spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 중 하나 소환 
                    e1num -= spawnnum;
                    if (e1num >= 0)
                    {
                        break;
                    }
                    else
                    {
                        e1num += spawnnum;
                        continue;
                    }
                }
                if (spawnnum >= 1)
                {
                    SpawnEnemies(spawnnum, 1);  //소환
                    yield return new WaitForSeconds(2f);
                    Debug.Log(e1num);
                }
            }

            if(e2num != 0)//몬스터 수가 0이 아닐때만
            {
                while (true)
                {
                    spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 중 하나 소환 
                    e2num -= spawnnum;
                    if (e2num >= 0)
                    {
                        break;
                    }
                    else
                    {
                        e2num += spawnnum;
                        continue;
                    }
                }

                if (spawnnum >= 1)
                {
                    SpawnEnemies(spawnnum, 2);  //소환
                    yield return new WaitForSeconds(2f);
                }
            }
            if (e1num == 0 && e2num == 0)
            {
                break; //스테이지 종료 
            }
        }
        Debug.Log("스테이지 끝");
        yield return new WaitForSeconds(3f);
        CardManager.i.CardDraw();
        Time.timeScale = 0f;
        //다음스테이지에 맞게 조정
        stage += 1;
        isStage = false;
    }

    void SpawnBoss()
    {
        EnemyPoolManager.i.CreatBoss();
    }
}

