using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f; // ���� �̵� �ӵ�
    [SerializeField] float hp = 100f;//���� ü��
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

    void EnemyDestroy() //�� ����
    {
        hp = 100f; //���߿� �ٽ� ����� �� Hp 100
        EnemyPoolManager.i.ReturnEnemy(gameObject);
    }
    
}

