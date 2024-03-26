//using System;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float BulletSpeed = 30f;   //�Ѿ� �ӵ�
    public float dmg;          //���ݷ�
    public int monsterPass = 0;       //���� Ƚ��
    [SerializeField] GameObject swordPrefebs;        //�˱�
    float size = 1;                             //������
    float index;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*Invoke("Collider", 0.3f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;*/
        dmg = PlayerManager.i.atk;
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * BulletSpeed; //�Ѿ� ���󰡴� �κ�
        Invoke("BulletDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            dmg = 0;
        }
        if (collision.CompareTag("Enemy"))
        {
            if (monsterPass == 0)//���� Ƚ���� 0�϶��� �ı�
            {
                BulletDestory();
                CancelInvoke("BulletDestory");
            }
            else//�ƴ϶�� ���� ���� Ƚ���� �ѹ� ��
            {
                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    //�Ϲ� ���� �Լ� 
    void SpeedUp()//�ؾ�� �̵� �ӵ��� ������ ����
    {
        BulletSpeed *= 1.05f; //5%�� ������
    }

    void atkRateUp()//�ؾ �߻� �ӵ��� ������ ����
    {
        PlayerManager.i.atk_spd *= 0.95f; //5%�� ������
    }

    void atkUp()
    {
        PlayerManager.i.atk += 5f; //5�� ����
    }//�ؾ ���ݷ��� ����

    void passUp()
    {
        monsterPass++;
    }//�ؾ�� ���� Ƚ���� �ø�

    void SizeUp()//�ؾ�� �������� ����
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
    /*public void Collider()   //�浹 Ȱ��ȭ
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }*/
}
