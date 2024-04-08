using UnityEngine;

enum MAGICBALLTYPE
{
    FIRE,
    ICE,
    MAGIC
}
public class MagicBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float ballSpeed = 30f;   //��ü�� ���󰡴� �ӵ�
    [SerializeField] public float dmg =0;        //���ݷ�
    [SerializeField] MAGICBALLTYPE type;      //�� Ÿ������ ���� Ÿ������ ����
    public float firedmg = 5;                        //ȭ��������
    public float iceTime = 5f;               //���� ���� �ð�
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(type == MAGICBALLTYPE.FIRE)
        {
            dmg = PlayerManager.i.fire_dmg;
        }
        else if(type == MAGICBALLTYPE.ICE)
        {
            dmg = PlayerManager.i.ice_dmg;
        }
        else if(type == MAGICBALLTYPE.MAGIC)
        {
            dmg = 10;
        }
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
