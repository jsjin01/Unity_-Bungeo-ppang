using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;

    [SerializeField] GameObject Enemy_Prefeds; //프리팹
    [SerializeField] int initEnemyCount = 50; //초기 생성량

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatEnemy(initEnemyCount);
    }

    public void CreatEnemy(int cnt = 5)              //적 생성 부분
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(Enemy_Prefeds, transform);   //적 생성
        }
    }

    public void UseEnemy(Vector2 p, Quaternion rot)   //적 사용하는 부분
    {
        if (transform.childCount == 0)
        {
            CreatEnemy(); //적이 없다면 생성
        }

        Enemy e = transform.GetChild(0).GetComponent<Enemy>(); //pool 0번째 오브젝트에 접근

        e.transform.position = p;         //적 위치 설정
        e.transform.rotation = rot;       //적 각도 설정
        e.gameObject.SetActive(true);     //옵젝 활성화
        e.transform.parent = null;        //부모 설정 해제
        e.Move();                         //움직임 구현
    }

    public void ReturnEnemy(GameObject e)
    {
        e.gameObject.SetActive(false);
        e.transform.SetParent(transform);
        //사용 후 다시 pool안으로 가져옴
    }
}
