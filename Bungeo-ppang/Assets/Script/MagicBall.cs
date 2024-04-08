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
    [SerializeField] float ballSpeed = 30f;   //구체가 날라가는 속도
    [SerializeField] public float dmg =0;        //공격력
    [SerializeField] MAGICBALLTYPE type;      //불 타입인지 얼음 타입인지 정함
    public float firedmg = 5;                        //화염데미지
    public float iceTime = 5f;               //얼음 지속 시간
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
        rb.velocity = shootPos.normalized * ballSpeed; //볼이 날라가는 부분
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
