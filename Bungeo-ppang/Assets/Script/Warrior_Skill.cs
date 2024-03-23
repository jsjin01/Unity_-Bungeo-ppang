using System.Collections;
using UnityEngine;

public class Warrior_Skill : MonoBehaviour
{
    public static Warrior_Skill w;
    [SerializeField] GameObject shieldPrefebs;
    [SerializeField] GameObject swordPrefebs;
    [SerializeField] public bool warriorOn = false;    //전사붕
    [SerializeField] bool swordOn = false;    //검격(반달검격)
    bool isShot = true;
    public float index;
    public float radian;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (warriorOn)
        {
            if (isShot)
            {
                shieldMove();
            }
            
        }
        if (swordOn)
        {
            if (isShot)
            {
                swordMove();
            }
        }

    }

    void shieldMove()
    {
        StartCoroutine(ShootCol());
        Instantiate(shieldPrefebs, transform.position, Quaternion.identity);
    }
    void swordMove()
    {
        StartCoroutine(ShootCol());

        index = Random.Range(0f, 360f);
        radian = index * Mathf.Deg2Rad;

        Instantiate(swordPrefebs, transform.position, Quaternion.Euler(0, 0, index));
    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(PlayerManager.i.atk_spd);
        isShot = true;
    }

}
