using System.Collections;
using UnityEngine;

public class Sunflower : Enemy
{
    private Color originalcolor;
    private void Start()
    {
        Invoke("Move", 1f);
        originalcolor = sr.color;
        Invoke("EnemyDestroy", 10f);
    }
    // Update is called once per frame
    public override void OnEnable()
    {
        hp = MaxHp;
    }
    public override void Move()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        rb.velocity = pos.normalized * speed;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    public override void EnemyDestroy() //적 삭제
    {
        UIManager.i.GaugeBar.value += 0.1f;
        hp = 20f; //나중에 다시 사용할 때 Hp 100
        Destroy(gameObject);
        if (fireCor != null)
        {
            StopCoroutine(fireCor);
        }
        if (iceCor != null)
        {
            StopCoroutine(iceCor);
        }
        if (thunderCor != null)
        {
            StopCoroutine(thunderCor);
        }
    }
    public override IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalcolor;
    }
}
