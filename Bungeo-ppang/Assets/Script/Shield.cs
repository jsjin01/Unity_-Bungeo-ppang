using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D cd;
    [SerializeField] public int passcnt = 0;       //관통횟수
    [SerializeField] public float shieldSpeed = 30f;
    public float dmg;                             //방패 데미지


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.dep_atk;
        passcnt = PlayerManager.i.dep_pass;
        Move();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * shieldSpeed; //방어막이 날라가는 부분
        Invoke("ShieldDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (passcnt == 0)
            {
                CancelInvoke("ShieldDestory");
                Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, Quaternion.identity, true);
                Destroy(gameObject);

            }
            else
            {
                if (cd == null)
                {
                    cd = GetComponent<CapsuleCollider2D>();
                }
                cd.enabled = false;
                Invoke("MonsterPass", 0.2f);
                passcnt--;
            }
        }
    }

    void ShieldDestory()
    {
        Destroy(gameObject);
    }

    void MonsterPass()//관통 제어
    {
        cd.enabled = true;
    }
}
