using System.Collections;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public bool warriorOn = false;    //����� => ���� Ȱ��ȭ
    public float index;     //�˰� ����
    float atkspd;     //���ݼӵ�
    float speed;      //�÷��̾� �̵��ӵ�
    int shoot = 1;     //�߻� Ƚ��

    float firebalRate = 2f;
    float iceballRate = 2f;
    float thunderRate = 2f;

    Vector3 movement;       //move���� ����� ���� => �̵��� ��ġ�� ����
    Quaternion rotation;    //ȸ����

    bool isShot = true;                         //�߻� ���� ����
    bool fireball = false;                      //���̾ ����
    bool iceball = false;                       //���̽��� ����
    bool thunder = false;                       //���� ����
    bool Magic = false;                         //������ ����

    bool gameEnd = false;                      //���� ���� ����

    [SerializeField] GameObject[] magicPrefebs;
    [SerializeField] GameObject shieldPrefebs;

    float x1;
    float x2;

    //���� �ڷ�ƾ ���� ����
    IEnumerator magicCor;
    IEnumerator fireCor;
    IEnumerator iceCor;
    IEnumerator thunderCor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = PlayerManager.i.speed;
        atkspd = PlayerManager.i.atk_spd;
        rotation = Quaternion.Euler(0, 0, 0); //ȸ������ ���� ����
        PlayerManager.i.gameEnd += () =>
        {
            gameEnd = true;
            //�ڷ�ƾ ����
            StopCoroutine(magicCor);
            StopCoroutine(fireCor);
            StopCoroutine(iceCor);
            StopCoroutine (thunderCor);
        };

        magicCor = MagicBall();
        fireCor = Fireball();
        iceCor = Iceball();
        thunderCor = Thunder();
        StartCoroutine(magicCor);
        StartCoroutine(fireCor);
        StartCoroutine(iceCor);
        StartCoroutine(thunderCor);


    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd)
        {
            return;
        }
        warriorOn = PlayerManager.i.isShield;
        shoot = PlayerManager.i.shoot;

        firebalRate = PlayerManager.i.fire_col;
        iceballRate = PlayerManager.i.ice_col;
        thunderRate = PlayerManager.i.thunder_col;

        fireball = PlayerManager.i.fire;
        iceball = PlayerManager.i.ice;
        thunder = PlayerManager.i.thunder;
        Magic = PlayerManager.i.magic;
        if (isShot)
        {
            SoundManger.i.PlaySound(3);
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (gameEnd)
        {
            return;
        }
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
    public IEnumerator WizardSkill()
    {
        for(int i = 0; i < 4; i++)
        {
            x1 = Random.Range(-2.3f, 2.3f);
            x2 = Random.Range(-2.3f, 2.3f);
            ThunderCreat();
            Instantiate(magicPrefebs[0], new Vector3(x1, -4.2f, 0), rotation);
            Instantiate(magicPrefebs[1], new Vector3(x2, -4.2f, 0), rotation);
            yield return new WaitForSeconds(0.1f);
        }
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
        //float x1 = Random.Range(-2.7f, 2.7f);
        //float x2 = Random.Range(-2.7f, 2.7f);
        //float thunderangle = -Mathf.Atan2(x2 - x1, 12f) * Mathf.Rad2Deg;
        //Quaternion rot = Quaternion.Euler(0f, 0f, thunderangle);
        Instantiate(magicPrefebs[2], new Vector3(x1, -1.69f, 0), rotation);
    }

    IEnumerator MagicBall()
    {
        while (true)
        {
            if (Magic) 
            {
                SoundManger.i.PlaySound(7);
                float x1 = Random.Range(-2.7f, 2.7f);
                Instantiate(magicPrefebs[3], new Vector3(x1,-4.2f,0), rotation); 
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Fireball()
    {
        while (true)
        {
            if (fireball)
            {
                SoundManger.i.PlaySound(4);
                float x1 = Random.Range(-2.7f, 2.7f);
                Instantiate(magicPrefebs[0], new Vector3(x1, -4.2f, 0), rotation);
            }
            yield return new WaitForSeconds(firebalRate);
        }
    }

    IEnumerator Iceball()
    {
        while (true)
        {
            if (iceball)
            {
                SoundManger.i.PlaySound(5);
                float x1 = Random.Range(-2.7f, 2.7f);
                Instantiate(magicPrefebs[1], new Vector3(x1, -4.2f, 0), rotation);
            }
            yield return new WaitForSeconds(iceballRate);
        }
    }
    IEnumerator Thunder()
    {
        while (true)
        {
            if (thunder)
            {
                SoundManger.i.PlaySound(6);
                ThunderCreat();
            }
            yield return new WaitForSeconds(thunderRate);
        }
    }

    void shieldMove()
    {
        SoundManger.i.PlaySound(3);
        StartCoroutine(ShootCol());
        Instantiate(shieldPrefebs, transform.position, Quaternion.identity);
    }

    IEnumerator Attackroutine()
    {
        for (int i = 0; i < shoot; i++)
        {
            if (warriorOn)
            {
                Instantiate(shieldPrefebs, transform.position, Quaternion.identity);
            }
            else
            {
                Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, rotation);

            }
            yield return new WaitForSeconds(0.1f);
        }

    }
}
