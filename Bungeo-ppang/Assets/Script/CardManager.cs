using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    /*카드 종류
     * 일반 카드  1번 ~ 6번
     * 전사 붕 카드 7번 ~14번
     * 법사 붕 카드 15번 ~23번
     */
    public static CardManager i;
    [SerializeField] GameObject[] cards;
    int num1;
    int num2;
    int num3;

    //카드 알고리즘 
    List<int> standardSet = new List<int> { 0, 1, 2, 3, 4, 5, 6, 13 };     // 기본으로 나오는 카드이자 계속 추가하고 삭제할 기초가 되는 툴
    
    //6 -> 전사붕 전직
    List<int> W1Set = new List<int> {7, 8, 9}; // 전사붕의 1차 스킬 set 
    //9 -> 반달 검격 전직 -> rm 9
    List<int> W2Set = new List<int> { 10, 11, 12 }; //전사붕의 2차 스킬 set

    //13-> 법사붕 전직
    List<int> M1set = new List<int> {14, 17, 20 }; //법사붕의 1차 스킬 set
    //14 -> fireball
    List<int> fireSet = new List<int> { 15, 16 };
    //17 -> ice ball
    List<int> iceSet = new List<int> { 18, 19 };
    //20 -> thunder
    List<int> thunderSet = new List<int> { 21, 22 };


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

    private void OnEnable()
    {
        if (i == null)
        {
            i = this;
        }
    }
    public void click(int num)
    {
        if (num == 1)
        {
            Debug.Log("붕어빵 달인");
            evt1();
            selectedCard();
        }
        else if (num == 2)
        {
            Debug.Log("배달이요");
            evt2();
            selectedCard();
        }
        else if (num == 3)
        {
            Debug.Log("이건 보너스");
            evt3();
            selectedCard();
        }
        else if (num == 4)
        {
            Debug.Log("따끈따끈 붕어빵");
            evt4();
            selectedCard();
        }
        else if (num == 5)
        {
            Debug.Log("붕어빵 드릴게요");
            evt5();
            selectedCard();
        }
        else if (num == 6)
        {
            Debug.Log("왕 큰 붕어빵");
            evt6();
            selectedCard();
        }
        else if (num == 7)
        {
            Debug.Log("전사붕");
            standardSet.Remove(6);          //전사붕 선택창 삭제
            standardSet.AddRange(W1Set);    //전사붕 스킬 개방
            evt7();
            selectedCard();
        }
        else if (num == 8)
        {
            Debug.Log("단단한 방패");
            evt8();
            selectedCard();
        }
        else if (num == 9)
        {
            Debug.Log("방패 강화");
            evt9();
            selectedCard();
        }
        else if (num == 10)
        {
            Debug.Log("반달 검격");
            standardSet.Remove(9);       //전사붕 검격 삭제
            standardSet.AddRange(W2Set); //검격 업데이트
            evt10();
            selectedCard();
        }
        else if (num == 11)
        {
            Debug.Log("검격 강화");
            evt11();
            selectedCard();
        }
        else if (num == 12)
        {
            Debug.Log("연속 검격");
            evt12();
            selectedCard();
        }
        else if (num == 13)
        {
            Debug.Log("관통 검격");
            evt13();
            selectedCard();
        }
        else if (num == 14)
        {
            Debug.Log("법사붕");
            standardSet.Remove(13);         //법사붕 선택지 삭제
            standardSet.AddRange(M1set);    //법사붕 관련 선택지 증가
            //evt14();
            cards[num1].SetActive(false);
            cards[num2].SetActive(false);
            cards[num3].SetActive(false);

            cards[14].SetActive(true);
            cards[17].SetActive(true);
            cards[20].SetActive(true);

        }
        else if (num == 15)
        {
            Debug.Log("파이어 볼");
            standardSet.Remove(15);         //파이어볼 선택지 제거
            standardSet.AddRange(fireSet);  //파이어볼 관련 카드 추가
            evt15();
            selectedCard();
        }
        else if (num == 16)
        {
            Debug.Log("마법 강화 : 불");
            evt16();
            selectedCard();
        }
        else if (num == 17)
        {
            Debug.Log("캐스팅 감소 : 불");
            evt17();
            selectedCard();
        }
        else if (num == 18)
        {
            Debug.Log("아이스 볼");
            standardSet.Remove(17);         //아이스볼 선택지 제거
            standardSet.AddRange(iceSet);   //아이스볼 관련 카드 추가
            evt18();
            selectedCard();
        }
        else if (num == 19)
        {
            Debug.Log("마법 강화 : 얼음");
            evt19();
            selectedCard();
        }
        else if (num == 20)
        {
            Debug.Log("캐스팅 감소 : 얼음");
            evt20();
            selectedCard();
        }
        else if (num == 21)
        {
            Debug.Log("썬더 볼트");
            standardSet.Remove(20);             //번개 선택지 제거
            standardSet.AddRange(thunderSet);   //번개 관련 카드 추가
            evt21();
            selectedCard();
        }
        else if (num == 22)
        {
            Debug.Log("깨달음 : 번개");
            evt22();
            selectedCard();
        }
        else if (num == 23)
        {
            Debug.Log("캐스팅 감소 : 번개");
            evt23();
            selectedCard();
        }
    } //버튼 기능


    public void CardDraw() //카드 뽑기 알고리즘
    {
        System.Random random = new System.Random();
        while (true)
        {
            num1 = standardSet[random.Next(standardSet.Count)];
            num2 = standardSet[random.Next(standardSet.Count)];
            num3 = standardSet[random.Next(standardSet.Count)];
            if (num1 != num2 && num2 != num3 && num1 != num3)
            {
                break;
            }
        }
        cards[num1].SetActive(true);
        cards[num2].SetActive(true);
        cards[num3].SetActive(true);
    }

    void selectedCard()
    {
        cards[num1].SetActive(false);
        cards[num2].SetActive(false);
        cards[num3].SetActive(false);
        Time.timeScale = 1f;
    }
}
