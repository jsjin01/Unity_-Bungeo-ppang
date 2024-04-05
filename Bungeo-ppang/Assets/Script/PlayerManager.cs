using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;              //목숨
    [SerializeField] public float atk = 10;          //공격력
    [SerializeField] public float atk_spd = 10f;     //공격 속도
    [SerializeField] public float cri = 5;           //치명타
    [SerializeField] public float speed = 10f;       //플레이어 이동속도
    [SerializeField] public float bulletSpeed = 15f; // 붕어빵 속도
    [SerializeField] public int shoot = 1;           //발사횟수
    [SerializeField] public int pass = 0;            //관통횟수
    [SerializeField] public float size = 0.08f;          //붕어빵 사이즈

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
            atk_spd *= 0.9f;
            Debug.Log("공격 속도 증가");
        };

        CardManager.i.evt2 += () =>
        {
            bulletSpeed *= 1.1f;
            Debug.Log("speed up");
        };

        CardManager.i.evt3 += () =>
        {
            shoot += 1;
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
    }
    private void Update()
    {
        UIManager.i.SetHp(Hp);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Hp -= 1;
            Debug.Log("Hit");
        }
    }
}
