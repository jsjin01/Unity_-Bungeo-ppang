//using System;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float BulletSpeed = 30f;   //총알 속도
    public float dmg;          //공격력
    public int monsterPass = 0;       //관통 횟수
    [SerializeField] GameObject swordPrefebs;        //검기
    float size = 1;                             //사이즈
    float index;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*Invoke("Collider", 0.3f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;*/
        dmg = PlayerManager.i.atk;
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * BulletSpeed; //총알 날라가는 부분
        Invoke("BulletDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            dmg = 0;
        }
        if (collision.CompareTag("Enemy"))
        {
            if (monsterPass == 0)//관통 횟수가 0일때는 파괴
            {
                BulletDestory();
                CancelInvoke("BulletDestory");
            }
            else//아니라면 관통 가능 횟수를 한번 뺌
            {
                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    //일반 보상 함수 
    void SpeedUp()//붕어빵의 이동 속도를 빠르게 만듬
    {
        BulletSpeed *= 1.05f; //5%씩 빨라짐
    }

    void atkRateUp()//붕어빵 발사 속도를 빠르게 만듬
    {
        PlayerManager.i.atk_spd *= 0.95f; //5%씩 빨라짐
    }

    void atkUp()
    {
        PlayerManager.i.atk += 5f; //5씩 증가
    }//붕어빵 공격력을 높임

    void passUp()
    {
        monsterPass++;
    }//붕어빵의 관통 횟수를 늘림

    void SizeUp()//붕어빵의 데미지가 향상됨
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
    /*public void Collider()   //충돌 활성화
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }*/
}
