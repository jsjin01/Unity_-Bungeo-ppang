using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [Header ("플레이어 상태창")] 
    [SerializeField] public float atk = 10;        //공격력
    [SerializeField] public float atk_spd = 0.1f;  //공격 속도
    [SerializeField] public float cri = 5;         //치명타
    [SerializeField] public float speed = 10f;     //이동속도
    [SerializeField] public float mhp = 100;       //최대 체력

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
