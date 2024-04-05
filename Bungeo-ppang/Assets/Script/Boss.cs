using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float speed = 2f; // 적의 이동 속도
    [SerializeField] float hp = 500f;//적의 체력
    [SerializeField] float MaxHp = 500f;//적의 최대 체력
    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;
    SpriteRenderer sr;

    IEnumerator fireCor; //마법 적용 코루틴 함수
    IEnumerator iceCor;
    IEnumerator thunderCor;
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

    void SpawnSeed()
    {
        EnemyPoolManager.i.CreateSeed();
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
            CancelInvoke("SpawnSeed");      //패턴 중지
            Invoke("SpawnAgain", iceball.iceTime);      //빙결시간 후 패턴 다시 시작
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
    void SpawnAgain()     //빙결 맞으면 공격 중지
    {
        InvokeRepeating("SpawnSeed", 1f, 4f);
    }
    void BossDestroy() //적 삭제
    {
        hp = 500f; //나중에 다시 사용할 때 Hp 500
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
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            hp -= dmg;//불꽃 도트 데미지
        }
    }

    IEnumerator Ice(float t) //이동속도 0으로 만드는 매커니즘 교체
    {
        float nowSpeed = speed;// 현재 속도 저장
        //speed = 0;          //속도 정지
        //rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(t);
        //speed = nowSpeed; //다시 되돌아옴
        //rb.velocity = pos.normalized * speed;
    }

    IEnumerator Stun(float time) //경직 => 매커니즘 교체?
    {
        float nowSpeed = speed;// 현재 속도 저장
        //speed = 0;//감속
        //rb.velocity = pos.normalized * speed;
        yield return new WaitForSeconds(time);
        //speed = nowSpeed; //다시 되돌아옴
        //rb.velocity = pos.normalized * speed;
    }

    IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }
}
