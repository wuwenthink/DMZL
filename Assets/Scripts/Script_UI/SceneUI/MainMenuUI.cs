using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    //标题按钮
    public GameObject Button_TitleSection;
    public GameObject Button_GameCup;
    public GameObject Button_TitleSetting;
    public GameObject Button_TitleCloseGame;
    //主要弹窗
    public GameObject GameObject_ChooseStyle;
    public GameObject GameObject_ChooseStory;
    public GameObject GameObject_GameCup;
    public GameObject GameObject_Setting;
    public GameObject GameObject_CloseGame;
    //其他窗口
    public GameObject GameObject_ChooseStyleInfo;
    public GameObject GameObject_ChooseInfo;
    public GameObject GameObject_GameCupInfo;
    public GameObject GameObject_SettingInfo;
    public GameObject GameObject_CloseGameInfo;

    //其他按钮
    public GameObject Button_NewStory;
    public GameObject Button_LoadSection;
    public GameObject Button_SettingBack;
    public GameObject Button_NCG;
    public GameObject Button_ChooseStoryBack;
    public GameObject Button_ICG;
    public GameObject Button_ChooseStyleBack;
    public GameObject Button_GameCupBack;
    
    //系统时间显示文字控件
    GameObject Lable_sYear;
    GameObject Lable_sMonth;
    GameObject Lable_sDay;
    GameObject Lable_sHour;
    GameObject Lable_sMinute;
    GameObject Lable_sSecond;
    int sYear;
    int sMonth;
    int sDay;
    int sHour;
    int sMinute;
    int sSecond;

    //剧本相关
    public GameObject GameObject_Tips;
    public GameObject GameObject_StoryList;
    public GameObject GameObject_StoryPos;
    public GameObject GameObject_StoryEX;

    //剧本详情
    public GameObject GameObject_StoryInfo;
    public GameObject Sprite_BackMain;
    public UIText Label_Name;
    public GameObject GameObject_Scroll_History;
    public UIText Label_Histroy;
    public GameObject GameObject_Scroll_Story;
    public UIText Label_Story;
    public UIText Label_dynasty;
    public UIText Label_AncientYear;
    public UIText Label_AncientMonth;
    public UIText Label_AD;
    public GameObject Button_Info;
    public GameObject Button_New;
    public GameObject GameObject_Scroll_Role;
    public GameObject GameObject_Pos_Role;
    public GameObject Button_Open;


    //角色详情
 

    GameObject chooseStory;
    GameObject chooseRoleIcon;

    int MainRoleID;
    int DramaID;
    int Drama_RoleID;
    TimeManager timeManager;

    void Awake ()
    {
        //系统时间显示文字控件
        Lable_sYear = GameObject.Find ("Label_Year");
        Lable_sMonth = GameObject.Find ("Label_Month");
        Lable_sDay = GameObject.Find ("Label_Day");
        Lable_sHour = GameObject.Find ("Label_Hour");
        Lable_sMinute = GameObject.Find ("Label_Minute");
        Lable_sSecond = GameObject.Find ("Label_Second");

        CloseWindow(GameObject_Tips);
        GameObject_StoryInfo.SetActive(false);

        timeManager = FindObjectOfType<TimeManager>();
    }
    void Start ()
    {
        STime();
        ClickControl ();
    }

    //系统时间方法
    void STime()
    {
        sYear = System.DateTime.Today.Year;
        sMonth = System.DateTime.Today.Month;
        sDay = System.DateTime.Today.Day;
        sHour = System.DateTime.Today.Hour;
        sMinute = System.DateTime.Today.Minute;
        sSecond = System.DateTime.Today.Second;
        Lable_sYear.GetComponent<UILabel>().text = (sYear + " 年").ToString();
        Lable_sMonth.GetComponent<UILabel>().text = (sMonth + " 月").ToString();
        Lable_sDay.GetComponent<UILabel>().text = (sDay + " 日").ToString();
        Lable_sHour.GetComponent<UILabel>().text = (sHour + ":").ToString();
        Lable_sMinute.GetComponent<UILabel>().text = (sMinute + ":").ToString();
        Lable_sSecond.GetComponent<UILabel>().text = sSecond.ToString();

    }
    void Update ()
    {
        //BGControl ();
    }

    void FixedUpdate ()
    {

        //STime ();
    }


    //开关窗口
    void ChooseStyle (GameObject btn)
    {
        GameObject_ChooseStyle.SetActive (true);
    }
    //选择剧本
    void ChooseStory (GameObject button)
    {
        GameObject_ChooseStyle.SetActive (false);
        GameObject_ChooseStory.SetActive (true);
        Button_ChooseStoryBack.SetActive (true);
        GameObject_StoryEX.SetActive(true);
        NGUITools.DestroyChildren(GameObject_StoryPos.transform);

        foreach (var dic in SelectDao.GetDao().SelectAllDrama())
        {
            UnityEngine.Transform story = CommonFunc.GetInstance.UI_Instantiate(GameObject_StoryEX.transform, GameObject_StoryPos.transform, Vector3.zero, Vector3.one);
            int ID = dic.id;
            story.name = ID.ToString();
            story.Find("Label_StoryName").GetComponent<UIText>().SetText(false, dic.name);
            story.Find("Label_StoryTime").GetComponent<UIText>().SetText(false, Constants.GetYear(dic.TCYear)[0]+ Constants.GetYear(dic.TCYear)[1] + LanguageMgr.GetInstance.GetText("Nomal_15"));
            story.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
            if (ID == 1)
            {
                story.Find("Sprite_Lock").GetComponent<UISprite>().enabled = false;
                chooseStory = story.gameObject;
            }
            else
            {
                story.Find("Sprite_Lock").GetComponent<UISprite>().enabled = true;//除了第一个剧本外其他临时禁用。
            }
            UIEventListener.Get(story.gameObject).onClick = ChooseStoryClick;
            UIEventListener.Get(story.gameObject).onHover = chooseStoryHover;
        }
        GameObject_StoryPos.GetComponent<UIGrid>().enabled = true;
        GameObject_StoryEX.SetActive(false);
    }


    void chooseStoryHover(GameObject btn, bool isHover)
    {
        if (chooseStory != null)
        {
            chooseStory.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        chooseStory = btn;
    }

    //打开剧本界面
    void ChooseStoryClick (GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Button_Info, false);
        CommonFunc.GetInstance.SetButtonState(Button_Open, false);

        CommonFunc.GetInstance.SetButtonState(Button_New, false);//创建新人物按钮临时禁用

        Drama_Main drama = SelectDao.GetDao().SelectDrama_Main(int.Parse(btn.name));
        if (btn.name == "1")
        {
            DramaID = int.Parse(btn.name);
            GameObject_StoryInfo.SetActive(true);
            NGUITools.DestroyChildren(GameObject_Pos_Role.transform);

            Label_Name.SetText(false, drama.name);
            Label_Histroy.SetText(false, drama.History);
            Label_Story.SetText(false, drama.Story);
            Label_AncientMonth.SetText(false, Constants.TransformMonth(drama.TCMonth));
            Label_AD.SetText(false, drama.TCYear.ToString() + LanguageMgr.GetInstance.GetText("Nomal_15"));

            int count = 0;
            foreach (var item in SelectDao.GetDao().SelectDrama_Role_ByDramaID(DramaID))
            {
                if (item.use == 1)
                {
                    UnityEngine.Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_RoleIcon, GameObject_Pos_Role.transform, new Vector3(-60 * count, 0, 0), Vector3.one);
                    go.Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = SelectDao.GetDao().SelectRole(item.modelID).headIcon;
                    go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
                    go.Find("Sprite_Choose").Find("Sprite").GetComponent<UISprite>().enabled = false;
                    go.name = item.id.ToString();
                    UIEventListener.Get(go.gameObject).onClick = ChooseRoleIcon;
                    count++;
                }
            }
        }

    }

    void ChooseRoleIcon(GameObject btn)
    {
        if (chooseRoleIcon != null)
        {
            chooseRoleIcon.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
            chooseRoleIcon.transform.Find("Sprite_Choose").Find("Sprite").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        btn.transform.Find("Sprite_Choose").Find("Sprite").GetComponent<UISprite>().enabled = true;
        chooseRoleIcon = btn;

        Drama_RoleID = int.Parse(btn.name);
        MainRoleID = SelectDao.GetDao().SelectDrama_Role(Drama_RoleID).modelID;

        CommonFunc.GetInstance.SetButtonState(Button_Info, true);
        CommonFunc.GetInstance.SetButtonState(Button_Open, true);
        UIEventListener.Get(Button_Info).onClick = RoleInfo;
        UIEventListener.Get(Button_Open).onClick = ChooseMap;
    }

    //选择模式
    void ChooseType (GameObject btn)
    {
        GameObject_ChooseStyle.SetActive(true);
    }

    //角色详情界面
    void RoleInfo(GameObject btn)
    {
  
    }

    //开始新游戏
    void ChooseMap(GameObject btn)
    {
        FindObjectOfType<Load_Drama>().Load_OneDrama(DramaID, Drama_RoleID);

    }
    //加载存档
    void LoadSection(GameObject btn)
    {
        GameObject_ChooseStyle.SetActive(false);
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LoadGame, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
    }
    //游戏成就
    void GameCup (GameObject btn)
    {
        GameObject_GameCup.SetActive (true);
    }
    //游戏设置
    void Setting (GameObject button)
    {
        GameObject_Setting.SetActive (true);
    }
    //退出游戏
    void IsCloseGame (GameObject button)
    {
        GameObject_CloseGame.SetActive (true);
    }


    //点击响应判断
    void ClickControl()
    {


        UIEventListener.Get(Button_TitleSection).onClick = ChooseStyle;//打开选择游戏方式界面
        UIEventListener.Get(Button_NewStory).onClick = ChooseStory;//打开新游戏剧本界面
        UIEventListener.Get(Button_LoadSection).onClick = LoadSection;//打开继续游戏界面
        UIEventListener.Get(Button_GameCup).onClick = GameCup;//打开游戏里程碑界面
        UIEventListener.Get(Button_TitleSetting).onClick = Setting;//打开setting界面
        UIEventListener.Get(Button_TitleCloseGame).onClick = IsCloseGame;//打开NCG界面


        UIEventListener.Get(Button_ChooseStoryBack).onClick = CloseWindow;//关闭选择副本界面
        UIEventListener.Get(Button_SettingBack).onClick = CloseWindow;//关闭setting界面
        UIEventListener.Get(Button_NCG).onClick = CloseWindow;//关闭NCG界面
        UIEventListener.Get(Button_ChooseStyleBack).onClick = CloseWindow;//关闭继续游戏界面
        UIEventListener.Get(Button_GameCupBack).onClick = CloseWindow;//关闭游戏成就界面

        UIEventListener.Get(Button_ICG).onClick = ToCloseGame;//关闭游戏
        
        UIEventListener.Get(Sprite_BackMain).onClick = BackMain;//返回主界面
    }


    //关闭窗口检测+清除所有窗口状态
    void CloseWindow(GameObject button)
    {
        GameObject_ChooseStyle.SetActive(false);
        GameObject_ChooseStory.SetActive(false);
        GameObject_GameCup.SetActive(false);
        GameObject_Setting.SetActive(false);
        GameObject_CloseGame.SetActive(false);
    }


    //弹出大地图选地选人
    void ToMap_Setting(GameObject btn)
    {
        StartCoroutine(CommonFunc.GetInstance.ToSceneLoading(Data_Static.SceneName_Map_World));
    }

    //退出游戏
    void ToCloseGame(GameObject btn)
    {
        CommonFunc.GetInstance.ToCloseGame();
    }


    void BackMain(GameObject btn)
    {
        GameObject_StoryInfo.SetActive(false);
    }

    void Back_Person(GameObject btn)
    {
        
    }




















    ////背景卷轴滚动，超过边界返回
    //void BGControl ()
    //{
    //    if (index == 1)
    //    {
    //        Texture_MainBG.mainTexture = texture1;
    //        Texture_MainBG.width = 3374;
    //        Texture_MainBG.height = 100;
    //        if (m_Pos.x < -12852)
    //        {
    //            m_Pos.x = 12852;
    //        }
    //        else
    //        {
    //            m_Pos.x -= mspeed * Time.deltaTime;
    //            Main_BG.transform.localPosition = m_Pos;

    //        }
    //    }
    //    else if (index == 2)
    //    {
    //        Texture_MainBG.mainTexture = texture2;
    //        Texture_MainBG.width = 2823;
    //        Texture_MainBG.height = 90;
    //        if (m_Pos.x < -10619)
    //        {
    //            m_Pos.x = 10619;
    //        }
    //        else
    //        {
    //            m_Pos.x -= mspeed * Time.deltaTime;
    //            Main_BG.transform.localPosition = m_Pos;

    //        }
    //    }
    //    else if (index == 3)
    //    {
    //        Texture_MainBG.mainTexture = texture3;
    //        Texture_MainBG.width = 2525;
    //        Texture_MainBG.height = 100;
    //        if (m_Pos.x < -9224)
    //        {
    //            m_Pos.x = 8384;
    //        }
    //        else
    //        {
    //            m_Pos.x -= mspeed * Time.deltaTime;
    //            Main_BG.transform.localPosition = m_Pos;

    //        }
    //    }
    //    else
    //    {
    //        Debug.Log ("图片加载失败");
    //    }

    //}

}
