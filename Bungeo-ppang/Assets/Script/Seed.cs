using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Seed : Enemy
{
    private Color originalcolor;
    private void Start()
    {
        Move();
        originalcolor = sr.color;
        Invoke("EnemyDestroy", 2f);
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
    public override void EnemyDestroy() //적 삭제
    {
        hp = 20f; //나중에 다시 사용할 때 Hp 100
        Destroy(gameObject);
        EnemyPoolManager.i.CreateEnemies(transform.position);
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
