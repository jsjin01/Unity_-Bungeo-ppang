using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;

    [SerializeField] GameObject Enemy_Prefeds; //������
    [SerializeField] GameObject Boss_Prefeds; //�� ������
    [SerializeField] GameObject Seed_Prefeds;
    [SerializeField] GameObject[] Enemies_Prefeds;
    [SerializeField] int initEnemyCount = 50; //�ʱ� ������

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatEnemy(initEnemyCount);
    }

    public void CreatEnemy(int cnt = 5)              //�� ���� �κ�
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(Enemy_Prefeds, transform);   //�� ����
        }
    }


    public void UseEnemy(Vector2 p, Quaternion rot)   //�� ����ϴ� �κ�
    {
        if (transform.childCount == 0)
        {
            CreatEnemy(); //���� ���ٸ� ����
        }

        Enemy e = transform.GetChild(0).GetComponent<Enemy>(); //pool 0��° ������Ʈ�� ����

        e.transform.position = p;         //�� ��ġ ����
        e.transform.rotation = rot;       //�� ���� ����
        e.gameObject.SetActive(true);     //���� Ȱ��ȭ
        e.transform.parent = null;        //�θ� ���� ����
        e.Move();                         //������ ����
    }

    public void ReturnEnemy(GameObject e)
    {
        e.gameObject.SetActive(false);
        e.transform.SetParent(transform);
        //��� �� �ٽ� pool������ ������
    }
    public void CreatBoss()
    {
        Instantiate(Boss_Prefeds, new Vector3(0, 3.5f, 0), Quaternion.identity);   //���� ����
    }
    public void CreateSeed()
    {
        float x1 = Random.Range(-2.3f, 2.3f);
        float y1 = Random.Range(0.5f, 2.1f);
        Instantiate(Seed_Prefeds, new Vector3(x1, y1, 0), Quaternion.identity);
        float x2 = Random.Range(-2.3f, 2.3f);
        float y2 = Random.Range(0.5f, 2.1f);
        Instantiate(Seed_Prefeds, new Vector3(x2, y2, 0), Quaternion.identity);
        float x3 = Random.Range(-2.3f, 2.3f);
        float y3 = Random.Range(0.5f, 2.1f);
        Instantiate(Seed_Prefeds, new Vector3(x3, y3, 0), Quaternion.identity);
    }
    public void CreateEnemies(Vector3 Pos)
    {
        int i = Random.Range(0, 3);
        Instantiate(Enemies_Prefeds[i], Pos, Quaternion.identity);
    }
    public GameObject returnEnemies(int i)
    {
        return Enemies_Prefeds[(int)i];
    }
}
