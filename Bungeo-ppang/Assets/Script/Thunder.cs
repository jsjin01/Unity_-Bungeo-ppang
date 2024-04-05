using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float dmg;        //���ݷ�
    [SerializeField] public float stunTime = 0.5f;   //���� �ð�

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
