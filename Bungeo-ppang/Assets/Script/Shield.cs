using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public int passcnt=0;       //����Ƚ��
    [SerializeField] public float shieldSpeed = 30f;
    public float dmg;       //���� ������
    float size = 1;

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
