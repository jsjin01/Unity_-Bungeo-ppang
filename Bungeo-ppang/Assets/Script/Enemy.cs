using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f; // ���� �̵� �ӵ�
    [SerializeField] float hp = 100f;//���� ü��
    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;

    IEnumerator magicCor; //���� ���� �ڷ�ƾ �Լ�

    private void Update()
    {
        if (transform.position.y <= -6f || hp <= 0f)
        {
            EnemyDestroy();
        }
    }
    private void OnEnable()
    {
        speed = 2f;
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.velocity = pos.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();
            hp -= bullet.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("FIRE"))
        {
            MagicBall fireball = other.gameObject.GetComponent<MagicBall>();
            hp -= fireball.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
            else
            {
                if (magicCor != null)
                {
                    StopCoroutine(magicCor);
                }
                magicCor = Fire(fireball.firedmg);
                StartCoroutine(magicCor);
            }
        }
        else if (other.CompareTag("ICE"))
        {
            MagicBall iceball = other.gameObject.GetComponent<MagicBall>();
            hp -= iceball.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
            else
            {
                if (magicCor != null)
                {
                    StopCoroutine(magicCor);
                }
                magicCor = Ice(iceball.iceSpeedDown);
                StartCoroutine(magicCor);
            }
        }
    }

    void EnemyDestroy() //�� ����
    {
        hp = 100f; //���߿� �ٽ� ����� �� Hp 100
        EnemyPoolManager.i.ReturnEnemy(gameObject);
        if(magicCor != null)
        {
            StopCoroutine(magicCor);
        }
    }

    IEnumerator Fire(float dmg)
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            hp -= dmg;//�Ҳ� ��Ʈ ������
        }
    }

    IEnumerator Ice(float speedDown)
    {
        float nowSpeed = speed;// ���� �ӵ� ����
        speed *= speedDown;//����
        rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(3f);
        speed = nowSpeed; //�ٽ� �ǵ��ƿ�
        rb.velocity = pos.normalized * speed;
    }

}


