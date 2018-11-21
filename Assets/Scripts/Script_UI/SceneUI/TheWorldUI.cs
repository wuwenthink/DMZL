using UnityEngine;

public class TheWorldUI : MonoBehaviour
{
    //角色面板按钮
    public GameObject Button_Hp;
    public UISprite Background_Hp;
    public UIProgressBar ProgressBar_health;
    public UIProgressBar ProgressBar_mind;
    public GameObject Button_Mood;
    public UISprite Background_Mood;

    public GameObject Button_Change;

    public UIText Label_Temp;
    public UIText Label_Weather;
    public UISprite Sprite_DayTime;
    public UIText Label_DayTime;

    public GameObject Button_NowTime; // 当期时间
    public GameObject GameObject_NowTime; //日期
    public UIProgressBar GameObject_TimeSlider;
    public UIText Label_Hour;
    public UIText Label_Year;
    public UIText Label_Year_C;
    public UIText Label_Month;
    public UIText Label_Month_C;
    public UIText Label_Day;
    public UIText Label_Day_C;
    public UIText Label_Holiday;
    public GameObject GameObject_Holiday;

    public GameObject Button_Map;//地图按钮
    public GameObject GameObject_PosInfo;
    public GameObject Label_Fu;
    public GameObject Label_Xian;
    public GameObject Label_Scene;

    public GameObject Button_Item1;
    public GameObject Button_Item2;
    public GameObject Button_Item3;

    public TweenPosition GameObject_Top;
    public TweenPosition GameObject_Bottom;
    public TweenPosition GameObject_Right;

    Transform GameOver;
    KeyControl KC;
    Transform UI_Root;
    TimeManager timeManager;

    public GameObject Button_GM;
    public GameObject Button_Test;
    /// <summary>
    /// 移动速度
    /// </summary>
    float moveSpeed = 3;

    void GM(GameObject btn)
    {
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_GMTools, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
    }
    void test(GameObject btn)
    {
        GameObject_Top.enabled = true;
        GameObject_Bottom.enabled = true;
        GameObject_Right.enabled = true;
        GameObject_Top.PlayForward();
        GameObject_Bottom.PlayForward();
        GameObject_Right.PlayForward();
        //FindObjectOfType<WorldSceneChange>().SceneChange(Data_Static.Map_Fight_Scene);
    }

    void Awake ()
    {
        UIEventListener.Get(Button_GM).onClick = GM;
        UIEventListener.Get(Button_Test).onClick = test;

        GameOver = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_GameOver, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        GameOver.gameObject.SetActive(false);
    }


    void Start()
    {
        KC = FindObjectOfType<KeyControl>();
        UI_Root = FindObjectOfType<UIRoot>().transform;
        timeManager = FindObjectOfType<TimeManager>();

        ClickAnswer();
        ClickControl();
        SetPlace();

        GameObject_NowTime.SetActive(false);
        GameObject_PosInfo.SetActive(false);


        timeManager.health = Charactor.GetInstance.health;
        timeManager.hungry = Charactor.GetInstance.hunger;
        timeManager.temp = Charactor.GetInstance.temp;
        timeManager.mood = Charactor.GetInstance.mood;
    }

    void Update ()
    {
        ClickControl ();
    }

    void FixedUpdate ()
    {
        SetTime();
        SetRoleState();

        //角色移动控制
        if (UICamera.isOverUI)
        {
            return;
        }
        else
        {
            FindObjectOfType<RoleControl_Move>().RoleMove(moveSpeed);
        }
    }

    /// <summary>
    /// 切换时间模式
    /// </summary>
    /// <param name="btn"></param>
    public void ChangeTimeModel(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        string des = LanguageMgr.GetInstance.GetText("Tips_System_22");
        string yes = LanguageMgr.GetInstance.GetText("Tips_8");
        string no = LanguageMgr.GetInstance.GetText("Tips_7");
        go.GetComponent<SystemTipsUI>().SetTipDesc(des, yes, no);

        UIEventListener.Get(go.GetComponent<SystemTipsUI>().Button_Agree).onClick = Agree;
        UIEventListener.Get(go.GetComponent<SystemTipsUI>().Button_Cancel).onClick = Cancel;
    }

    void Agree(GameObject btn) { ChangeTimeModel(true); }
    void Cancel(GameObject btn) { ChangeTimeModel(false); }

