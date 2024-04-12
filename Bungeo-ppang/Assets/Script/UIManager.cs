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

    [SerializeField] public Slider GaugeBar_Warrior;        //����� ��������
    [SerializeField] public GameObject Gauge_Warrior;
    [SerializeField] public Slider GaugeBar_Wizard;     //����� ��������
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
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back", 100f);
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect", 100f);
    }
    private void Update()
    {
        if(startbtn == 1)
        {
            if (Input.anyKey)
            {
                SoundManger.i.PlaySound(0);
                Time.timeScale = 1f;
                GameStart.SetActive(false);
                startbtn = 0;
            }
        }
        if (Warrior_Skill.activeSelf)
        {
            if (GaugeBar_Warrior.value >= 1.2f)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    GaugeBar_Warrior.value = 0f;    //�������� �ʱ�ȭ
                    AliveEnemyPoolManager.i.WarriorSkill();  //�����ħ
                }
            }
        }
        else if (Wizard_Skill.activeSelf)
        {
            if (GaugeBar_Wizard.value >= 1.8f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GaugeBar_Wizard.value = 0f;     //�������� �ʱ�ȭ
                    PlayerMoveControl pmc = GameObject.FindObjectOfType<PlayerMoveControl>();
                    if (pmc != null)
                    {
                        StartCoroutine(pmc.WizardSkill());   //�ֹ�����
                    }
                }
            }
        }
    }
    public void SettingOn()
    {
        SoundManger.i.PlaySound(0);
        Time.timeScale = 0f;
    }
    public void SettingOff()
    {
        SoundManger.i.PlaySound(0);
        Time.timeScale = 1f;
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
    public void Out() //����ȭ������ ������
    {
        SoundManger.i.PlaySound(0);
        SceneManager.LoadScene("GameMenu");
        Time.timeScale = 1f;
    }

    public void Restart() //�����
    {
        SoundManger.i.PlaySound(0);
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;

    }
    public void Quit() //���� ������
    {
        SoundManger.i.PlaySound(0);
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
    } //ü�� UI�� ����

    public void SetStage(int num)
    {
        stageT.text = "STAGE : " + num;
    }

}
