using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static Sword s;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int passcnt = 0;         //����Ƚ��
    [SerializeField] int numberOfSwords = 0;  //�˰��� ��
    [SerializeField] float SwordSpeed = 50f;  //�˰� �ӵ�
    public Quaternion rotation;
    public float dmg;       //�˰� ������
    float size = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        s = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = Bungeo_ppong_BulletComponent.b.dmg;
        chooseRotation();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * SwordSpeed; //�˰��� ���󰡴� �κ�
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

    void chooseRotation()     //�˰� ���� ����
    {
        /*rotation[0] = Quaternion.Euler(0, 0, 0);
        rotation[1] = Quaternion.Euler(0, 45, 0);
        rotation[2] = Quaternion.Euler(0, 90, 0);
        rotation[3] = Quaternion.Euler(0, 270, 0);
        rotation[4] = Quaternion.Euler(0, 315, 0);
        int index = Random.Range(0, 5);
        return rotation[index];*/
        int index = Random.Range(0, 361);
        rotation=Quaternion.Euler(0, index, 0);
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
