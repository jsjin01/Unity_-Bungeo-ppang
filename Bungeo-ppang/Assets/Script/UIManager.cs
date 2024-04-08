using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager i;
    RawImage hp1;
    RawImage hp2;
    RawImage hp3;
    Text stageT;
    [SerializeField] Slider back_vol;
    [SerializeField] Slider effect_vol;
    [SerializeField] public Slider GaugeBar;
    [SerializeField] public GameObject Warrior_Skill;
    [SerializeField] public GameObject Wizard_Skill;

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        hp1 = GameObject.Find("Hp 1").GetComponent<RawImage>();
        hp2 = GameObject.Find("Hp 2").GetComponent<RawImage>();
        hp3 = GameObject.Find("Hp 3").GetComponent<RawImage>();
        stageT = GameObject.Find("StageText").GetComponent<Text>();
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back", 0f);
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect", 0f);

        
    }
    private void Update()
    {
        if (GaugeBar.value >= 1f)       //게이지바가 다 차면
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GaugeBar.value = 0f;        //게이지바 초기화
                AliveEnemyPoolManager.i.WarriorSkill();
            }
        }
    }
    public void SettingOn()
    {
        Time.timeScale = 0f;
    }
    public void SettingOff()
    {
        Time.timeScale = 1f;
    }
    public void setVolume_background(float volume)
    {
        Debug.Log("배경음악: " + volume);
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("효과음: " + volume);
    }
    public void SetHp(int hp)
    {
        if (hp == 3)
        {
            hp1.color = Color.red;
            hp2.color = Color.red;
            hp3.color = Color.red;
        }
        else if (hp == 2)
        {
            hp1.color = Color.red;
            hp2.color = Color.red;
            hp3.color = Color.black;
        }
        else if (hp == 1)
        {
            hp1.color = Color.red;
            hp2.color = Color.black;
            hp3.color = Color.black;
        }
        else
        {
            hp1.color = Color.black;
            hp2.color = Color.black;
            hp3.color = Color.black;
        }
    } //체력 UI이 설정

    public void SetStage(int num)
    {
        stageT.text = "STAGE : " + num;
    }

}
