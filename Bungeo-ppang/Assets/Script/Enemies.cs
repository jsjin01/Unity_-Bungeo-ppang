using System.Collections;
using UnityEngine;

public class Enemies : Enemy
{
    private Color originalcolor;
    private void Start()
    {
        Move();
        originalcolor = sr.color;

    }
    // Update is called once per frame
    public override void OnEnable()
    {
        hp = MaxHp;
    }
    public override void Move()
    {
        base.Move();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    public override void EnemyDestroy()
    {
        UIManager.i.GaugeBar.value += 0.1f;
        hp = 100f; //나중에 다시 사용할 때 Hp 100
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
