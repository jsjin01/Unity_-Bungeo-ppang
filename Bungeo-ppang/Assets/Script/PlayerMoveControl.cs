using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float speed = 5f;      //�̵��ӵ�

    Vector3 movement;                       //move���� ����� ���� => �̵��� ��ġ�� ����
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {  
        Move(Input.GetAxisRaw("Horizontal"));
    }

    void Move(float x)
    {
            movement.Set(x, 0, 0);
            movement = movement.normalized * speed * Time.deltaTime;
        
        if (rb.transform.position.x < -2.6 ) 
        {
            rb.transform.position = new Vector3( -2.6f, -5.5f, 0);
        }
        else if(rb.transform.position.x > 2.6)
        {
            rb.transform.position = new Vector3(2.6f, -5.5f, 0);
        }
        else
        {
            rb.MovePosition(transform.position + movement);
        }
   

    }


}
