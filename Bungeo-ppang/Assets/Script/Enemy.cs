using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f; // 적의 이동 속도
    [SerializeField] float hp = 100f;//적의 체력
    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;

    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            EnemyDestroy();
        }
    }
    public void Move()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.velocity = pos.normalized*speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();
            hp -= bullet.dmg;

            if(hp <= 0f)
            {
                EnemyDestroy();
            }
        }
    }

    void EnemyDestroy() //적 삭제
    {
        hp = 100f; //나중에 다시 사용할 때 Hp 100
        EnemyPoolManager.i.ReturnEnemy(gameObject);
    }
    
}

