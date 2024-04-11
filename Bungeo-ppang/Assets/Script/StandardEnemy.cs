using System.Collections;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    [SerializeField] protected float speed = 2f; // ���� �̵� �ӵ�
    [SerializeField] protected float hp = 100f;//���� ü��
    [SerializeField] protected float MaxHp = 100f;//���� �ִ� ü��
    protected Vector2 pos = new Vector2(0, -1);
    protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] Collider2D cd;

    protected IEnumerator fireCor; //���� ���� �ڷ�ƾ �Լ�
    protected IEnumerator iceCor;
    protected IEnumerator thunderCor;

    public virtual void OnEnable()
    {
        hp = MaxHp;
        speed = 2f;
    }

    public virtual void Move()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        rb.velocity = pos.normalized * speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();

            StartCoroutine(Hitchange());
            if (bullet.isShield) //���� ���¿��� ���� �ؾ�� ������ ���� ����
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
                fireCor = Fire(5);
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
                iceCor = Ice(iceball.iceTime);
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
        else if (other.CompareTag("Player"))
        {
            EnemyDestroy();
        }
    }

    virtual public void EnemyDestroy() //�� ����
    {
        UIManager.i.GaugeBar_Warrior.value += 0.1f;
        UIManager.i.GaugeBar_Wizard.value += 0.1f;
        hp = 100f; //���߿� �ٽ� ����� �� Hp 100
        StartCoroutine(Dead());
        if (fireCor != null)
        {
            StopCoroutine(fireCor);
        }
        if (iceCor != null)
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
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            hp -= dmg;//�Ҳ� ��Ʈ ������
        }
    }

    IEnumerator Ice(float t)
    {
        float nowSpeed = speed;// ���� �ӵ� ����
        speed = 0;   //����
        rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(t);
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


    public virtual IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }

    virtual public IEnumerator Dead()//0.3�� �ִٰ� ���������� ����
    {
        float nowSpeed = speed;
        speed = 0;                  //����
        rb.velocity = pos.normalized * speed;
        cd.enabled = false;
        yield return new WaitForSeconds(0.3f);
        speed = nowSpeed;
        cd.enabled = true;
        sr.color = Color.white;
        EnemyPoolManager.i.ReturnEnemy1(gameObject);
    }
}
