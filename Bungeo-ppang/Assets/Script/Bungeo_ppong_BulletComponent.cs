//using System;
using System.Collections;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cd;
    [SerializeField] public float BulletSpeed = 30f;   //총알 속도
    public float dmg;          //공격력
    public int monsterPass = 0;       //관통 횟수
    int maxMonsterPass = 0;          //최대 관통횟수
    [SerializeField] GameObject swordPrefebs;        //검기
    float size = 1;                             //사이즈
    float index;
    bool isSword = false;                       //검기 온오프
    [SerializeField]public bool isShield = false;               //쉴드 상태일때

    IEnumerator passCor; //관통 코루틴
    void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
    }

    private void OnEnable()//다시 가져왔을 때 초기화
    {
        monsterPass = maxMonsterPass;
        if (isShield)
        {
        if(cd == null)
            {
                cd = GetComponent<CapsuleCollider2D>();
            }
            cd.enabled = false;
            Invoke("MonsterPass", 0.2f);
        }
    }
    public void Move() //움직임
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
        if (collision.CompareTag("Enemy")||collision.CompareTag("Peanut"))
        {
            if (monsterPass == 0)//관통 횟수가 0일때는 파괴
            {
                BulletDestory();
                CancelInvoke("BulletDestory");
            }
            else//아니라면 관통 가능 횟수를 한번 뺌
            {
                if (cd == null)
                {
                    cd = GetComponent<CapsuleCollider2D>();
                }
                cd.enabled = false;
                Invoke("MonsterPass", 0.2f);
                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        if (isSword)//검기 소환
        {
            SwordCreat();
        }
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    public void SwordCreat()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
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

    void MonsterPass()//관통 제어
    {
        cd.enabled = true;
    }


}
