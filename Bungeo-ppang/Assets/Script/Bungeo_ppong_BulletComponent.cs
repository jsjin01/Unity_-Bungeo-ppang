//using System;
using System.Collections;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cd;
    [SerializeField] public float BulletSpeed = 30f;   //�Ѿ� �ӵ�
    public float dmg;          //���ݷ�
    public int monsterPass = 0;       //���� Ƚ��
    int maxMonsterPass = 0;          //�ִ� ����Ƚ��
    [SerializeField] GameObject swordPrefebs;        //�˱�
    float size = 1;                             //������
    float index;
    bool isSword = false;                       //�˱� �¿���
    [SerializeField]public bool isShield = false;               //���� �����϶�

    IEnumerator passCor; //���� �ڷ�ƾ
    void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
    }

    private void OnEnable()//�ٽ� �������� �� �ʱ�ȭ
    {
        monsterPass = maxMonsterPass;
        if (isShield)
        {
        if(cd == null)
            {
                cd = GetComponent<CapsuleCollider2D>();
            }
            cd.enabled = false;
            Invoke("MonsterPass", 0.2f);
        }
    }
    public void Move() //������
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
        if (collision.CompareTag("Enemy")||collision.CompareTag("Peanut"))
        {
            if (monsterPass == 0)//���� Ƚ���� 0�϶��� �ı�
            {
                BulletDestory();
                CancelInvoke("BulletDestory");
            }
            else//�ƴ϶�� ���� ���� Ƚ���� �ѹ� ��
            {
                if (cd == null)
                {
                    cd = GetComponent<CapsuleCollider2D>();
                }
                cd.enabled = false;
                Invoke("MonsterPass", 0.2f);
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

    void MonsterPass()//���� ����
    {
        cd.enabled = true;
    }


}
