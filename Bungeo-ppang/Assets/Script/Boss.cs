using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public static Boss i;

    [SerializeField] float speed = 2f;  // ���� �̵� �ӵ�
    [SerializeField] float hp = 1000f;   //���� ü��
    [SerializeField] float MaxHp = 1000f;//���� �ִ� ü��

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

    Slider Hpslider;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        Hpslider =GameObject.Find("BossSlider").GetComponent<Slider>();
        Hpslider.maxValue = MaxHp;

        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        InvokeRepeating("SpawnSeed", 1f, 6f);
    }
    private void Update()
    {
        Debug.Log("Hp :" + hp);
        Hpslider.value = hp;
        if (hp <= 0f)
        {
            BossDestroy();
        }
    }

    void SpawnSeed()
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
            SoundManger.i.PlaySound(10);
            anit.SetTrigger("isHit");
            if (bullet.isShield)
            {
                bullet.isShield = false;
            }
            else
            {
                hp -= bullet.dmg;
                if (hp <= 0f)
                {
                    SoundManger.i.PlaySound(9);
                    BossDestroy();
                }
            }
        }
        else if (other.CompareTag("FIRE"))
        {
            MagicBall fireball = other.gameObject.GetComponent<MagicBall>();
            SoundManger.i.PlaySound(10);
            hp -= fireball.dmg;
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                SoundManger.i.PlaySound(9);
                BossDestroy();
            }
            else
            {
                if (fireCor != null)
                {
                    StopCoroutine(fireCor);
                }
                fireCor = Fire(3);
                StartCoroutine(fireCor);
            }
        }
        else if (other.CompareTag("ICE"))
        {
            MagicBall iceball = other.gameObject.GetComponent<MagicBall>();
            SoundManger.i.PlaySound(10);
            CancelInvoke("SpawnSeed");      //���� ����
            Invoke("SpawnAgain", iceball.iceTime);      //����ð� �� ���� �ٽ� ����
            hp -= iceball.dmg;
            if (hp <= 0f)
            {
                SoundManger.i.PlaySound(9);
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
            anit.SetTrigger("isHit");
            SoundManger.i.PlaySound(10);
            if (hp <= 0f)
            {
                SoundManger.i.PlaySound(9);
                BossDestroy();
            }
        }
        else if (other.CompareTag("Sword"))
        {
            Sword sword = other.gameObject.GetComponent<Sword>();
            hp -= sword.dmg;
            anit.SetTrigger("isHit");
            SoundManger.i.PlaySound(10);
            if (hp <= 0f)
            {
                SoundManger.i.PlaySound(9);
                BossDestroy();
            }
        }
        else if (other.CompareTag("THUNDER"))
        {
            Thunder t = other.gameObject.GetComponentInParent<Thunder>();
            SoundManger.i.PlaySound(10);
            CancelInvoke("SpawnSeed");              //���� ����
            Invoke("SpawnAgain", t.stunTime);      //���Ͻð� �� ���� �ٽ� ����
            hp -= t.dmg;
            if (hp <= 0f)
            {
                SoundManger.i.PlaySound(9);
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
    void SpawnAgain()     //����, ���� ������ ���� ����
    {
        InvokeRepeating("SpawnSeed", 1f, 10f);
    }
    void BossDestroy() //�� ����
    {
        hp = MaxHp; //���߿� �ٽ� ����� �� Hp 500
        SoundManger.i.PlaySound(11);
        anit.SetTrigger("isDead");
        Invoke("gameClear", 0.1f);
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
        for (int i = 0; i < 3; i++)
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
        SoundManger.i.PlaySound(8);
        yield return new WaitForSeconds(1.15f);
        EnemyPoolManager.i.CreateSeed();
    }

    void gameClear()
    {
        GameClear.SetActive(true);//ui�� ����
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}
