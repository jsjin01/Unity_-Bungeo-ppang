using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;
    Rigidbody2D rb;
    [SerializeField] public float atk { get; private set; } = 10;        //공격력
    [SerializeField] public float atk_spd { get; private set; } = 0.1f;  //공격 속도
    [SerializeField] public float cri { get; private set; } = 5;         //치명타
    [SerializeField] public float speed { get; private set; } = 10f;     //이동속도
    [SerializeField] public float mhp { get; private set; } = 100f;       //최대 체력

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
