using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss i;

    [SerializeField] float speed = 2f;  // 적의 이동 속도
    [SerializeField] float hp = 1000f;   //적의 체력
    [SerializeField] float MaxHp = 1000f;//적의 최대 체력

    [SerializeField]GameObject GameClear; //보스 처치 시 클리어


    Vector2 pos = new Vector2(0, -1);
    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField]Animator anit;

    IEnumerator fireCor; //마법 적용 코루틴 함수
    IEnumerator iceCor;
    IEnumerator thunderCor;
    //스폰 코루틴
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
        InvokeRepeating("SpawnSeed", 1f, 10f);
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
                fireCor = Fire(3);
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
            CancelInvoke("SpawnSeed");              //패턴 중지
            Invoke("SpawnAgain", t.stunTime);      //스턴시간 후 패턴 다시 시작
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
    void SpawnAgain()     //빙결, 번개 맞으면 공격 중지
    {
        InvokeRepeating("SpawnSeed", 1f, 10f);
    }
    void BossDestroy() //적 삭제
    {
        hp = 500f; //나중에 다시 사용할 때 Hp 500
        GameClear.SetActive(true);//ui이 띄우기
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
        for (int i = 0; i < 3; i++)
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

    IEnumerator skill()
    {
        anit.SetTrigger("isSkill");
        yield return new WaitForSeconds(1.15f);
        EnemyPoolManager.i.CreateSeed();
    }
}
