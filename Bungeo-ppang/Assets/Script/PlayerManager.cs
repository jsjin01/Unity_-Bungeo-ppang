using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [Header ("�÷��̾� ����â")] 
    [SerializeField] public float atk = 10;        //���ݷ�
    [SerializeField] public float atk_spd = 0.1f;  //���� �ӵ�
    [SerializeField] public float cri = 5;         //ġ��Ÿ
    [SerializeField] public float speed = 10f;     //�̵��ӵ�
    [SerializeField] public float mhp = 100;       //�ִ� ü��

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
