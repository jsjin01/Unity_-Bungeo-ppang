using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    /*ī�� ����
     * �Ϲ� ī��  1�� ~ 6��
     * ���� �� ī�� 7�� ~14��
     * ���� �� ī�� 15�� ~23��
     */
    public static CardManager i;
    [SerializeField] GameObject[] cards;
    int cardCase = 1; //ī�尡 ������ ��츦 ���� ��

    int num1;
    int num2;
    int num3;

    //����, ����, ����
    List<int> sets1 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 14 };
    List<int> sets2 = new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12, 13 };
    List<int> sets3 = new List<int> { 0, 1, 2, 3, 4, 5, 15, 16, 17, 18, 19, 20, 21, 22 };

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
        CardDraw();
    }
    public void click(int num)
    {
        if (num == 1)
        {
            Debug.Log("�ؾ ����");
            evt1();
            cards[0].SetActive(false);
        }
        else if (num == 2)
        {
            Debug.Log("����̿�");
            evt2();
            cards[1].SetActive(false);
        }
        else if (num == 3)
        {
            Debug.Log("�̰� ���ʽ�");
            evt3();
            cards[2].SetActive(false);
        }
        else if (num == 4)
        {
            Debug.Log("�������� �ؾ");
            evt4();
            cards[3].SetActive(false);
        }
        else if (num == 5)
        {
            Debug.Log("�ؾ �帱�Կ�");
            evt5();
            cards[4].SetActive(false);
        }
        else if (num == 6)
        {
            Debug.Log("�� ū �ؾ");
            evt6();
            cards[5].SetActive(false);
        }
        else if (num == 7)
        {
            Debug.Log("�����");
            cardCase = 2;
            evt7();
            cards[6].SetActive(false);
        }
        else if (num == 8)
        {
            Debug.Log("�ܴ��� ����");
            evt8();
            cards[7].SetActive(false);
        }
        else if (num == 9)
        {
            Debug.Log("���� ��ȭ");
            evt9();
            cards[8].SetActive(false);
        }
        else if (num == 10)
        {
            Debug.Log("�ݴ� �˰�");
            evt10();
            cards[9].SetActive(false);
        }
        else if (num == 11)
        {
            Debug.Log("�˰� ��ȭ");
            evt11();
            cards[10].SetActive(false);
        }
        else if (num == 12)
        {
            Debug.Log("���� �˰�");
            evt12();
            cards[11].SetActive(false);
        }
        else if (num == 13)
        {
            Debug.Log("���� �˰�");
            evt13();
            cards[12].SetActive(false);
        }
        else if (num == 14)
        {
            Debug.Log("�����");
            cardCase = 3;
            evt14();
            cards[13].SetActive(false);
        }
        else if (num == 15)
        {
            Debug.Log("���̾� ��");
            evt15();
            cards[14].SetActive(false);
        }
        else if (num == 16)
        {
            Debug.Log("���� ��ȭ : ��");
            evt16();
            cards[15].SetActive(false);
        }
        else if (num == 17)
        {
            Debug.Log("ĳ���� ���� : ��");
            evt17();
            cards[16].SetActive(false);
        }
        else if (num == 18)
        {
            Debug.Log("���̽� ��");
            evt18();
            cards[17].SetActive(false);
        }
        else if (num == 19)
        {
            Debug.Log("���� ��ȭ : ����");
            evt19();
            cards[18].SetActive(false);
        }
        else if (num == 20)
        {
            Debug.Log("ĳ���� ���� : ����");
            evt20();
            cards[19].SetActive(false);
        }
        else if (num == 21)
        {
            Debug.Log("��� ��Ʈ");
            evt21();
            cards[20].SetActive(false);
        }
        else if (num == 22)
        {
            Debug.Log("������ : ����");
            evt22();
            cards[21].SetActive(false);
        }
        else if (num == 23)
        {
            Debug.Log("ĳ���� ���� : ����");
            evt23();
            cards[22].SetActive(false);
        }
    } //��ư ���


    public void CardDraw() //ī�� �̱� �˰���
    {
        System.Random random = new System.Random();
        if (cardCase == 1)
        {
            while (true)
            {
                num1 = sets1[random.Next(sets1.Count)];
                num2 = sets1[random.Next(sets1.Count)];
                num3 = sets1[random.Next(sets1.Count)];
                if (num1 != num2 && num2 != num3 && num1 != num3)
                {
                    break;
                }
            }
            cards[num1].SetActive(true);
            cards[num2].SetActive(true);
            cards[num3].SetActive(true);
        }
        else if(cardCase == 2)
        {
            while (true)
            {
                num1 = sets2[random.Next(sets2.Count)];
                num2 = sets2[random.Next(sets2.Count)];
                num3 = sets2[random.Next(sets2.Count)];
                if (num1 != num2 && num2 != num3 && num1 != num3)
                {
                    break;
                }
            }
            cards[num1].SetActive(true);
            cards[num2].SetActive(true);
            cards[num3].SetActive(true);
        }
        else if (cardCase == 3)
        {
            while (true)
            {
                num1 = sets3[random.Next(sets3.Count)];
                num2 = sets3[random.Next(sets3.Count)];
                num3 = sets3[random.Next(sets3.Count)];
                if (num1 != num2 && num2 != num3 && num1 != num3)
                {
                    break;
                }
            }
            cards[num1].SetActive(true);
            cards[num2].SetActive(true);
            cards[num3].SetActive(true);
        }

    }
}
