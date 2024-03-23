/*using System.Collections;
using System.Collections.Generic;*/
using System.Drawing;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static Shield s;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public int passcnt=0;       //관통횟수
    public float dmg;       //방패 데미지
    float size = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        s = this;
    }
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        Bungeo_ppong_BulletComponent.i.monsterPass = 1;
        //Bungeo_ppong_BulletComponent.b.dmg = 0f;
        dmg = Bungeo_ppong_BulletComponent.i.dmg;
        Move();
    }

    public void Move()
    {
        if (rb == null)
        {
            rb = rb.GetComponent<Rigidbody2D>();
        }
        Vector2 shootPos = new Vector2(0, 1);
        rb.velocity = shootPos.normalized * Bungeo_ppong_BulletComponent.i.BulletSpeed; //방어막이 날라가는 부분
        Invoke("ShieldDestory", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(passcnt==0)
            {
                Bungeo_ppong_BulletComponent.i.dmg= PlayerManager.i.atk;
                CancelInvoke("ShieldDestory");
                Destroy(gameObject);
            }
            else
            {
                passcnt--;
            }
        }
    }
    void ShieldDestory()
    {
        Destroy(gameObject);
    }

    void add_passcnt()      //방패 관통횟수 추가
    {
        passcnt++;
    }
    void SizeUp()       //방패 사이즈 증가
    {
        size *= 1.5f;
        transform.localScale = new Vector3(size, size, 1);
    }
}
