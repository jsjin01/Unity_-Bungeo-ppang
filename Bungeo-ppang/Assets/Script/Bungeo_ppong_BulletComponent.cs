//using System;
using System.Collections;
using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cd;
    [SerializeField] Sprite[] bungs;
    [SerializeField] SpriteRenderer sr;

    float BulletSpeed;                          //�Ѿ� �ӵ�
    public float dmg;                           //���ݷ�
    public int monsterPass = 0;                 //���� Ƚ��
    int maxMonsterPass = 0;                     //�ִ� ����Ƚ��
    [SerializeField] GameObject swordPrefebs;   //�˱�
    float size = 0.08f;                         //������
    float index;

    int swords = 1;                             //�˱� ����
    [SerializeField] bool isSword = false;      //�˱� �¿���
    public bool isShield = false;               //���� �����϶�

    public bool magic;                          //�����
    public bool warrior;                        //�����

    IEnumerator passCor;                        //���� �ڷ�ƾ
    void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
    }

    private void OnEnable()//�ٽ� �������� �� �ʱ�ȭ
    {
        BulletSpeed = PlayerManager.i.bulletSpeed;
        dmg = PlayerManager.i.atk;
        maxMonsterPass = PlayerManager.i.pass;
        size = PlayerManager.i.size;
        transform.localScale = new Vector3(size, size, 1);

        isSword =  PlayerManager.i.isSword;
        swords = PlayerManager.i.swords;  
        
        magic = PlayerManager.i.magic;
        warrior = PlayerManager.i.isShield;

        if (magic)
        {
            sr.sprite = bungs[2];
        }
        else if (warrior)
        {
            sr.sprite= bungs[1];
        }

        //�ʱ�ȭ
        monsterPass = maxMonsterPass;
        if (isShield)
        {
            if (cd == null)
            {
                cd = GetComponent<CapsuleCollider2D>();
            }
            cd.enabled = false;
            Invoke("MonsterPass", 0.1f);
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
        if (collision.CompareTag("Enemy"))
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
                Invoke("MonsterPass", 0.25f);
                monsterPass--;
            }
        }
    }

    public void BulletDestory()
    {
        if (isSword)//�˱� ��ȯ
        {
            SoundManger.i.PlaySound(13);
            for (int i = 0; i < swords; i++)
            {
                SwordCreat();
            }
        }
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
    }

    public void SwordCreat()
    {
        index = Random.Range(0f, 360f);
        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
    }

    void MonsterPass()//���� ����
    {
        cd.enabled = true;
    }
}
