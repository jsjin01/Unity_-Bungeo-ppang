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
    int num1;
    int num2;
    int num3;

    //ī�� �˰��� 
    List<int> standardSet = new List<int> { 0, 1, 2, 3, 4, 5, 6, 13 };     // �⺻���� ������ ī������ ��� �߰��ϰ� ������ ���ʰ� �Ǵ� ��
    
    //6 -> ����� ����
    List<int> W1Set = new List<int> {7, 8, 9}; // ������� 1�� ��ų set 
    //9 -> �ݴ� �˰� ���� -> rm 9
    List<int> W2Set = new List<int> { 10, 11, 12 }; //������� 2�� ��ų set

    //13-> ����� ����
    List<int> M1set = new List<int> {14, 17, 20 }; //������� 1�� ��ų set
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
            Debug.Log("�ؾ ����");
            SoundManger.i.PlaySound(0);
            evt1();
            selectedCard();
        }
        else if (num == 2)
        {
            Debug.Log("����̿�");
            SoundManger.i.PlaySound(0);
            evt2();
            selectedCard();
        }
        else if (num == 3)
        {
            Debug.Log("�̰� ���ʽ�");
            SoundManger.i.PlaySound(0);
            evt3();
            selectedCard();
        }
        else if (num == 4)
        {
            Debug.Log("�������� �ؾ");
            SoundManger.i.PlaySound(0);
            evt4();
            selectedCard();
        }
        else if (num == 5)
        {
            Debug.Log("�ؾ �帱�Կ�");
            SoundManger.i.PlaySound(0);
            evt5();
            selectedCard();
        }
        else if (num == 6)
        {
            Debug.Log("�� ū �ؾ");
            SoundManger.i.PlaySound(0);
            evt6();
            selectedCard();
        }
        else if (num == 7)
        {
            Debug.Log("�����");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(6);          //����� ����â ����
            standardSet.Remove(13);         //����� ����â ����
            standardSet.AddRange(W1Set);    //����� ��ų ����
            evt7();
            selectedCard();
            UIManager.i.Warrior_Skill.SetActive(true);      //�ǹ� ����
            UIManager.i.Gauge_Warrior.SetActive(true);
            UIManager.i.GaugeBar_Warrior.value = 0f;
            PlayerManager.i.pass += 1;
        }
        else if (num == 8)
        {
            Debug.Log("�ܴ��� ����");
            SoundManger.i.PlaySound(0);
            evt8();
            selectedCard();
        }
        else if (num == 9)
        {
            Debug.Log("���� ��ȭ");
            SoundManger.i.PlaySound(0);
            evt9();
            selectedCard();
        }
        else if (num == 10)
        {
            Debug.Log("�ݴ� �˰�");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(9);       //����� �˰� ����
            standardSet.AddRange(W2Set); //�˰� ������Ʈ
            evt10();
            selectedCard();
        }
        else if (num == 11)
        {
            Debug.Log("�˰� ��ȭ");
            SoundManger.i.PlaySound(0);
            evt11();
            selectedCard();
        }
        else if (num == 12)
        {
            Debug.Log("���� �˰�");
            SoundManger.i.PlaySound(0);
            evt12();
            selectedCard();
        }
        else if (num == 13)
        {
            Debug.Log("���� �˰�");
            SoundManger.i.PlaySound(0);
            evt13();
            selectedCard();
        }
        else if (num == 14)
        {
            Debug.Log("�����");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(13);         //����� ������ ����
            standardSet.Remove(6);          //����� ����â ����
            standardSet.AddRange(M1set);    //����� ���� ������ ����
            evt14();

            UIManager.i.Wizard_Skill.SetActive(true);       //�ǹ� ����
            UIManager.i.Gauge_Wizard.SetActive(true);
            UIManager.i.GaugeBar_Wizard.value = 0f;
            selectedCard();
        }
        else if (num == 15)
        {
            Debug.Log("���̾� ��");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(14);         //���̾ ������ ����
            standardSet.AddRange(fireSet);  //���̾ ���� ī�� �߰�
            evt15();
            selectedCard();
        }
        else if (num == 16)
        {
            Debug.Log("���� ��ȭ : ��");
            SoundManger.i.PlaySound(0);
            evt16();
            selectedCard();
        }
        else if (num == 17)
        {
            Debug.Log("ĳ���� ���� : ��");
            SoundManger.i.PlaySound(0);
            evt17();
            selectedCard();
        }
        else if (num == 18)
        {
            Debug.Log("���̽� ��");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(17);         //���̽��� ������ ����
            standardSet.AddRange(iceSet);   //���̽��� ���� ī�� �߰�
            evt18();
            selectedCard();
        }
        else if (num == 19)
        {
            Debug.Log("���� ��ȭ : ����");
            SoundManger.i.PlaySound(0);
            evt19();
            selectedCard();
        }
        else if (num == 20)
        {
            Debug.Log("ĳ���� ���� : ����");
            SoundManger.i.PlaySound(0);
            evt20();
            selectedCard();
        }
        else if (num == 21)
        {
            Debug.Log("��� ��Ʈ");
            SoundManger.i.PlaySound(0);
            standardSet.Remove(20);             //���� ������ ����
            standardSet.AddRange(thunderSet);   //���� ���� ī�� �߰�
            evt21();
            selectedCard();
        }
        else if (num == 22)
        {
            Debug.Log("������ : ����");
            SoundManger.i.PlaySound(0);
            evt22();
            selectedCard();
        }
        else if (num == 23)
        {
            Debug.Log("ĳ���� ���� : ����");
            SoundManger.i.PlaySound(0);
            evt23();
            selectedCard();
        }
    } //��ư ���


    public void CardDraw() //ī�� �̱� �˰���
    {
        System.Random random = new System.Random();
        if(EnemySpawner.i.stage == 2 && standardSet[6] == 6)
        {
            num1 = standardSet[6];
            num2 = standardSet[7];
            num3 = standardSet[random.Next(5)];

            cards[num1].SetActive(true);
            cards[num2].SetActive(true);
            cards[num3].SetActive(true);
            return;
        }
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
