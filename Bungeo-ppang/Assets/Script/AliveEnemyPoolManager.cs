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
        /*if (Input.GetKey(KeyCode.Space))
        {
            for(int i=0;i<transform.childCount;i++)
            {
                Enemy e = transform.GetChild(i).GetComponent<Enemy>();
                e.StopMove();
            }
            Invoke("StopEnemy",1f);
            Debug.Log("ss");
        }*/
    }
    public void AddEnemy(GameObject e)
    {
        e.transform.SetParent(transform);
    }
    void StopEnemy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy e = transform.GetChild(i).GetComponent<Enemy>();
            e.Move();
        }
    }
    public void WarriorSkill()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy e = transform.GetChild(i).GetComponent<Enemy>();
            e.StopMove();
        }
        Invoke("StopEnemy", 1f);
    }
}
