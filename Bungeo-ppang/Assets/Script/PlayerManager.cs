using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;             //���
    [SerializeField] public float atk = 10;        //���ݷ�
    [SerializeField] public float atk_spd = 10f;  //���� �ӵ�
    [SerializeField] public float cri = 5;         //ġ��Ÿ
    [SerializeField] public float speed  = 10f;     //�̵��ӵ�

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
