using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
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
}
