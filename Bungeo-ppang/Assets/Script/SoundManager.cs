using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManger : MonoBehaviour
{
    public static SoundManger i;
    GameObject baseobj;
    [SerializeField] AudioSource Bgm;
    [SerializeField] AudioClip[] clipList;
    /*
     * 0 => Ŭ����
     * 1 => �´� �Ҹ�
     * 2 => �״� �Ҹ�
     * 3 => �ؾ ��� �Ҹ�
     * 4 => ��
     * 5 => ���̽�
     * 6 => ����
     * 7 => ������
     * 8 => ���� ��ų 
     * 9 => ���� �״� �Ҹ�
     * 10 => ���� �´� �Ҹ�
     * 11 => �¸��� ���� �Ҹ�
     * 12 => �й�� ���� �Ҹ�
     * 13 => Į�� ���� �Ҹ�
     */

    
    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        baseobj = transform.GetChild(0).gameObject;
        Bgm = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Bgm.volume = PlayerPrefs.GetFloat("Volume_Back", 50);
    }

    public void PlaySound(int id, bool _loop = false)          //���� Ÿ�԰� Ÿ�Կ� ���� �ε��� 
    {
        AudioSource s = null;

        //���尡 �÷��� ������ ���� ������Ʈ�� �ִٸ�
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().isPlaying == false)
            {
                s = transform.GetChild(i).GetComponent<AudioSource>();
                break;
            }
        }

        if (s == null)
        {
            GameObject temp = Instantiate(baseobj, transform); //������Ʈ �ϳ� ����
            s = temp.GetComponent<AudioSource>();
        }

        s.volume = PlayerPrefs.GetFloat("Volume_Effect", 50);
        s.clip = clipList[id];                  //���� Ŭ�� ����
        s.loop = _loop;                         //���� ���� ����
        s.Play();                               //���� �÷���
    }


    public void StopSound(int id)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().clip == clipList[id] &&
                transform.GetChild(i).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(i).GetComponent<AudioSource>().Stop();
                break;
            }
        }
    }
}
