using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public int Hp = 3;              //���
    [SerializeField] public float atk = 10;          //���ݷ�
    [SerializeField] public float atk_spd = 10f;     //���� �ӵ�
    [SerializeField] public float speed = 10f;       //�÷��̾� �̵��ӵ�
    
    //�ؾ ����
    [SerializeField] public float bulletSpeed = 15f;     // �ؾ �ӵ�
    [SerializeField] public int shoot = 1;               //�߻�Ƚ��
    [SerializeField] public int pass = 0;                //����Ƚ��
    [SerializeField] public float size = 0.08f;          //�ؾ ������

    //����
    [SerializeField] public bool isShield = false;      //���� �� ���� && ����� Ȱ��ȭ
    [SerializeField] public float dep_atk = 20;         //���� ���ݷ�
    [SerializeField] public int dep_pass = 0;           //���� �����

    //�˱�
    [SerializeField] public bool isSword = false;       //�˱� �¿���
    [SerializeField] public int sword_dmg = 15;         //�˱� ������
    [SerializeField] public int swords = 1;             //�˱� ����
    [SerializeField] public int sword_pass = 0;         //�˱� ���� ����

    //�����
    [SerializeField] public bool magic = false;        //����� Ȱ��ȭ 
    [SerializeField] public bool fire = false;         //���̾ Ȱ��ȭ
    [SerializeField] public bool ice = false;          //���̽��� Ȱ��ȭ
    [SerializeField] public bool thunder = false;      //���� Ȱ��ȭ

    //���̾
    [SerializeField] public float fire_dmg = 10;       //���̾ ������
    [SerializeField] public float fire_col = 5f;       //���̾ ��Ÿ��

    //���̽���
    [SerializeField] public float ice_dmg = 10;        //���̽��� ������
    [SerializeField] public float ice_col = 5f;        //���̽��� ��Ÿ��

    //����
    [SerializeField] public float thunder_dmg = 10;    //���� ������
    [SerializeField] public float thunder_col = 5f;    //���� ��Ÿ��


    public Action gameEnd;  //���� �� => over or Boss óġ
    public Action boss;     //���� ó��
    bool Gend = false;      //���� �� �Ǵ� ����
    [SerializeField]GameObject gameover;
    [SerializeField]GameObject gameClear;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CardManager.i.evt1 += () =>
        {
            atk_spd *= 0.8f;
            Debug.Log("���� �ӵ� ����");
        };

        CardManager.i.evt2 += () =>
        {
            bulletSpeed *= 1.3f;
            Debug.Log("speed up");
        };

        CardManager.i.evt3 += () =>
        {
            shoot += 1;
            atk -= 2;
            Debug.Log("1ȸ �߰�");
        };

        CardManager.i.evt4 += () =>
        {
            atk += 5;
            Debug.Log("Dmg ����");
        };

        CardManager.i.evt5 += () =>
        {
            pass += 1;
            Debug.Log("pass����");
        };

        CardManager.i.evt6 += () =>
        {
            size *= 1.5f;
            Debug.Log("size up");
        };

        CardManager.i.evt7 += () =>
        {
            isShield = true;        //���� Ȱ��ȭ
            Debug.Log("shield");
        };

        CardManager.i.evt8 += () =>
        {
            dep_pass += 1;
            Debug.Log("dep_pass");
        };

        CardManager.i.evt9 += () =>
        {
            dep_atk += 10;
            Debug.Log("dep_atk");
        };

        CardManager.i.evt10 += () =>
        {
            isSword = true;
            Debug.Log("sword on");
        };

        CardManager.i.evt11 += () =>
        {
            sword_dmg += 10;
            Debug.Log("sword dmg");
        };

        CardManager.i.evt12 += () =>
        {
            swords += 1;
            Debug.Log("swords");
        };

        CardManager.i.evt13 += () =>
        {
            sword_pass += 1;
            Debug.Log("sword pass");
        };

        CardManager.i.evt14 += () =>
        {
            magic = true;
        };

        CardManager.i.evt15 += () =>
        {
            fire = true;
        };

        CardManager.i.evt16 += () =>
        {
            fire_dmg += 20;
        };

        CardManager.i.evt17 += () =>
        {
            fire_col *= 0.8f;
        };

        CardManager.i.evt18 += () =>
        {
            ice = true;
        };

        CardManager.i.evt19 += () =>
        {
            ice_dmg += 20;
        };

        CardManager.i.evt20 += () =>
        {
            ice_col *= 0.8f;
        };

        CardManager.i.evt21 += () =>
        {
            thunder = true;
        };

        CardManager.i.evt22 += () =>
        {
            thunder_dmg += 20;
        };

        CardManager.i.evt23 += () =>
        {
            thunder_col *= 0.8f;
        };

    }
    private void Update()
    {
        UIManager.i.SetHp(Hp);
        if(Hp <= 0)
        {
            if (Gend)
            {
                return;
            }
            Time.timeScale = 0f;
            gameEnd(); //���� ��
            gameover.SetActive(true);
            Gend = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Hp -= 1;
            Debug.Log("Hit");
            UIManager.i.GaugeBar_Warrior.value += 0.4f;
            UIManager.i.GaugeBar_Wizard.value += 0.4f;
        }
    }
}
