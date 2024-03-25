using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static Shield s;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bungeo_ppong_Prefeds;
    [SerializeField] public int passcnt=0;       //����Ƚ��
    [SerializeField] public float shieldSpeed = 30f;
    public float dmg;       //���� ������
    float size = 1;

    float atkspd;
    bool isShot = true;

    // Start is called before the first frame update
    private void Awake()
    {
        s = this;
    }
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
        atkspd = PlayerManager.i.atk_spd;
        Move();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * shieldSpeed; //���� ���󰡴� �κ�
        Invoke("ShieldDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(passcnt==0)
            {
                CancelInvoke("ShieldDestory");
                Destroy(gameObject);

                //StartCoroutine(delaytime());
                if (isShot)
                {
                    Attack();
                }
            }
            else
            {
                passcnt--;
            }
        }
    }
    /*IEnumerator delaytime()
    {
        yield return new WaitForSeconds(0.3f);
    }*/
    void Attack()
    {
        StartCoroutine(ShootCol());
        Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, Quaternion.identity);
    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(atkspd);
        isShot = true;
    }
    void ShieldDestory()
    {
        Destroy(gameObject);
    }

    void add_passcnt()      //���� ����Ƚ�� �߰�
    {
        passcnt++;
    }
    void SizeUp()       //���� ������ ����
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
}
