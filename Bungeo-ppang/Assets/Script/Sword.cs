using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static Sword s;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int passcnt = 0;         //관통횟수
    [SerializeField] int numberOfSwords = 0;  //검격의 수
    [SerializeField] float SwordSpeed = 50f;  //검격 속도
    public Quaternion rotation;
    public float dmg;       //검격 데미지
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
        rb.velocity = shootPos.normalized * SwordSpeed; //검격이 날라가는 부분
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

    void chooseRotation()     //검격 각도 고르기
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
    void add_passcnt()      //검격 관통횟수 추가
    {
        passcnt++;
    }
    void addSword()     //검격 개수 증가
    {
        numberOfSwords++;
    }
    void SizeUp()       //검격 사이즈 증가
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
}
