using System.Collections;
using UnityEngine;

public class Warrior_Skill : MonoBehaviour
{
    [SerializeField] GameObject shieldPrefebs;
    [SerializeField] public bool warriorOn = false;    //ภüป็บุ
    bool isShot = true;
    public float index;
    // Start is called before the first frame update
    

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
    }

    void shieldMove()
    {
        StartCoroutine(ShootCol());
        Instantiate(shieldPrefebs, transform.position, Quaternion.identity);
    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(PlayerManager.i.atk_spd);
        isShot = true;
    }

}
