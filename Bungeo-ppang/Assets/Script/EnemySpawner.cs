using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner i;

    [SerializeField] bool boss;
    Vector2 spawnPos;                                 //���� ����Ʈ ����
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //�����Ҷ� �����Ǵ� ��������
    int spawnnum = 0;                                 //�ѹ��� �����Ǵ� ��
    public int stage = 1;                             //���� ��������
    public float E1mul = 1;
    public float E2mul = 1;

    float spawnTime1 = 1.5f;
    float spawnTime2 = 2f;

    List<int> unit_1 = new List<int> {5, 8, 15, 12, 15, 18, 0, 0, 0, 0 };
    List<int> unit_2 = new List<int> { 0, 0, 0, 8, 15, 22, 40, 45, 50, 0 };

    bool isStage = false; //�������� �����ߴ��� �Ǵ� ����
    /*�������� �����
     * �ѹ��� 1~2������ ����(2�� ����)
     * ������ ���� �� ä��� 10�� ��� �� ���� �������� ����
     */

    [SerializeField] GameObject bossMonster;
    public bool isFever = false;
    bool gameEnd = false;
    IEnumerator stageCor; //�������� �ڷ�ƾ
    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        PlayerManager.i.gameEnd += () =>
        {
            gameEnd = true;
            //�ڷ�ƾ ����
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
        //�������� ����
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
            float randomX = Random.Range(-2.3f, 2.3f);      //x������ ������ �κ����� ����
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
    //IEnumerator StageSpawn() //��������
    //{
    //    UIManager.i.SetStage(stage);
    //    Debug.Log(stage + "�������� ����");
    //    int e1total = unit_1[stage - 1]; // ������������ ��ȯ�� �� e1 ������ ��
    //    int e2total = unit_2[stage - 1]; // ������������ ��ȯ�� �� e2 ������ ��
    //    Debug.Log("�� e1 ����: " + e1total);
    //    Debug.Log("�� e2 ����: " + e2total);

    //    if (stage == 10)
    //    {
    //        SpawnBoss();
    //        yield return null;
    //    }

    //    while (e1total > 0 || e2total > 0)
    //    {
    //        int spawnCount = 0;
    //        int spawnType = 1; // �ʱ� ���� Ÿ���� 1�� �����մϴ�.

    //        // �����ϰ� e1num�� e2num �� �ϳ��� �����Ͽ� ���� �����մϴ�.
    //        if (Random.Range(0, 2) == 0 && e1total > 0)
    //        {
    //            // e1 ���� ����
    //            spawnCount = Mathf.Min(e1total, Random.Range(1, 3)); // �ִ� 2���������� �����մϴ�.
    //            spawnType = 1;
    //        }
    //        else if (e2total > 0)
    //        {
    //            // e2 ���� ����
    //            spawnCount = Mathf.Min(e2total, Random.Range(1, 3)); // �ִ� 2���������� �����մϴ�.
    //            spawnType = 2;
    //        }

    //        // �����ִ� ������ ���� �����Ǵ� ������ ������ ���� ��쿡�� ���������� ó���˴ϴ�.
    //        if (spawnCount > 0)
    //        {
    //            // ����
    //            SpawnEnemies(spawnCount, spawnType);

    //            // �ش��ϴ� ���� �� ����
    //            if (spawnType == 1)
    //                e1total -= spawnCount;
    //            else
    //                e2total -= spawnCount;

    //            yield return new WaitForSeconds(2f);
    //        }
    //        else
    //        {
    //            // �����ִ� ������ ���� �����Ǵ� ������ ������ ���� ���
    //            // �ݺ��� �ߴ��մϴ�.
    //            break;
    //        }
    //    }

    //    Debug.Log("�������� ��");
    //    yield return new WaitForSeconds(3f);
    //    CardManager.i.CardDraw();
    //    Time.timeScale = 0f;
    //    //�������������� �°� ����
    //    stage += 1;
    //    isStage = false;
    //}


    IEnumerator StageSpawn() //��������
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "�������� ����");
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
            if (e1num != 0) //���� ���� 0�� �ƴҶ���
            {
                while (true)
                {
                    spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 �� �ϳ� ��ȯ 
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
                    SpawnEnemies(spawnnum, 1);  //��ȯ
                    yield return new WaitForSeconds(spawnTime1);
                    Debug.Log(e1num);
                }
            }

            if (e2num != 0)//���� ���� 0�� �ƴҶ���
            {
                while (true)
                {
                    spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 �� �ϳ� ��ȯ 
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
                    SpawnEnemies(spawnnum, 2);  //��ȯ
                    yield return new WaitForSeconds(spawnTime2);
                }
            }
            if (e1num == 0 && e2num == 0)
            {
                break; //�������� ���� 
            }
        }
        Debug.Log("�������� ��");
        yield return new WaitForSeconds(3f);
        CardManager.i.CardDraw();
        Time.timeScale = 0f;
        //�������������� �°� ����
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

