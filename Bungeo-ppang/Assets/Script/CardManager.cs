using System;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager i;
    public Action evt1;
    public Action evt2;
    public Action evt3;
    public Action evt4;
    public Action evt5;
    public Action evt6;
    public Action evt7;
    public Action evt8;
    public Action evt9;
    public Action evt10;
    public Action evt11;
    public Action evt12;
    public Action evt13;
    public Action evt14;
    public Action evt15;
    public Action evt16;
    public Action evt17;
    public Action evt18;
    public Action evt19;
    public Action evt20;
    public Action evt21;
    public Action evt22;
    public Action evt23;
    public Action evt24;

    private void OnEnable()
    {
        i = this;
    }
    public void click(int num)
    {
        if (num == 1)
        {
            Debug.Log("붕어빵 달인");
            evt1();
        }
        else if (num == 2)
        {
            Debug.Log("배달이요");
            evt2();
        }
        else if (num == 3)
        {
            Debug.Log("이건 보너스");
            evt3();
        }
        else if (num == 4)
        {
            Debug.Log("따끈따끈 붕어빵");
            evt4();
        }
        else if (num == 5)
        {
            Debug.Log("붕어빵 드릴게요");
            evt5();
        }
        else if (num == 6)
        {
            Debug.Log("왕 큰 붕어빵");
            evt6();
        }
        else if (num == 7)
        {
            Debug.Log("전사붕");
            evt7();
        }
        else if (num == 8)
        {
            Debug.Log("단단한 방패");
            evt8();
        }
        else if (num == 9)
        {
            Debug.Log("방패 강화");
            evt9();
        }
        else if (num == 10)
        {
            Debug.Log("반달 검격");
            evt10();
        }
        else if (num == 11)
        {
            Debug.Log("검격 강화");
            evt11();
        }
        else if (num == 12)
        {
            Debug.Log("연속 검격");
            evt12();
        }
        else if (num == 13)
        {
            Debug.Log("관통 검격");
            evt13();
        }
        else if (num == 14)
        {
            Debug.Log("전장 외침");
            evt14();
        }
        else if (num == 15)
        {
            Debug.Log("법사붕");
            evt15();
        }
        else if (num == 16)
        {
            Debug.Log("파이어 볼");
            evt16();
        }
        else if (num == 17)
        {
            Debug.Log("마법 강화 : 불");
            evt17();
        }
        else if (num == 18)
        {
            Debug.Log("캐스팅 감소 : 불");
            evt18();
        }
        else if (num == 19)
        {
            Debug.Log("아이스 볼");
            evt19();
        }
        else if (num == 20)
        {
            Debug.Log("마법 강화 : 얼음");
            evt20();
        }
        else if (num == 21)
        {
            Debug.Log("캐스팅 감소 : 얼음");
            evt21();
        }
        else if (num == 22)
        {
            Debug.Log("썬더 볼트");
            evt22();
        }
        else if (num == 23)
        {
            Debug.Log("깨달음 : 번개");
            evt23();
        }
        else if (num == 24)
        {
            Debug.Log("캐스팅 감소 : 번개");
            evt24();
        }
    }
}
