using System.Collections;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public bool warriorOn = false;    //전사붕
    public float index;     //검격 각도
    float atkspd;     //공격속도
    float speed;      //플레이어 이동속도
    int shoot = 1;     //발사 횟수

    float firebalRate = 3f;
    float iceballRate = 3f;
    float thunderRate = 3f;

    Vector3 movement;       //move에서 사용할 변수 => 이동할 위치의 변수
    Quaternion rotation;    //회전값

    bool isShot = true;     //발사 가능 변수

    [SerializeField] GameObject[] magicPrefebs;
    [SerializeField] GameObject shieldPrefebs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = PlayerManager.i.speed;
        atkspd = PlayerManager.i.atk_spd;
        rotation = Quaternion.Euler(0, 0, 0); //회전하지 않은 상태
        StartCoroutine(Fireball());
        StartCoroutine(Thunder());
        StartCoroutine(Iceball());
    }

    // Update is called once per frame
    void Update()
    {

        shoot = PlayerManager.i.shoot;

        if (warriorOn)
        {
            if (isShot)
            {
                shieldMove();
            }

        }
        else
        {
            if (isShot)
            {
                Attack();
            }

        }
    }

    private void FixedUpdate()
    {
        Move(Input.GetAxisRaw("Horizontal"));
    }

    void Move(float x)
    {
        movement.Set(x, 0, 0);
        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    void Attack()
    {
        StartCoroutine(Attackroutine());
        StartCoroutine(ShootCol());
    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(atkspd);
        isShot = true;
    }

    void ThunderCreat()
    {
        float x1 = Random.Range(-2.7f, 2.7f);
        float x2 = Random.Range(-2.7f, 2.7f);
        float thunderangle = -Mathf.Atan2(x2 - x1, 12f) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, thunderangle);
        Instantiate(magicPrefebs[2], new Vector3(x1, -4, 0), rot);
    }

    IEnumerator Fireball()
    {
        while (true)
        {
            Instantiate(magicPrefebs[0], transform.position - new Vector3(1, 0, 0), rotation);
            yield return new WaitForSeconds(firebalRate);
        }
    }

    IEnumerator Iceball()
    {
        while (true)
        {
            Instantiate(magicPrefebs[1], transform.position - new Vector3(-1, 0, 0), rotation);
            yield return new WaitForSeconds(iceballRate);
        }
    }
    IEnumerator Thunder()
    {
        while (true)
        {
            ThunderCreat();
            yield return new WaitForSeconds(thunderRate);
        }
    }

    void ColDown(int a)
    {
        if (a == 0)
        {
            firebalRate -= 1f;
        }
        else if (a == 1)
        {
            iceballRate -= 1f;
        }
        else if (a == 2)
        {
            thunderRate -= 1f;
        }
    }
    void shieldMove()
    {
        StartCoroutine(ShootCol());
        Instantiate(shieldPrefebs, transform.position, Quaternion.identity);
    }

    IEnumerator Attackroutine()
    {
        for (int i = 0; i < shoot; i++)
        {
            Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, rotation);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
