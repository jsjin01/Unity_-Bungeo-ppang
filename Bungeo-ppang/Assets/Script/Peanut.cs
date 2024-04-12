using System.Collections;
using UnityEngine;

public class Peanut : Enemy
{
    [SerializeField] GameObject peanutslice1_Prefebs;
    [SerializeField] GameObject peanutslice2_Prefebs;
    private Color originalcolor;
    private void Start()
    {
        Move();
        originalcolor = sr.color;

    }
    // Update is called once per frame
    public override void OnEnable()
    {
        hp = MaxHp;
    }
    public override void Move()
    {
        base.Move();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bungeo_ppong_BulletComponent bullet = other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();

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
                    Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                    Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
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
                Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
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
                Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
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
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("Sword"))
        {
            Sword sword = other.gameObject.GetComponent<Sword>();
            hp -= sword.dmg;
            anit.SetTrigger("isHit");
            if (hp <= 0f)
            {
                Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                EnemyDestroy();
            }
        }
        else if (other.CompareTag("THUNDER"))
        {
            Thunder t = other.gameObject.GetComponentInParent<Thunder>();
            hp -= t.dmg;
            if (hp <= 0f)
            {
                Instantiate(peanutslice1_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(peanutslice2_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
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
    public override void EnemyDestroy()
    {
        base.EnemyDestroy();
    }
    public override IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalcolor;
    }
}
