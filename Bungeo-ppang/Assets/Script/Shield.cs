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

    // Start is called before the first frame update
    private void Awake()
    {
        s = this;
    }
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
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
                Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, Quaternion.identity);
                Destroy(gameObject);
                //StartCoroutine(delaytime());

            }
            else
            {
                passcnt--;
            }
        }
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
