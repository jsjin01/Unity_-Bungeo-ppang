using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;             //목숨
    [SerializeField] public float atk = 10;        //공격력
    [SerializeField] public float atk_spd = 10f;  //공격 속도
    [SerializeField] public float cri = 5;         //치명타
    [SerializeField] public float speed  = 10f;     //이동속도

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
