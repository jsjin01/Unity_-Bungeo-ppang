//using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D cd;
    [SerializeField] public int passcnt = 0;       //관통횟수
    [SerializeField] public float shieldSpeed = 30f;
    public float dmg;                             //방패 데미지

    [SerializeField] GameObject swordPrefebs;   //검기
    float index;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.dep_atk;
        passcnt = PlayerManager.i.dep_pass;
        Move();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * shieldSpeed; //방어막이 날라가는 부분
        Invoke("ShieldDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (passcnt == 0)
            {
                CancelInvoke("ShieldDestory");
                Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, Quaternion.identity, false);
                Destroy(gameObject);
                if (PlayerManager.i.isSword)
                {
                    for (int i = 0; i < PlayerManager.i.swords; i++)
                    {
                        SwordCreat();
                    }
                }
            }
            else
            {
                if (cd == null)
                {
                    cd = GetComponent<CapsuleCollider2D>();
                }
                cd.enabled = false;
                Invoke("MonsterPass", 0.2f);
                passcnt--;
            }
        }
    }
    public void SwordCreat()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
    }

    void ShieldDestory()
    {
        Destroy(gameObject);
    }

    void MonsterPass()//관통 제어
    {
        cd.enabled = true;
    }
}
