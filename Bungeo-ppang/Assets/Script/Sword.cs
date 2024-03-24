using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public static Sword i;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public int passcnt = 0;         //관통횟수
    [SerializeField] public int numberOfSwords = 1;  //검격의 수
    [SerializeField] float SwordSpeed = 20f;  //검격 속도
    public float dmg;       //검격 데미지
    float size = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        //i = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = Bungeo_ppong_BulletComponent.i.dmg;
        //Move();
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
        /*if (Warrior_Skill.w != null && Warrior_Skill.w.radian >= 0 && Warrior_Skill.w.radian < Mathf.PI * 2)
        {
            Vector2 shootPos = new Vector2(Mathf.Cos(Warrior_Skill.w.radian), Mathf.Sin(Warrior_Skill.w.radian));
            rb.velocity = shootPos.normalized * SwordSpeed; //검격이 날라가는 부분
            Invoke("SwordDestory", 3);
        }*/
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
