using System.Collections;
using UnityEngine;

public enum ENEMY{
    E1,
    E2
}

public class Enemy : MonoBehaviour
{
    [SerializeField] ENEMY type;
    [SerializeField] public float speed = 1f; // 적의 이동 속도
    [SerializeField] public float hp = 100f;//적의 체력
    [SerializeField] protected float MaxHp = 100f;//적의 최대 체력

    float e1mul = 1;
    float e2mul = 1;
    protected Vector2 pos = new Vector2(0, -1);
    public Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] public Animator anit; //애니메이터 컨트롤
    [SerializeField] Collider2D cd;

    protected IEnumerator fireCor; //마법 적용 코루틴 함수
    protected IEnumerator iceCor;
    protected IEnumerator thunderCor;

    protected IEnumerator deadCor; //죽는 코루틴 변수

    public virtual void OnEnable()
    {
        if(type == ENEMY.E1)
        {
            e1mul = EnemySpawner.i.E1mul;
            Debug.Log(e1mul);
            MaxHp *= e1mul;
            hp = MaxHp;
            Debug.Log(hp);

        }
        else if(type == ENEMY.E2)
        {
            e2mul = EnemySpawner.i.E2mul;
            Debug.Log(e1mul);
            MaxHp *= e2mul;
            hp = MaxHp;
            Debug.Log(hp);
        }
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
    public void StopMove()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        rb.velocity = Vector2.zero;
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();
            SoundManger.i.PlaySound(1);
            StartCoroutine(Hitchange());
            if (bullet.isShield) //쉴드 상태에서 나온 붕어빵은 데미지 없이 관통
            {
                bullet.isShield = false;
            }
            else
            {
                anit.SetTrigger("isHit");
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
            SoundManger.i.PlaySound(1);
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
            SoundManger.i.PlaySound(1);
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
        else if (other.CompareTag("Magic"))
        {
            MagicBall magicBall= other.gameObject.GetComponent<MagicBall>();
            hp -= magicBall.dmg;
            SoundManger.i.PlaySound(1);
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("Shield"))
        {
            Shield shield = other.gameObject.GetComponent<Shield>();
            hp -= shield.dmg;
            SoundManger.i.PlaySound(1);
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("Sword"))
        {
            Sword sword = other.gameObject.GetComponent<Sword>();
            hp -= sword.dmg;
            SoundManger.i.PlaySound(1);
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("THUNDER"))
        {
            Thunder t = other.gameObject.GetComponentInParent<Thunder>();
            hp -= t.dmg;
            SoundManger.i.PlaySound(1);
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

    virtual public void EnemyDestroy() //적 삭제
    {
        SoundManger.i.PlaySound(2);
        UIManager.i.GaugeBar_Warrior.value += 0.1f;
        UIManager.i.GaugeBar_Wizard.value += 0.1f;
        hp = MaxHp; //나중에 다시 사용할 때 Hp 100
        if(deadCor != null)
        {
            return;
        }
        deadCor = Dead();
        StartCoroutine(deadCor);
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

    public IEnumerator Fire(float dmg)
    {
        anit.SetBool("isFire", true);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            hp -= dmg;//불꽃 도트 데미지
        }
        anit.SetBool("isFire", false);
    }

    public IEnumerator Ice(float t)
    {
        anit.SetBool("isIce", true);
        float nowSpeed = speed;// 현재 속도 저장
        speed = 0;   //정지
        rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(t);
        speed = nowSpeed; //다시 되돌아옴
        rb.velocity = pos.normalized * speed;
        anit.SetBool("isIce", false);
    }

    public IEnumerator Stun(float time) //경직
    {
        anit.SetTrigger("isThunder");
        float nowSpeed = speed;// 현재 속도 저장
        speed = 0;//정지
        rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(time);
        speed = nowSpeed; //다시 되돌아옴
        rb.velocity = pos.normalized * speed;
    }


    public virtual IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }

    public virtual IEnumerator Dead()//0.3초 있다가 없어지도록 설계
    {
        anit.SetTrigger("isDead");
        float nowSpeed = speed;
        speed = 0;                  //정지
        rb.velocity = pos.normalized * speed;
        cd.enabled = false;
        yield return new WaitForSeconds(0.3f);
        speed = nowSpeed;
        cd.enabled = true;
        sr.color = Color.white;
        if(type == ENEMY.E1)
        {
            EnemyPoolManager.i.ReturnEnemy1(gameObject);
        }
        else if(type == ENEMY.E2)
        {
            EnemyPoolManager.i.ReturnEnemy2(gameObject);
        }

    }
}


