using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingUI : MonoBehaviour {
    public TweenPosition UI_Pos;

    public GameObject Button_Hp;
    public UISprite Background_Hp;
    public UIProgressBar ProgressBar_health;
    public UIProgressBar ProgressBar_mind;
    public GameObject Button_ActionPoint;
    public UISprite Background_ActionPoint;

    public GameObject Button_GiveUp;
    public GameObject Button_RunAway;
    public GameObject Button_Cancel;

    public GameObject Button_NextRound;

    public GameObject Button_HeadIconEX;
    public GameObject GameObject_OurTeamPos;
    public GameObject GameObject_EnemyTeamPos;

    KeyControl KC;
    Transform UI_Root;
    TimeManager timeManager;
    Transform GameOver;
    Charactor charactor;


    /// <summary>
    /// 已使用行动值
    /// </summary>
    int actionPoint;
    void Start ()
    {
        charactor = Charactor.GetInstance;
        KC = FindObjectOfType<KeyControl>();
        UI_Root = FindObjectOfType<UIRoot>().transform;
        timeManager = FindObjectOfType<TimeManager>();

        timeManager.health = charactor.health;
        timeManager.hungry = charactor.hunger;
        timeManager.temp = charactor.temp;
        timeManager.mood = charactor.mood;

        GameOver = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_GameOver, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        GameOver.gameObject.SetActive(false);


        actionPoint = 0;
    }
	

	void Update () {
        if (actionPoint <= 0)
        {
            actionPoint = 0;

        }
	}


    void FixedUpdate()
    {
        //角色移动控制
        if (UICamera.isOverUI)
        {
            return;
        }
        else
        {
            actionPoint = Mathf.CeilToInt(FindObjectOfType<RoleControl_Move>().RoleMove(3f));
            charactor.action -= actionPoint;
        }
    }

    public void FightStart(string fightType,bool isLoot,bool isPretent)
    {
        UI_Pos.enabled = true;
        UI_Pos.PlayForward();

        //传递战斗开始选项
        //isLoot isPretent


        //敌我双方角色数据获得
        int ourCount = 0;
        foreach (var item in RunTime_Data.team_Our)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_HeadIconEX.transform, GameObject_OurTeamPos.transform, Vector3.zero, Vector3.one);
            go.localPosition += new Vector3(0, ourCount * 60, 0);
            //给角色头像赋值
            go.Find("Sprite_HeadIcon").GetComponent<UISprite>().spriteName = RunTime_Data.RolePool[item].headIcon;
            go.gameObject.name = item.ToString();
            UIEventListener.Get(go.gameObject).onClick = ChooseRole_Our;

            //默认选中一个角色（玩家角色）

            ourCount++;
        }
        int enemyCount = 0;
        foreach (var item in RunTime_Data.team_Enemy)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_HeadIconEX.transform, GameObject_EnemyTeamPos.transform, Vector3.zero, Vector3.one);
            go.localPosition += new Vector3(0, enemyCount * 60, 0);
            //给角色头像赋值
            go.Find("Sprite_HeadIcon").GetComponent<UISprite>().spriteName = RunTime_Data.RolePool[item].headIcon;
            go.gameObject.name = item.ToString();
            UIEventListener.Get(go.gameObject).onClick = ChooseRole_Enemy;

            enemyCount++;
        }

    }

    /// <summary>
    /// 选中我方人物后的处理：我方人物居中，获得我方角色的信息和技能信息
    /// </summary>
    /// <param name="btn"></param>
    void ChooseRole_Our(GameObject btn)
    {
        int id = int.Parse(btn.name);
    }

    /// <summary>
    /// 选中敌方人物后的处理：读取信息，打开信息界面
    /// </summary>
    /// <param name="btn"></param>
    void ChooseRole_Enemy(GameObject btn)
    {
        int id = int.Parse(btn.name);
    }

    /// <summary>
    /// 玩家状态更新
    /// </summary>
    public void SetRoleState()
    {
        //健康和精神根据BUFF和事件进行增减
        ProgressBar_health.value = (float)timeManager.health / 100;
        ProgressBar_mind.value = (float)timeManager.hungry / 100;
        Background_Hp.fillAmount = (float)timeManager.hp / 100;

        //行动值改变
        Background_ActionPoint.fillAmount = 1;

        //人物死亡，游戏结束，弹出结局。（暂时回到主界面）
        if (timeManager.hp < 0)
        {
            GameOver.gameObject.SetActive(true);
            GameObject box = GameOver.GetComponentInChildren<BoxCollider>().gameObject;
            UIEventListener.Get(box).onClick = backMain;
        }
    }

    void ClickControl()
    {
        //角色面板按钮响应
        UIEventListener.Get(Button_GiveUp).onClick = GiveUp;
        UIEventListener.Get(Button_GiveUp).onClick = RunAway;
        UIEventListener.Get(Button_GiveUp).onClick = Cancel;

    }
    /// <summary>
    /// 撤销行动
    /// </summary>
    /// <param name="btn"></param>
    void GiveUp(GameObject btn)
    {
        FindObjectOfType<Scene_Fight>().giveUpAction();
        charactor.action += actionPoint;
    }
    void RunAway(GameObject btn)
    {

    }
    void Cancel(GameObject btn)
    {

    }

    void backMain(GameObject btn)
    {
        StartCoroutine(CommonFunc.GetInstance.ToSceneLoading(Data_Static.SceneName_Main_Menu));
    }

    public void FightOver()
    {
        UI_Pos.enabled = true;
        UI_Pos.PlayReverse();

        Destroy(this.gameObject);
        Destroy(FindObjectOfType<Scene_Fight>().gameObject);
    }
}
