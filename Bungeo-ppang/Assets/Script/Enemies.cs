using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Enemy
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
    private void OnDisable()
    {
        if (gameObject.CompareTag("Peanut"))
        {
            Instantiate(peanutslice_Prefebs, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
            Instantiate(peanutslice_Prefebs, transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
        }
    }
    public override IEnumerator Hitchange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalcolor;
    }
}
