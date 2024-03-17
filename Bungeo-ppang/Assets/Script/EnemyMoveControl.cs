using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMoveControl : MonoBehaviour
{
    public float speed = 2f; // 적의 이동 속도
    [SerializeField]
    private float hp = 1f;
    
    
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Bungeo_ppong_BulletComponent bullet=other.gameObject.GetComponent<Bungeo_ppong_BulletComponent>();
            hp -= bullet.dmg;
            if(hp <= 0f)
            {
                Destroy (gameObject);
            }

            Destroy(other.gameObject);
        }
    }
    
}

