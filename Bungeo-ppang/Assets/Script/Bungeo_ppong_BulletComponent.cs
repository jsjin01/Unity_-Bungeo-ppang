using UnityEngine;

public class Bungeo_ppong_BulletComponent : MonoBehaviour
{
    public static Bungeo_ppong_BulletComponent i;       //��ü���� �ٸ��� static?
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float BulletSpeed = 30f;   //�Ѿ� �ӵ�
    [SerializeField] public float dmg;          //���ݷ�
    [SerializeField] public int monsterPass = 0;       //���� Ƚ��
    [SerializeField] GameObject swordPrefebs;        //�˱�
    float size = 1;                             //������


    //public Vector3 collisionPosition;
    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (collision.CompareTag("Enemy"))
        {
            if (monsterPass == 0)//���� Ƚ���� 0�϶��� �ı�
            {
                //collisionPosition = transform.position;
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
        Bungeo_ppong_PoolManager.i.ReturnBungeo_ppong(gameObject);
        Instantiate(swordPrefebs, transform.position, Quaternion.identity);
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

}