    /// <summary>
    /// 接收系统提示界面的按钮选择
    /// </summary>
    /// <param name="isYes"></param>
    void ChangeTimeModel(bool isYes)
    {
        if (isYes)
        {
            if (timeManager.GetTimeState())//从地图模式转到即时模式，进行时间流动。
            {
                Button_Change.GetComponentInChildren<UIText>().SetText(true, "Main_10");
                Button_Change.transform.Find("Sprite_Sign").GetComponent<UISprite>().spriteName = "Button3Sign060";

                TimeManager.ContinueTime();
            }
            else//从即时模式转到地图模式，时间跳转到第二天早晨6点，停止时间流动。
            {
                Button_Change.GetComponentInChildren<UIText>().SetText(true, "Main_9");
                Button_Change.transform.Find("Sprite_Sign").GetComponent<UISprite>().spriteName = "Button3Sign063";

                TimeManager.PauseTime();
            }
            FindObjectOfType<SystemTipsUI>().gameObject.SetActive(false);
            //昼夜切换、界面特效和加速的效果暂时用进度条动画代替
            Transform tf= CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_ProgressAnimation, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
            float mtime = 1f / tf.GetComponent<ProgressAnimationUI>().animationTime;
            Invoke("timeInit", mtime);
        }
    }

    void timeInit()
    {
        //时间跳转到第二天早晨
        timeManager.SpeedTime(timeManager.GetYears(), timeManager.GetMonths(), TimeManager.GetDays() + 1, 4);
        //进行每日初始化

    }

    /// <summary>
    /// 快捷物品栏刷新
    /// </summary>
    public void SetFastItem()
    {

    }

    /// <summary>
    /// 玩家状态显示
    /// </summary>
    public void SetRoleState()
    {
        //健康和精神根据BUFF和事件进行增减
        ProgressBar_health.value = (float)timeManager.health/100;
        ProgressBar_mind.value = (float)timeManager.hungry /100;
        Background_Hp.fillAmount = (float)timeManager.hp /100;
        //心情的值受到一些事件的影响
        Background_Mood.fillAmount = 1;


        //冷暖/气候变化
        Label_Temp.SetText(false, "寒冷");
        Label_Weather.SetText(false, "多云");

        //人物死亡，游戏结束，弹出结局。（暂时回到主界面）
        if (timeManager.hp < 0) {
            GameOver.gameObject.SetActive(true);
            GameObject box = GameOver.GetComponentInChildren<BoxCollider>().gameObject;
            UIEventListener.Get(box).onClick = backMain;
        }
    }

    void backMain(GameObject btn)
    {
        StartCoroutine(CommonFunc.GetInstance.ToSceneLoading(Data_Static.SceneName_Main_Menu));
    }

    /// <summary>
    /// 当前任务刷新
    /// </summary>
    public void SetNowTask()
    {

    }
    /// <summary>
    /// 当前消息刷新
    /// </summary>
    public void SetNowNews()
    {

    }

    /// <summary>
    /// 当前位置信息
    /// </summary>
    public void SetPlace()
    {
        Label_Fu.GetComponent<UIText>().SetText(false, "顺天府");
        Label_Xian.GetComponent<UIText>().SetText(false, "宛平县");
        Label_Scene.GetComponent<UIText>().SetText(false, "朝天日中坊");
    }

    /// <summary>
    /// 当前时间刷新
    /// </summary>
    public void SetTime()
    {
        GameObject_TimeSlider.value = timeManager.GetQuarter() * 15f / 120;

        Label_Hour.SetText(false, TimeManager.GetTwoHour(-1));

        string[] year = Constants.GetYear(timeManager.GetYears());
        Label_Year.SetText(false, year[0] + year[1] + LanguageMgr.GetInstance.GetText("Nomal_15"));
        Label_Year_C.SetText(false, year[2]);

        string[] date = Constants.GetDate(timeManager.GetMonths(),TimeManager.GetDays());
        Label_Month.SetText(false, date[0]);
        Label_Month_C.SetText(false, date[2]);
        Label_Day.SetText(false, date[1]);
        Label_Day_C.SetText(false, timeManager.GetSolarTerms());

        string festival = timeManager.GetFestival();
        if (festival.Equals(""))
            GameObject_Holiday.SetActive(false);
        else
        {
            GameObject_Holiday.SetActive(true);
            Label_Holiday.SetText(false, festival);
        }

        TimeFrame timeFrame = timeManager.GetCurrTimeFrame();
        switch (timeFrame)
        {
            case TimeFrame.Morning:
                Label_DayTime.SetText(true, "Nomal_66");
                //Sprite_DayTime
                break;
            case TimeFrame.DayTime:
                Label_DayTime.SetText(true, "Nomal_67");
                //Sprite_DayTime
                break;
            case TimeFrame.Evening:
                Label_DayTime.SetText(true, "Nomal_68");
                //Sprite_DayTime
                break;
            case TimeFrame.Night:
                Label_DayTime.SetText(true, "Nomal_69");
                //Sprite_DayTime
                break;
        }
    }

