using System.Collections;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    float atkspd;     //���ݼӵ�
    float speed;      //�̵��ӵ�

    Vector3 movement;       //move���� ����� ���� => �̵��� ��ġ�� ����
    Quaternion rotation;    //ȸ����

    bool isShot = true;     //�߻� ���� ����

    [SerializeField]GameObject[] magicPrefebs;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = PlayerManager.i.speed;
        atkspd = PlayerManager.i.atk_spd;
        rotation = Quaternion.Euler(0, 0, 0); //ȸ������ ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
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
        StartCoroutine(ShootCol());
        Bungeo_ppong_PoolManager.i.UseBuneo_ppong(transform.position, rotation);
        Instantiate(magicPrefebs[0], transform.position - new Vector3(1, 0, 0) ,rotation);
        Instantiate(magicPrefebs[1], transform.position - new Vector3(-1, 0, 0), rotation);

    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(atkspd);
        isShot = true;
    }
}
