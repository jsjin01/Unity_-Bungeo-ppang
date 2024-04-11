using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager i;
    RawImage hp1;
    RawImage hp2;
    RawImage hp3;
    [SerializeField] Texture[] hpUI;
    Text stageT;
    [SerializeField] Slider back_vol;
    [SerializeField] Slider effect_vol;

    [SerializeField] public Slider GaugeBar_Warrior;        //전사붕 게이지바
    [SerializeField] public GameObject Gauge_Warrior;
    [SerializeField] public Slider GaugeBar_Wizard;     //법사붕 게이지바
    [SerializeField] public GameObject Gauge_Wizard;

    [SerializeField] public GameObject Warrior_Skill;
    [SerializeField] public GameObject Wizard_Skill;

    [SerializeField] public GameObject GameStart;
    int startbtn = 1;
    private void Awake()
    {
        i = this;
        Time.timeScale = 0f;
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
        if(startbtn == 1)
        {
            if (Input.anyKey)
            {
                Time.timeScale = 1f;
                GameStart.SetActive(false);
                startbtn = 0;
            }
        }

        if (GaugeBar_Warrior.value >= 1.2f || GaugeBar_Wizard.value >=1.8f)    //게이지바가 다 차면
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Warrior_Skill.activeSelf)
                {
                    GaugeBar_Warrior.value = 0f;    //게이지바 초기화
                    AliveEnemyPoolManager.i.WarriorSkill();  //전장외침
                }
                else if(Wizard_Skill.activeSelf)
                {
                    GaugeBar_Wizard.value = 0f;     //게이지바 초기화
                    PlayerMoveControl pmc = GameObject.FindObjectOfType<PlayerMoveControl>();
                    if (pmc != null)
                    {
                        StartCoroutine(pmc.WizardSkill());   //주문난사
                    }
                }
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
        PlayerPrefs.SetFloat("Volume_Back", volume);
        PlayerPrefs.Save();
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("효과음: " + volume);
        PlayerPrefs.SetFloat("Volume_Effect", volume);
        PlayerPrefs.Save();
    }
    public void Out() //시작화면으로 나가기
    {
        SceneManager.LoadScene("GameMenu");
        Time.timeScale = 1f;
    }

    public void Restart() //재시작
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;

    }
    public void Quit() //게임 나가기
    {
        Application.Quit();
    }
    public void SetHp(int hp)
    {
        if (hp == 3)
        {
            hp1.color = Color.white;
            hp1.texture = hpUI[0];
            hp2.color = Color.white;
            hp2.texture = hpUI[0];
            hp3.color = Color.white;
            hp3.texture = hpUI[0];
        }
        else if (hp == 2)
        {
            hp1.color = Color.white;
            hp1.texture = hpUI[0];
            hp2.color = Color.white;
            hp2.texture = hpUI[0];
            hp3.color = Color.gray;
            hp3.texture = hpUI[1];
        }
        else if (hp == 1)
        {
            hp1.color = Color.white;
            hp1.texture = hpUI[0];
            hp2.color = Color.gray;
            hp2.texture = hpUI[1];
            hp3.color = Color.gray;
            hp3.texture = hpUI[1];
        }
        else
        {
            hp1.color = Color.gray;
            hp1.texture = hpUI[1];
            hp2.color = Color.gray;
            hp2.texture = hpUI[1];
            hp3.color = Color.gray;
            hp3.texture = hpUI[1];
        }
    } //체력 UI이 설정

    public void SetStage(int num)
    {
        stageT.text = "STAGE : " + num;
    }

}
