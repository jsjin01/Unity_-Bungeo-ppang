using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager i;
    RawImage hp1;
    RawImage hp2;
    RawImage hp3;
    Text stageT;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        hp1 = GameObject.Find("Hp 1").GetComponent<RawImage>();
        hp2 = GameObject.Find("Hp 2").GetComponent<RawImage>();
        hp3 = GameObject.Find("Hp 3").GetComponent<RawImage>();
        stageT = GameObject.Find("StageText").GetComponent<Text>();
    }

    public void SetHp(int hp)
    {
        if (hp == 3)
        {
            hp1.color = Color.red;
            hp2.color = Color.red;
            hp3.color = Color.red;
        }
        else if (hp == 2)
        {
            hp1.color = Color.red;
            hp2.color = Color.red;
            hp3.color = Color.black;
        }
        else if (hp == 1)
        {
            hp1.color = Color.red;
            hp2.color = Color.black;
            hp3.color = Color.black;
        }
        else
        {
            hp1.color = Color.black;
            hp2.color = Color.black;
            hp3.color = Color.black;
        }
    } //체력 UI이 설정

    public void SetStage(int num)
    {
        stageT.text = "STAGE : " + num;
    }

}
