using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner i;

    [SerializeField] bool boss;
    Vector2 spawnPos;                                 //스폰 포인트 지정
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //스폰할때 생성되는 각도지정
    int spawnnum = 0;                                 //한번에 스폰되는 양
    public int stage = 1;                             //현재 스테이지
    public float E1mul = 1;
    public float E2mul = 1;

    float spawnTime1 = 1.5f;
    float spawnTime2 = 2f;

    List<int> unit_1 = new List<int> {5, 8, 15, 12, 15, 18, 0, 0, 0, 0 };
    List<int> unit_2 = new List<int> { 0, 0, 0, 8, 15, 22, 40, 45, 50, 0 };

    bool isStage = false; //스테이지 시작했는지 판단 변수
    /*스테이지 진행시
     * 한번에 1~2마리씩 스폰(2초 단위)
     * 정해진 수를 다 채우면 10초 대기 후 다음 스테이지 시작
     */

    [SerializeField] GameObject bossMonster;
    public bool isFever = false;
    bool gameEnd = false;
    IEnumerator stageCor; //스테이지 코루틴
    private void Awake()
    {
        i = this;
    }
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
            if(bossMonster != null)
            {
                bossMonster.SetActive(true);
            }
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
                bossMonster.SetActive(true);
            }
               
        }

    }


    void SpawnEnemies(int num, int t)
    {
        for (int i = 0; i < num; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);      //x축으로 랜덤한 부분으로 스폰
            spawnPos = new Vector2(randomX, 5.5f);
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
    //IEnumerator StageSpawn() //스테이지
    //{
    //    UIManager.i.SetStage(stage);
    //    Debug.Log(stage + "스테이지 시작");
    //    int e1total = unit_1[stage - 1]; // 스테이지에서 소환될 총 e1 몬스터의 양
    //    int e2total = unit_2[stage - 1]; // 스테이지에서 소환될 총 e2 몬스터의 양
    //    Debug.Log("총 e1 몬스터: " + e1total);
    //    Debug.Log("총 e2 몬스터: " + e2total);

    //    if (stage == 10)
    //    {
    //        SpawnBoss();
    //        yield return null;
    //    }

    //    while (e1total > 0 || e2total > 0)
    //    {
    //        int spawnCount = 0;
    //        int spawnType = 1; // 초기 스폰 타입은 1로 설정합니다.

    //        // 랜덤하게 e1num과 e2num 중 하나를 선택하여 적을 스폰합니다.
    //        if (Random.Range(0, 2) == 0 && e1total > 0)
    //        {
    //            // e1 몬스터 스폰
    //            spawnCount = Mathf.Min(e1total, Random.Range(1, 3)); // 최대 2마리까지만 스폰합니다.
    //            spawnType = 1;
    //        }
    //        else if (e2total > 0)
    //        {
    //            // e2 몬스터 스폰
    //            spawnCount = Mathf.Min(e2total, Random.Range(1, 3)); // 최대 2마리까지만 스폰합니다.
    //            spawnType = 2;
    //        }

    //        // 남아있는 몬스터의 수가 스폰되는 몬스터의 수보다 적을 경우에도 정상적으로 처리됩니다.
    //        if (spawnCount > 0)
    //        {
    //            // 스폰
    //            SpawnEnemies(spawnCount, spawnType);

    //            // 해당하는 적의 수 차감
    //            if (spawnType == 1)
    //                e1total -= spawnCount;
    //            else
    //                e2total -= spawnCount;

    //            yield return new WaitForSeconds(2f);
    //        }
    //        else
    //        {
    //            // 남아있는 몬스터의 수가 스폰되는 몬스터의 수보다 적은 경우
    //            // 반복을 중단합니다.
    //            break;
    //        }
    //    }

    //    Debug.Log("스테이지 끝");
    //    yield return new WaitForSeconds(3f);
    //    CardManager.i.CardDraw();
    //    Time.timeScale = 0f;
    //    //다음스테이지에 맞게 조정
    //    stage += 1;
    //    isStage = false;
    //}


    IEnumerator StageSpawn() //스테이지
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "스테이지 시작");
        int e1num = unit_1[stage - 1];
        int e2num = unit_2[stage - 1];
        Debug.Log(e1num);
        if (stage == 10)
        {
            SpawnBoss();
            yield return new WaitForSeconds(0);
        }
        while (true)
        {
            if (e1num != 0) //몬스터 수가 0이 아닐때만
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
                    yield return new WaitForSeconds(spawnTime1);
                    Debug.Log(e1num);
                }
            }

            if (e2num != 0)//몬스터 수가 0이 아닐때만
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
                    yield return new WaitForSeconds(spawnTime2);
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
        E1mul *= 1.1f;
        if(stage > 4)
        {
            E2mul *= 1.1f;
        }
        spawnTime1 *= 0.97f;
        spawnTime2 *= 0.97f;
        isStage = false;
    }

    void SpawnBoss()
    {
        EnemyPoolManager.i.CreatBoss();
    }
}

