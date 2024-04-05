using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;              //���
    [SerializeField] public float atk = 10;          //���ݷ�
    [SerializeField] public float atk_spd = 10f;     //���� �ӵ�
    [SerializeField] public float cri = 5;           //ġ��Ÿ
    [SerializeField] public float speed = 10f;       //�÷��̾� �̵��ӵ�
    [SerializeField] public float bulletSpeed = 15f; // �ؾ �ӵ�
    [SerializeField] public int shoot = 1;           //�߻�Ƚ��
    [SerializeField] public int pass = 0;            //����Ƚ��
    [SerializeField] public float size = 0.08f;          //�ؾ ������

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
            Debug.Log("���� �ӵ� ����");
        };

        CardManager.i.evt2 += () =>
        {
            bulletSpeed *= 1.1f;
            Debug.Log("speed up");
        };

        CardManager.i.evt3 += () =>
        {
            shoot += 1;
            Debug.Log("1ȸ �߰�");
        };

        CardManager.i.evt4 += () =>
        {
            atk += 5;
            Debug.Log("Dmg ����");
        };

        CardManager.i.evt5 += () =>
        {
            pass += 1;
            Debug.Log("pass����");
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
