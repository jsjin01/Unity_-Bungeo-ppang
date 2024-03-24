using UnityEngine;

enum MAGICBALLTYPE
{
    FIRE,
    ICE
}
public class MagicBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float ballSpeed = 30f;   //��ü�� ���󰡴� �ӵ�
    [SerializeField] public float dmg;        //���ݷ�
    [SerializeField] MAGICBALLTYPE type;      //�� Ÿ������ ���� Ÿ������ ����
    public float firedmg = 5;                        //ȭ��������
    public float iceSpeedDown = 0.5f;               //���� ���� 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
        Move();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * ballSpeed; //���� ���󰡴� �κ�
        Invoke("MagicBallDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CancelInvoke("MagicBallDestory");
            Destroy(gameObject);
        }
    }
    void MagicBallDestory()
    {
        Destroy(gameObject);
    }
}
