using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D cd;
    [SerializeField] public int passcnt = 0;         //관통횟수
    [SerializeField] public int numberOfSwords = 1;  //검격의 수
    [SerializeField] float SwordSpeed = 30f;  //검격 속도
    public float dmg;       //검격 데미지

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.sword_dmg;
        numberOfSwords = PlayerManager.i.swords;
        passcnt = PlayerManager.i.sword_pass;
    }
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector3 forceDirection = rb.transform.up;
        rb.AddForce(forceDirection * SwordSpeed);
        Invoke("SwordDestory", 3);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (passcnt == 0)
            {
                CancelInvoke("SwordDestory");
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
        else if (collision.CompareTag("Wall"))
        {
            CancelInvoke("SwordDestory");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            CancelInvoke("SwordDestory");
            Destroy(gameObject);
        }
    }

    void SwordDestory()
    {
        Destroy(gameObject);
    }
    void MonsterPass()//관통 제어
    {
        cd.enabled = true;
    }

}