    private void UnfoldTime(GameObject btn)
    {
        GameObject_NowTime.SetActive(!GameObject_NowTime.activeSelf);
    }

    void ClickAnswer()
    {
        //按ESC弹出游戏暂停菜单
        if (Input.GetKeyDown(KC.Key_SystemTip) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            //ESCPauseMenu(Button_system);
            KC.windowIndex = "esc";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_SystemTip) && KC.windowIndex == "esc")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }

        //C 角色界面
        if (Input.GetKeyDown(KC.Key_RoleInfo) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            //Open_Button_Role(Button_role);
            KC.windowIndex = "C";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_RoleInfo) && KC.windowIndex == "C")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }

        //B  背包界面
        if (Input.GetKeyDown(KC.Key_Bag) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            //Open_Button_Bag(Button_bag);
            KC.windowIndex = "B";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_Bag) && KC.windowIndex == "B")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }

        //T  任务界面
        if (Input.GetKeyDown(KC.Key_Task) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            //Open_Button_Task(Button_task);
            KC.windowIndex = "T";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_Task) && KC.windowIndex == "B")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }

        //N  消息界面
        if (Input.GetKeyDown(KC.Key_News) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            //Open_Button_News(Button_news);
            KC.windowIndex = "N";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_News) && KC.windowIndex == "B")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }

        //M  地图界面
        if (Input.GetKeyDown(KC.Key_Map) && KC.windowIndex == "begin" && KC.isAllow == true)
        {
            Open_Button_Map(Button_Map);
            KC.windowIndex = "M";
            KC.isAllow = false;
        }
        else if (Input.GetKeyDown(KC.Key_Map) && KC.windowIndex == "B")
        {
            KC.windowIndex = "begin";
            KC.isAllow = true;
        }
    }


    void ClickControl()
    {
        //初始化各隐藏界面


        //角色面板按钮响应
        //UIEventListener.Get(Button_role).onClick = Open_Button_Role;
        //UIEventListener.Get(Button_bag).onClick = Open_Button_Bag;
        //UIEventListener.Get(Button_news).onClick = Open_Button_News;
        //UIEventListener.Get(Button_system).onClick = ESCPauseMenu;
        //UIEventListener.Get(Button_task).onClick = Open_Button_Task;
        UIEventListener.Get(Button_Map).onClick = Open_Button_Map;
        UIEventListener.Get(Button_Map).onHover = Hover_Button_Map;

        UIEventListener.Get(Button_NowTime).onClick = UnfoldTime; // 点击当前时间展开日期
        UIEventListener.Get(Button_Change).onClick = ChangeTimeModel; // 点击当前时间展开日期
        
    }

    /// <summary>
    /// 系统选项菜单
    /// </summary>
    void ESCPauseMenu(GameObject btn)
    {
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemMenu, UI_Root, Vector3.zero, Vector3.one);
    }

    //角色面板系列按钮响应方法
    void Open_Button_Role(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Role, UI_Root, Vector3.zero, Vector3.one);
        go.GetComponentInChildren<Role_MainUI>().Open();
        //go.GetComponentInChildren<Role_MainUI>().roleImageState = go.GetComponentInChildren<Role_MainUI>().armatureComponent.animation.Play("idle");
    }
    void Open_Button_Bag(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Bag, UI_Root.transform, Vector3.zero, Vector3.one);
        go.GetComponent<BagUI>().ShowBag();
    }
    void Open_Button_News(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_News, UI_Root, Vector3.zero, Vector3.one);
        //go.GetComponent<NewsUI>().SetNew ();
    }
    void Open_Button_Task(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Task, UI_Root, Vector3.zero, Vector3.one);
        go.GetComponent<TaskUI>().ShowAll();
    }
    void Open_Button_Map(GameObject btn)
    {

    }
    void Hover_Button_Map(GameObject btn, bool isHover)
    {
        if (isHover)
        {
            GameObject_PosInfo.SetActive(true);
        }
        else
        {
            GameObject_PosInfo.SetActive(false);
        }
    }



}
