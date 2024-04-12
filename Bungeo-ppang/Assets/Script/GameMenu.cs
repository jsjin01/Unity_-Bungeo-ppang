using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] Slider back_vol;
    [SerializeField] Slider effect_vol;
    [SerializeField] GameObject[] Scenes;
    private void Start()
    {
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back" ,100f);
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect" , 100f);
    }
    private void Update()
    {
        if (Scenes[5].activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MainView();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))       //������ ����Ű ������
        {
            for (int i = Scenes.Length-2; i >= 0; i--)
            {
                if (Scenes[i].activeSelf)
                {
                    Scenes[i + 1].SetActive(true);      //���� ������ ��ȯ
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))       //������ ����Ű ������
        {
            for (int i = Scenes.Length-1; i >= 1 ; i--)
            {
                if (Scenes[i].activeSelf)
                {
                    Scenes[i].SetActive(false);      //���� ������ ��ȯ
                    break;
                }
            }
        }
    }
    public void GameStart()
    {
        SoundManger.i.PlaySound(0);
        Scenes[0].SetActive(true);
        //StartCoroutine("SceneOn");
    }

    public void setVolume_background(float volume)
    {
        Debug.Log("�������: " + volume);
        PlayerPrefs.SetFloat("Volume_Back", volume);

        PlayerPrefs.Save();
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("ȿ����: " + volume);
        PlayerPrefs.SetFloat("Volume_Effect", volume);
        PlayerPrefs.Save();
    }
    /*IEnumerator SceneOn()
    {
        for(int i = 0;i < Scenes.Length; i++)
        {
            Scenes[i].SetActive(true);
            yield return new WaitForSeconds(5f);
        }
    }*/
    public void NextScene_Click(int num)
    {
        SoundManger.i.PlaySound(0);
        Scenes[num+1].SetActive(true);
    }
    public void PrevScene_Click(int num)
    {
        SoundManger.i.PlaySound(0);
        Scenes[num].SetActive(false);
    }
    public void MainView()
    {
        SoundManger.i.PlaySound(0);
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    public void GameEixt()
    {
        Application.Quit();
    }
}
