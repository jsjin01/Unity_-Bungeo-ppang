using UnityEngine;

public class Bungeo_ppong_PoolManager : MonoBehaviour
{
    public static Bungeo_ppong_PoolManager i;
    [SerializeField] GameObject bungeo_ppong_Prefeds; //프리팹
    [SerializeField] int initBungeo_ppongCount = 30; //초기 생성량

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatBungeo_ppong(initBungeo_ppongCount);
    }

    public void CreatBungeo_ppong(int cnt = 1)              //붕어빵 생성 부분
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(bungeo_ppong_Prefeds, transform);   //붕어빵 생성
        }
    }

    public void UseBuneo_ppong(Vector2 p, Quaternion rot)   //붕어빵 사용하는 부분
    {
        if (transform.childCount == 0)
        {
            CreatBungeo_ppong(); //붕어빵이 없다면 생성
        }

        Bungeo_ppong_BulletComponent bbp = transform.GetChild(0).GetComponent<Bungeo_ppong_BulletComponent>(); //pool 0번째 오브젝트에 접근
        
        bbp.transform.position = p;         //붕어빵 위치 설정
        bbp.transform.rotation = rot;       //붕어빵 각도 설정
        bbp.gameObject.SetActive(true);     //옵젝 활성화
        bbp.transform.parent = null;        //부모 설정 해제
        bbp.Move();                         //움직임 구현
    }

    public void ReturnBungeo_ppong(GameObject bbp)
    {
        bbp.gameObject.SetActive(false);
        bbp.transform.SetParent(transform);
        //사용 후 다시 pool안으로 가져옴
    }
}
