using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool boss;
    Vector2 spawnPos;                                 //���� ����Ʈ ����
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //�����Ҷ� �����Ǵ� ��������
    int spawnnum = 0;                                 //�ѹ��� �����Ǵ� ��
    int stage = 10;                                    //���� ��������

    List<int> unit_1 = new List<int> {5, 8, 15, 12, 15, 18, 0, 0, 0, 0 };
    List<int> unit_2 = new List<int> { 0, 0, 0, 8, 15, 22, 40, 45, 50, 0 };

    bool isStage = false; //�������� �����ߴ��� �Ǵ� ����
    /*�������� �����
     * �ѹ��� 1~2������ ����(2�� ����)
     * ������ ���� �� ä��� 10�� ��� �� ���� �������� ����
     */

    public bool isFever = false;
    bool gameEnd = false;
    IEnumerator stageCor; //�������� �ڷ�ƾ
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
        if (boss) //���� ����
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
        //�������� ����
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
            float randomX = Random.Range(-2.3f, 2.3f);      //x������ ������ �κ����� ����
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
    IEnumerator StageSpawn() //��������
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "�������� ����");
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
            if(e1num != 0) //���� ���� 0�� �ƴҶ���
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
                    yield return new WaitForSeconds(2f);
                    Debug.Log(e1num);
                }
            }

            if(e2num != 0)//���� ���� 0�� �ƴҶ���
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
                    yield return new WaitForSeconds(2f);
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
        isStage = false;
    }

    void SpawnBoss()
    {
        EnemyPoolManager.i.CreatBoss();
    }
}

