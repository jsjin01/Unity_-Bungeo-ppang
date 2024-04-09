using System.Collections;
using UnityEngine;

public class SmallPeanut : StandardEnemy
{
    [SerializeField] GameObject peanutslice_Prefebs;
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
        base.EnemyDestroy();
    }
    public override IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalcolor;
    }
}
