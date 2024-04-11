using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;              //목숨
    [SerializeField] public float atk = 10;          //공격력
    [SerializeField] public float atk_spd = 10f;     //공격 속도
    [SerializeField] public float speed = 10f;       //플레이어 이동속도
    
    //붕어빵 관련
    [SerializeField] public float bulletSpeed = 15f;     // 붕어빵 속도
    [SerializeField] public int shoot = 1;               //발사횟수
    [SerializeField] public int pass = 0;                //관통횟수
    [SerializeField] public float size = 0.08f;          //붕어빵 사이즈

    //쉴드
    [SerializeField] public bool isShield = false;      //쉴드 온 오프 && 전사붕 활성화
    [SerializeField] public float dep_atk = 20;         //방패 공격력
    [SerializeField] public int dep_pass = 0;           //방패 관통력

    //검기
    [SerializeField] public bool isSword = false;       //검기 온오프
    [SerializeField] public int sword_dmg = 15;         //검기 데미지
    [SerializeField] public int swords = 1;             //검기 갯수
    [SerializeField] public int sword_pass = 0;         //검기 관통 갯수

    //법사붕
    [SerializeField] public bool magic = false;        //법사붕 활성화 
    [SerializeField] public bool fire = false;         //파이어볼 활성화
    [SerializeField] public bool ice = false;          //아이스볼 활성화
    [SerializeField] public bool thunder = false;      //번개 활성화

    //파이어볼
    [SerializeField] public float fire_dmg = 10;       //파이어볼 데미지
    [SerializeField] public float fire_col = 5f;       //파이어볼 쿨타임

    //아이스볼
    [SerializeField] public float ice_dmg = 10;        //아이스볼 데미지
    [SerializeField] public float ice_col = 5f;        //아이스볼 쿨타임

    //번개
    [SerializeField] public float thunder_dmg = 10;    //번개 데미지
    [SerializeField] public float thunder_col = 5f;    //번개 쿨타임


    public Action gameEnd;  //게임 끝 => over or Boss 처치
    public Action boss;     //보스 처리
    bool Gend = false;      //게임 끝 판단 변수
    [SerializeField]GameObject gameover;
    [SerializeField]GameObject gameClear;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CardManager.i.evt1 += () =>
        {
            atk_spd *= 0.8f;
            Debug.Log("공격 속도 증가");
        };

        CardManager.i.evt2 += () =>
        {
            bulletSpeed *= 1.3f;
            Debug.Log("speed up");
        };

        CardManager.i.evt3 += () =>
        {
            shoot += 1;
            atk -= 2;
            Debug.Log("1회 추가");
        };

        CardManager.i.evt4 += () =>
        {
            atk += 5;
            Debug.Log("Dmg 증가");
        };

        CardManager.i.evt5 += () =>
        {
            pass += 1;
            Debug.Log("pass증가");
        };

        CardManager.i.evt6 += () =>
        {
            size *= 1.5f;
            Debug.Log("size up");
        };

        CardManager.i.evt7 += () =>
        {
            isShield = true;        //쉴드 활성화
            Debug.Log("shield");
        };

        CardManager.i.evt8 += () =>
        {
            dep_pass += 1;
            Debug.Log("dep_pass");
        };

        CardManager.i.evt9 += () =>
        {
            dep_atk += 10;
            Debug.Log("dep_atk");
        };

        CardManager.i.evt10 += () =>
        {
            isSword = true;
            Debug.Log("sword on");
        };

        CardManager.i.evt11 += () =>
        {
            sword_dmg += 10;
            Debug.Log("sword dmg");
        };

        CardManager.i.evt12 += () =>
        {
            swords += 1;
            Debug.Log("swords");
        };

        CardManager.i.evt13 += () =>
        {
            sword_pass += 1;
            Debug.Log("sword pass");
        };

        CardManager.i.evt14 += () =>
        {
            magic = true;
        };

        CardManager.i.evt15 += () =>
        {
            fire = true;
        };

        CardManager.i.evt16 += () =>
        {
            fire_dmg += 20;
        };

        CardManager.i.evt17 += () =>
        {
            fire_col *= 0.8f;
        };

        CardManager.i.evt18 += () =>
        {
            ice = true;
        };

        CardManager.i.evt19 += () =>
        {
            ice_dmg += 20;
        };

        CardManager.i.evt20 += () =>
        {
            ice_col *= 0.8f;
        };

        CardManager.i.evt21 += () =>
        {
            thunder = true;
        };

        CardManager.i.evt22 += () =>
        {
            thunder_dmg += 20;
        };

        CardManager.i.evt23 += () =>
        {
            thunder_col *= 0.8f;
        };

    }
    private void Update()
    {
        UIManager.i.SetHp(Hp);
        if(Hp <= 0)
        {
            if (Gend)
            {
                return;
            }
            Time.timeScale = 0f;
            gameEnd(); //게임 끝
            gameover.SetActive(true);
            Gend = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Hp -= 1;
            Debug.Log("Hit");
            UIManager.i.GaugeBar_Warrior.value += 0.4f;
            UIManager.i.GaugeBar_Wizard.value += 0.4f;
        }
    }
}
