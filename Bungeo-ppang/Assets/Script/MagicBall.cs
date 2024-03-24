using UnityEngine;

enum MAGICBALLTYPE
{
    FIRE,
    ICE
}
public class MagicBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float ballSpeed = 30f;   //구체가 날라가는 속도
    [SerializeField] public float dmg;        //공격력
    [SerializeField] MAGICBALLTYPE type;      //불 타입인지 얼음 타입인지 정함
    public float firedmg = 5;                        //화염데미지
    public float iceSpeedDown = 0.5f;               //얼음 감속 
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
