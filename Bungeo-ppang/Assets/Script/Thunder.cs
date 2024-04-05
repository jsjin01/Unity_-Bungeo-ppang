using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float dmg;        //공격력
    [SerializeField] public float stunTime = 0.5f;   //경직 시간

    void Start()
    {
        dmg = PlayerManager.i.atk;
        Invoke("ThunderDestory", 0.5f);
    }
    void ThunderDestory()
    {
        Destroy(gameObject);
    }
}
