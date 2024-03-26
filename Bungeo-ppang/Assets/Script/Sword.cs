using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public int passcnt = 0;         //����Ƚ��
    [SerializeField] public int numberOfSwords = 1;  //�˰��� ��
    [SerializeField] float SwordSpeed = 30f;  //�˰� �ӵ�
    public float dmg;       //�˰� ������
    float size = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
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
                passcnt--;
            }
        }
    }
    void SwordDestory()
    {
        Destroy(gameObject);
    }

    void add_passcnt()      //�˰� ����Ƚ�� �߰�
    {
        passcnt++;
    }
    void addSword()     //�˰� ���� ����
    {
        numberOfSwords++;
    }
    void SizeUp()       //�˰� ������ ����
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
}
