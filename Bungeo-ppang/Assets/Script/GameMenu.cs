using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] Slider back_vol;
    [SerializeField] Slider effect_vol;
    private void Start()
    {
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back");
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect");
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
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
}
