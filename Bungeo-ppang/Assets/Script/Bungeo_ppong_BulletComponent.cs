//using System;
using System.Collections;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cd;
    [SerializeField] Sprite[] bungs;
    [SerializeField] SpriteRenderer sr;

    float BulletSpeed;                          //총알 속도
    public float dmg;                           //공격력
    public int monsterPass = 0;                 //관통 횟수
    int maxMonsterPass = 0;                     //최대 관통횟수
    [SerializeField] GameObject swordPrefebs;   //검기
    float size = 0.08f;                         //사이즈
    float index;

    int swords = 1;                             //검기 갯수
    [SerializeField] bool isSword = false;      //검기 온오프
    public bool isShield = false;               //쉴드 상태일때

    public bool magic;                          //법사붕
    public bool warrior;                        //전사붕

    IEnumerator passCor;                        //관통 코루틴
    void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
    }

    private void OnEnable()//다시 가져왔을 때 초기화
    {
        BulletSpeed = PlayerManager.i.bulletSpeed;
        dmg = PlayerManager.i.atk;
        maxMonsterPass = PlayerManager.i.pass;
        size = PlayerManager.i.size;
        transform.localScale = new Vector3(size, size, 1);

        isSword =  PlayerManager.i.isSword;
        swords = PlayerManager.i.swords;  
        
        magic = PlayerManager.i.magic;
        warrior = PlayerManager.i.isShield;

        if (magic)
        {
            sr.sprite = bungs[2];
        }
        else if (warrior)
        {
            sr.sprite= bungs[1];
        }

        //초기화
        monsterPass = maxMonsterPass;
        if (isShield)
        {
            if (cd == null)
            {
                cd = GetComponent<CapsuleCollider2D>();
            }
            cd.enabled = false;
            Invoke("MonsterPass", 0.1f);
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
        if (collision.CompareTag("Enemy"))
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
                Invoke("MonsterPass", 0.25f);
                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        if (isSword)//검기 소환
        {
            SoundManger.i.PlaySound(13);
            for (int i = 0; i < swords; i++)
            {
                SwordCreat();
            }
        }
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    public void SwordCreat()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
    }

    void MonsterPass()//관통 제어
    {
        cd.enabled = true;
    }
}
