using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f; // ���� �̵� �ӵ�
    [SerializeField] float hp = 100f;//���� ü��
    [SerializeField] float MaxHp = 100f;//���� �ִ� ü��
    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;
    SpriteRenderer sr;

    IEnumerator fireCor; //���� ���� �ڷ�ƾ �Լ�
    IEnumerator iceCor;
    IEnumerator thunderCor;
    private void Update()
    {
        if (transform.position.y <= -6f || hp <= 0f)
        {
            EnemyDestroy();
        }
    }
    private void OnEnable()
    {
        hp = MaxHp;
        speed = 2f;

    }

    public void Move()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            
        }
        if(sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        rb.velocity = pos.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();
            StartCoroutine(Hitchange());
            if (bullet.isShield)
            {
                bullet.isShield = false;
            }
            else
            {
                hp -= bullet.dmg;
                if (hp <= 0f)
                {
                    EnemyDestroy();
                }
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
                if (fireCor != null)
                {
                    StopCoroutine(fireCor);
                }
                fireCor = Fire(fireball.firedmg);
                StartCoroutine(fireCor);
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
                if (iceCor != null)
                {
                    StopCoroutine(iceCor);
                }
                iceCor = Ice(iceball.iceSpeedDown);
                StartCoroutine(iceCor);
            }
        }
        else if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            hp -= shield.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("Sword"))
        {
            Sword sword = other.gameObject.GetComponent<Sword>();
            hp -= sword.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("THUNDER"))
        {
            Thunder t = other.gameObject.GetComponentInParent<Thunder>();
            hp -= t.dmg;
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
            else
            {
                if (thunderCor != null)
                {
                    StopCoroutine(thunderCor);
                }
                thunderCor = Stun(t.stunTime);
                StartCoroutine(thunderCor);
            }
        }
    }

    void EnemyDestroy() //�� ����
    {
        hp = 100f; //���߿� �ٽ� ����� �� Hp 100
        EnemyPoolManager.i.ReturnEnemy(gameObject);
        if(fireCor != null)
        {
            StopCoroutine(fireCor);
        }
        if(iceCor != null)
        {
            StopCoroutine(iceCor);
        }
        if (thunderCor != null)
        {
            StopCoroutine(thunderCor);
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

    IEnumerator Stun(float time) //����
    {
        float nowSpeed = speed;// ���� �ӵ� ����
        speed = 0;//����
        rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(time);
        speed = nowSpeed; //�ٽ� �ǵ��ƿ�
        rb.velocity = pos.normalized * speed;
    }

    IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }
}


