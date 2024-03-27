//using System;
using System.Collections;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField]Collider2D cd;
    [SerializeField] public float BulletSpeed = 30f;   //�Ѿ� �ӵ�
    public float dmg;          //���ݷ�
    public int monsterPass = 0;       //���� Ƚ��
    int maxMonsterPass = 2;          //�ִ� ����Ƚ��
    [SerializeField] GameObject swordPrefebs;        //�˱�
    float size = 1;                             //������
    float index;
    bool isSword = false;                       //�˱� �¿���

    IEnumerator passCor; //���� �ڷ�ƾ
    void Start()
    {
        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        /*Invoke("Collider", 0.3f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;*/
        dmg = PlayerManager.i.atk;
    }

    private void OnEnable()
    {
        if(cd = null)
        {
            cd = GetComponent<Collider2D>();    
        }
        monsterPass = maxMonsterPass;
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
                if(passCor == null)
                {
                    passCor = MonsterPass();
                    StartCoroutine(passCor);
                }
                else
                {
                    StartCoroutine(passCor);
                }

                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        if (isSword)//�˱� ��ȯ
        {
            SwordCreat();
        }
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    public void SwordCreat()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
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

    IEnumerator MonsterPass()
    {
        cd.enabled = false;
        yield return new WaitForSeconds(0.1f);
        cd.enabled = true;
    }
   //public void Collider()   //�浹 Ȱ��ȭ
   // {
   //     gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
   // }
}
