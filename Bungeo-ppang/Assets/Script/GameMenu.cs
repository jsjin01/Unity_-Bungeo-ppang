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
    private void Update()
    {
        if(Input.anyKey)
        {
            if (isStart)
            {
                SceneManager.LoadScene("Main");
                Time.timeScale = 1f;
            }
        }
    }
    public void GameStart()
    {
        StartCoroutine("SceneOn");
        isStart = true;
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
}
