using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEnemyPoolManager : MonoBehaviour
{
    public static AliveEnemyPoolManager i;
    private void Awake()
    {
        i = this; 
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddEnemy(GameObject e)
    {
        e.transform.SetParent(transform);
    }
    void StopEnemy()
    {
        EnemySpawner es = GameObject.FindObjectOfType<EnemySpawner>();
        if (es != null)
        {
            es.isFever = false;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy e = transform.GetChild(i).GetComponent<Enemy>();
            e.Move();
        }
    }
    public void WarriorSkill()
    {
        EnemySpawner es = GameObject.FindObjectOfType<EnemySpawner>();
        if (es != null)
        {
            es.isFever = true;      //���� ���� ����
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy e = transform.GetChild(i).GetComponent<Enemy>();
            e.StopMove();       //������ ���߱�
        }
        Invoke("StopEnemy", 4f);
    }
}
