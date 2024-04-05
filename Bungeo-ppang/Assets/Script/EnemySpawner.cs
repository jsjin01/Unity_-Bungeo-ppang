using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool boss;
    Vector2 spawnPos;     //���� ����Ʈ ����
    Quaternion rotation = Quaternion.Euler(0, 0, 0);  //�����Ҷ� �����Ǵ� ��������
    int stageEnemies = 5;  //������������ ������ ���ͷ� / �ʱⰪ:5
    int stagePlus = 3; //�������� �� ���� �þ�� �� 3/4/5/6/7
    int spawnnum = 0; //�ѹ��� �����Ǵ� ��
    int stage = 1; //���� ��������

    bool isStage = false; //�������� �����ߴ��� �Ǵ� ����
    /*�������� �����
     * �ѹ��� 1~2������ ����(2�� ����)
     * ������ ���� �� ä��� 10�� ��� �� ���� �������� ����
     */
    void Start()
    {
        if (boss) //���� ����
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
            float randomX = Random.Range(-2.3f, 2.3f);      //x������ ������ �κ����� ����
            spawnPos = new Vector2(randomX, 5.7f);
            EnemyPoolManager.i.UseEnemy(spawnPos, rotation);
        }
    }

    IEnumerator StageSpawn(int num)
    {
        UIManager.i.SetStage(stage);
        Debug.Log(stage + "�������� ����");
        while (true)
        {
            while (true)
            {
                spawnnum = Mathf.RoundToInt(Random.Range(1f, 2f)); //1,2 �� �ϳ� ��ȯ 
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
        Debug.Log("�������� ��");
        yield return new WaitForSeconds(10f);
        CardManager.i.CardDraw();
        Time.timeScale = 0f;
        //�������������� �°� ����
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

