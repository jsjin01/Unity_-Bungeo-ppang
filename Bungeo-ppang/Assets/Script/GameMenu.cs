using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public float volume_back=0f;
    public float volume_effect=0f;
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void setVolume_background(float volume)
    {
        Debug.Log("배경음악: " + volume);
        //volume_back = volume;
        PlayerPrefs.SetFloat("Volume_Back", volume);
        PlayerPrefs.Save();
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("효과음: " + volume);
        //volume_effect = volume;
        PlayerPrefs.SetFloat("Volume_Effect", volume);
        PlayerPrefs.Save();
    }
}
