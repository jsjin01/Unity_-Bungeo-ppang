using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;
    [SerializeField] public float speed = 2f; // ���� �̵� �ӵ�
    [SerializeField] GameObject[] Enemy_Prefeds; //������
    [SerializeField] GameObject Boss_Prefeds; //�� ������
    [SerializeField] GameObject Seed_Prefeds;
    [SerializeField] GameObject[] Enemies_Prefeds;
    [SerializeField] int initEnemyCount = 50; //�ʱ� ������

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatEnemy1(initEnemyCount);
        CreatEnemy2(initEnemyCount);
    }
    public void CreatEnemy1(int cnt = 5)              //�� ���� �κ�
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(Enemy_Prefeds[0], transform.GetChild(0));   //�� ����
        }
    }

    public void CreatEnemy2(int cnt = 5)              //�� ���� �κ�
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(Enemy_Prefeds[1], transform.GetChild(1));   //�� ����
        }
    }
    public void UseEnemy1(Vector2 p, Quaternion rot)   //�� ����ϴ� �κ�
    {
        if (transform.childCount == 0)
        {
            CreatEnemy1(); //���� ���ٸ� ����
        }

        Enemy e = transform.GetChild(0).GetChild(0).GetComponent<Enemy>(); //pool 0��° ������Ʈ�� ����

        e.transform.position = p;         //�� ��ġ ����
        e.transform.rotation = rot;       //�� ���� ����
        e.gameObject.SetActive(true);     //���� Ȱ��ȭ
        e.transform.parent = null;        //�θ� ���� ����
        e.Move();                         //������ ����
        AliveEnemyPoolManager.i.AddEnemy(e.GameObject());
    }

    public void UseEnemy2(Vector2 p, Quaternion rot)   //�� ����ϴ� �κ�
    {
        if (transform.childCount == 0)
        {
            CreatEnemy2(); //���� ���ٸ� ����
        }

        Enemy e = transform.GetChild(1).GetChild(0).GetComponent<Enemy>(); //pool 0��° ������Ʈ�� ����

        e.transform.position = p;         //�� ��ġ ����
        e.transform.rotation = rot;       //�� ���� ����
        e.gameObject.SetActive(true);     //���� Ȱ��ȭ
        e.transform.parent = null;        //�θ� ���� ����
        e.Move();                         //������ ����
        AliveEnemyPoolManager.i.AddEnemy(e.GameObject());
    }

    public void ReturnEnemy1(GameObject e)
    {
        e.gameObject.SetActive(false);
        e.transform.SetParent(transform.GetChild(0));
        //��� �� �ٽ� pool������ ������
    }

    public void ReturnEnemy2(GameObject e)
    {
        e.gameObject.SetActive(false);
        e.transform.SetParent(transform.GetChild(1));
        //��� �� �ٽ� pool������ ������
    }

    public void CreatBoss()
    {
        Instantiate(Boss_Prefeds, new Vector3(0, 3.5f, 0), Quaternion.identity);   //���� ����
    }
    public void CreateSeed()
    {
        Instantiate(Seed_Prefeds, new Vector3(0, 3.5f, 0), Quaternion.identity);
        Instantiate(Seed_Prefeds, new Vector3(0, 3.5f, 0), Quaternion.identity);
        Instantiate(Seed_Prefeds, new Vector3(0, 3.5f, 0), Quaternion.identity);
    }
    public void CreateEnemies(Vector3 Pos)
    {
        int i = Random.Range(0, 3);
        Instantiate(Enemies_Prefeds[i], Pos, Quaternion.identity);
    }
    public GameObject returnEnemies(int i)
    {
        return Enemies_Prefeds[(int)i];
    }
}
