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

    bool isStart = false;
    private void Start()
    {
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back");
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect");
    }
    public void GameStart()
    {
        StartCoroutine("SceneOn");
        isStart = true;
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
    IEnumerator SceneOn()
    {
        for(int i = 0;i < Scenes.Length; i++)
        {
            Scenes[i].SetActive(true);
            yield return new WaitForSeconds(5f);
        }
    }
    public void NextScene_Click(int num)
    {
        Scenes[num+1].SetActive(true);
    }
    public void PrevScene_Click(int num)
    {
        Scenes[num].SetActive(false);
    }
    public void MainView()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
