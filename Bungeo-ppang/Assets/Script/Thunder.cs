using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float dmg;        //°ø°Ý·Â

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerManager.i.atk;
        Invoke("ThunderDestory", 0.5f);
    }
    void ThunderDestory()
    {
        Destroy(gameObject);
    }
}
