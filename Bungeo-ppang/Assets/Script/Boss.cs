using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss i;

    [SerializeField] float speed = 2f;  // ���� �̵� �ӵ�
    [SerializeField] float hp = 500f;   //���� ü��
    [SerializeField] float MaxHp = 500f;//���� �ִ� ü��

    [SerializeField]GameObject GameClear; //���� óġ �� Ŭ����


    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField]Animator anit;

    IEnumerator fireCor; //���� ���� �ڷ�ƾ �Լ�
    IEnumerator iceCor;
    IEnumerator thunderCor;
    //���� �ڷ�ƾ
    IEnumerator spawnCor;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {

        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        InvokeRepeating("SpawnSeed", 1f, 4f);
    }
    private void Update()
    {
        if (hp <= 0f)
        {
            BossDestroy();
        }
    }

    public void SpawnSeed()
    {
        spawnCor = skill();
        StartCoroutine(spawnCor);
    }

    private void OnEnable()
    {
        hp = MaxHp;
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
                    BossDestroy();
                }
            }
        }
        else if (other.CompareTag("FIRE"))
        {
            MagicBall fireball = other.gameObject.GetComponent<MagicBall>();
            hp -= fireball.dmg;
            if (hp <= 0f)
            {
                BossDestroy();
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
            CancelInvoke("SpawnSeed");      //���� ����
            Invoke("SpawnAgain", iceball.iceTime);      //����ð� �� ���� �ٽ� ����
            hp -= iceball.dmg;
            if (hp <= 0f)
            {
                BossDestroy();
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
                BossDestroy();
            }
        }
        else if (other.CompareTag("Sword"))
        {
            Sword sword = other.gameObject.GetComponent<Sword>();
            hp -= sword.dmg;
            if (hp <= 0f)
            {
                BossDestroy();
            }
        }
        else if (other.CompareTag("THUNDER"))
        {
            Thunder t = other.gameObject.GetComponentInParent<Thunder>();
            CancelInvoke("SpawnSeed");              //���� ����
            Invoke("SpawnAgain", t.stunTime);      //���Ͻð� �� ���� �ٽ� ����
            hp -= t.dmg;
            if (hp <= 0f)
            {
                BossDestroy();
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
    public void SpawnAgain()     //����, ���� ������ ���� ����
    {
        InvokeRepeating("SpawnSeed", 1f, 4f);
    }
    void BossDestroy() //�� ����
    {
        UIManager.i.GaugeBar.value += 0.1f;
        hp = 500f; //���߿� �ٽ� ����� �� Hp 500
        GameClear.SetActive(true);//ui�� ����
        Time.timeScale = 0f;
        Destroy(gameObject);
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
        anit.SetBool("isFire", true);
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            hp -= dmg;//�Ҳ� ��Ʈ ������
        }
        anit.SetBool("isFire", false);
    }

    IEnumerator Ice(float t) //�̵��ӵ� 0���� ����� ��Ŀ���� ��ü
    {
        anit.SetBool("isIce", true);
        float nowSpeed = speed;// ���� �ӵ� ����
        //speed = 0;          //�ӵ� ����
        //rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(t);
        //speed = nowSpeed; //�ٽ� �ǵ��ƿ�
        //rb.velocity = pos.normalized * speed;
        anit.SetBool("isIce", false);
    }

    IEnumerator Stun(float time) //���� => ��Ŀ���� ��ü?
    {
        anit.SetBool("isThunder", true);
        float nowSpeed = speed;// ���� �ӵ� ����
        //speed = 0;//����
        //rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(time);
        //speed = nowSpeed; //�ٽ� �ǵ��ƿ�
        //rb.velocity = pos.normalized * speed;
    }

    IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }

    IEnumerator skill()
    {
        anit.SetTrigger("isSkill");
        yield return new WaitForSeconds(1.15f);
        EnemyPoolManager.i.CreateSeed();
    }
}
