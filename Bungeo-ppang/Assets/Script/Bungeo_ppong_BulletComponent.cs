using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField] float BulletSpeed = 30f;   //총알 속도
    [SerializeField] public float dmg;       //공격력
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * BulletSpeed; //총알 날라가는 부분
        Invoke("BulletDestory",3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BulletDestory();
        }
    }

    public void BulletDestory()
    {
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }
}
