using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool boss;
    Vector2 spawnPos;     //스폰 포인트 지정
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //스폰할때 생성되는 각도지정
    int stageEnemies = 5;  //스테이지마다 나오는 몬스터량 / 초기값:5
    int stagePlus = 3; //스테이지 갈 수록 늘어나는 양 3/4/5/6/7
    int spawnnum = 0; //한번에 스폰되는 양
    int stage = 1; //현재 스테이지

    bool isStage = false; //스테이지 시작했는지 판단 변수
    /*스테이지 진행시
     * 한번에 1~2마리씩 스폰(2초 단위)
     * 정해진 수를 다 채우면 10초 대기 후 다음 스테이지 시작
     */
    void Start()
    {
        if (boss) //보스 여부
        {
            SpawnBoss();
        }
    }
    private void Update()
    {
        if (boss)
        {
            isStage = true;
        }
        else if (!isStage)
        {
            isStage = true;
            StartCoroutine(StageSpawn(stageEnemies));

        }

    }


    void SpawnEnemies(int num)
    {
        for (int i = 0; i < num; i++)
        {
            float randomX = Random.Range(-2.3f, 2.3f);      //x축으로 랜덤한 부분으로 스폰
            spawnPos = new Vector2(randomX, 5.7f);
            EnemyPoolManager.i.UseEnemy(spawnPos, rotation);
        }
    }

    IEnumerator StageSpawn(int num)
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "스테이지 시작");
        while (true)
        {
            while (true)
            {
                spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 중 하나 소환 
                num -= spawnnum;
                if (num >= 0)
                {
                    break;
                }
                else
                {
                    num += spawnnum;
                    continue;
                }
            }


            SpawnEnemies(spawnnum);
            yield return new WaitForSeconds(2f);

            if (num <= 0)
            {
                break;
            }
        }
        Debug.Log("스테이지 끝");
        yield return new WaitForSeconds(10f);
        CardManager.i.CardDraw();
        Time.timeScale = 0f;
        //다음스테이지에 맞게 조정
        stage += 1;
        stageEnemies = stageEnemies + stagePlus;
        stagePlus += 1;
        isStage = false;
    }

    void SpawnBoss()
    {
        EnemyPoolManager.i.CreatBoss();
    }
}

