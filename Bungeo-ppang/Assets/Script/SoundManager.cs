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
     * 0 => 클릭음
     * 1 => 맞는 소리
     * 2 => 죽는 소리
     * 3 => 붕어빵 쏘는 소리
     * 4 => 불
     * 5 => 아이스
     * 6 => 번개
     * 7 => 매직볼
     * 8 => 보스 스킬 
     * 9 => 보스 죽는 소리
     * 10 => 보스 맞는 소리
     * 11 => 승리시 나는 소리
     * 12 => 패배시 나는 소리
     * 13 => 칼날 생성 소리
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

    public void PlaySound(int id, bool _loop = false)          //사운드 타입과 타입에 따른 인덱스 
    {
        AudioSource s = null;

        //사운드가 플레이 중이지 않은 오브젝트가 있다면
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
            GameObject temp = Instantiate(baseobj, transform); //오브젝트 하나 생성
            s = temp.GetComponent<AudioSource>();
        }

        s.volume = PlayerPrefs.GetFloat("Volume_Effect", 50);
        s.clip = clipList[id];                  //사운드 클립 변경
        s.loop = _loop;                         //루프 상태 변경
        s.Play();                               //사운드 플레이
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
