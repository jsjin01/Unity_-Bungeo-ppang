using System.Collections;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Seed : StandardEnemy
{
    private Color originalcolor;
    Vector3 target;
    float x1;
    float y1;
    private void Start()
    {
        x1 = Random.Range(-2.3f, 2.3f);
        y1 = Random.Range(0.5f, 2.1f);
        target = new Vector3(x1, y1, 0);
        Move();
        originalcolor = sr.color;
        Invoke("spawnEnemies", 1f);     //발화 시간
    }
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
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 3f*Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Vector3 randomDestination = new Vector3(x1, 0f, y1);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    void spawnEnemies()
    {
        EnemyPoolManager.i.CreateEnemies(transform.position);
        Destroy(gameObject);
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
