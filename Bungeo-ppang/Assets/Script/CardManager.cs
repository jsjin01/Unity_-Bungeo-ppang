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
            Debug.Log("�ؾ ����");
            evt1();
        }
        else if (num == 2)
        {
            Debug.Log("����̿�");
            evt2();
        }
        else if (num == 3)
        {
            Debug.Log("�̰� ���ʽ�");
            evt3();
        }
        else if (num == 4)
        {
            Debug.Log("�������� �ؾ");
            evt4();
        }
        else if (num == 5)
        {
            Debug.Log("�ؾ �帱�Կ�");
            evt5();
        }
        else if (num == 6)
        {
            Debug.Log("�� ū �ؾ");
            evt6();
        }
        else if (num == 7)
        {
            Debug.Log("�����");
            evt7();
        }
        else if (num == 8)
        {
            Debug.Log("�ܴ��� ����");
            evt8();
        }
        else if (num == 9)
        {
            Debug.Log("���� ��ȭ");
            evt9();
        }
        else if (num == 10)
        {
            Debug.Log("�ݴ� �˰�");
            evt10();
        }
        else if (num == 11)
        {
            Debug.Log("�˰� ��ȭ");
            evt11();
        }
        else if (num == 12)
        {
            Debug.Log("���� �˰�");
            evt12();
        }
        else if (num == 13)
        {
            Debug.Log("���� �˰�");
            evt13();
        }
        else if (num == 14)
        {
            Debug.Log("���� ��ħ");
            evt14();
        }
        else if (num == 15)
        {
            Debug.Log("�����");
            evt15();
        }
        else if (num == 16)
        {
            Debug.Log("���̾� ��");
            evt16();
        }
        else if (num == 17)
        {
            Debug.Log("���� ��ȭ : ��");
            evt17();
        }
        else if (num == 18)
        {
            Debug.Log("ĳ���� ���� : ��");
            evt18();
        }
        else if (num == 19)
        {
            Debug.Log("���̽� ��");
            evt19();
        }
        else if (num == 20)
        {
            Debug.Log("���� ��ȭ : ����");
            evt20();
        }
        else if (num == 21)
        {
            Debug.Log("ĳ���� ���� : ����");
            evt21();
        }
        else if (num == 22)
        {
            Debug.Log("��� ��Ʈ");
            evt22();
        }
        else if (num == 23)
        {
            Debug.Log("������ : ����");
            evt23();
        }
        else if (num == 24)
        {
            Debug.Log("ĳ���� ���� : ����");
            evt24();
        }
    }
}
