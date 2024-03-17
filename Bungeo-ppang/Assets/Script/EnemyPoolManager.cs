using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;

    [SerializeField] GameObject Enemy_Prefeds; //������
    [SerializeField] int initEnemyCount = 50; //�ʱ� ������

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatEnemy(initEnemyCount);
    }

    public void CreatEnemy(int cnt = 5)              //�� ���� �κ�
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(Enemy_Prefeds, transform);   //�� ����
        }
    }

    public void UseEnemy(Vector2 p, Quaternion rot)   //�� ����ϴ� �κ�
    {
        if (transform.childCount == 0)
        {
            CreatEnemy(); //���� ���ٸ� ����
        }

        Enemy e = transform.GetChild(0).GetComponent<Enemy>(); //pool 0��° ������Ʈ�� ����

        e.transform.position = p;         //�� ��ġ ����
        e.transform.rotation = rot;       //�� ���� ����
        e.gameObject.SetActive(true);     //���� Ȱ��ȭ
        e.transform.parent = null;        //�θ� ���� ����
        e.Move();                         //������ ����
    }

    public void ReturnEnemy(GameObject e)
    {
        e.gameObject.SetActive(false);
        e.transform.SetParent(transform);
        //��� �� �ٽ� pool������ ������
    }
}
