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
    public void GameStart()
    {
        SoundManger.i.PlaySound(0);
        StartCoroutine("SceneOn");
    }

    public void setVolume_background(float volume)
    {
        Debug.Log("배경음악: " + volume);
        PlayerPrefs.SetFloat("Volume_Back", volume);

        PlayerPrefs.Save();
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("효과음: " + volume);
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
