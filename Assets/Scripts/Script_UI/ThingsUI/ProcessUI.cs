// ======================================================================================
// 文 件 名 称：ProcessUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-15 19:30:49
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-15 19:30:49
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessUI : MonoBehaviour {

    public UIText Label_TitleAll;
    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_ListPos_All;
    public GameObject Button_MakeEX;

    public GameObject Button_Make;
    public GameObject GameObject_Scroll_Item;
    public GameObject GameObject_Pos_Item;
    public GameObject GameObject_Scroll_Skill;
    public GameObject GameObject_Pos_Skill;
    public UIText Label_Time;
    public UIText Label_Power;
    public UIText Label_Energy;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    GameObject chooseMake;

    MakeRecipe make_Class;
    int make_ID;
    bool isOne;

    bool isHave_Item;//道具是否足够
    bool isHave_Know;//学识是否为0
    bool isHave_Skill;//技法是否为0
    bool isHave_Power;//体力是否足够
    bool isEnough_Bag;//背包空间是否充足
    bool isEnough_Know;//学识是否足够
    bool isEnough_Skill;//技法是否足够

    float AnimationTime; //动画时间

    void Awake()
    {
        CommonFunc.GetInstance.SetUIPanel(gameObject);
        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_ScrollList_All.GetComponent<UIPanel>().depth + 1;
    }

    void Start () {
        Open(1);//Test

        ClickControl();
    }
	

	void Update () {

	}

    /// <summary>
    /// 初始化界面，显示列表内容
    /// </summary>
    /// <param name="makeType">器具类型</param>
    public void Open(int makeType)
    {
        Label_TitleAll.SetText(true, "MakeRecipe" + makeType);

        int col = 0;//行
        foreach (var item in SelectDao.GetDao().SelectProduceByTool(makeType))
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_MakeEX.transform, GameObject_ListPos_All.transform, new Vector3(0, col * (-45), 0), Vector3.one);
            Transform itemIcon = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, go.Find("GameObject_ItemIconPos"), Vector3.zero, Vector3.one);
            itemIcon.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
            itemIcon.GetComponent<Botton_ItemEXUI>().SetItem(item.composeItemId, item.composeItemNum);
            go.Find("Label_Name").GetComponent<UIText>().SetText(false, item.name);
            go.Find("Label_Count").GetComponent<UIText>().SetText(false, item.composeItemNum.ToString());
            go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
            go.name = item.id.ToString();

            if (col == 0)
            {
                ShowInfo(go.gameObject);
            }
            col++;
            UIEventListener.Get(go.gameObject).onClick = ShowInfo;
            itemIcon.gameObject.name = item.composeItemId.ToString();
            UIEventListener.Get(itemIcon.gameObject).onHover = ShowTips;
        }
        Button_MakeEX.SetActive(false);
    }

    /// <summary>
    /// 显示配方详情
    /// </summary>
    /// <param name="btn"></param>
    void ShowInfo(GameObject btn)
    {
        NGUITools.DestroyChildren(GameObject_Pos_Item.transform);
        NGUITools.DestroyChildren(GameObject_Pos_Skill.transform);
        if (chooseMake != null)
        {
            chooseMake.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        chooseMake = btn;

        make_ID = int.Parse(btn.name);
        make_Class = SelectDao.GetDao().SelectProduce(int.Parse(btn.name));

        //需要道具材料
        int count = 0;
        foreach (var item in make_Class.Dic_item_Need)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_Item.transform, new Vector3(count * 55, 0, 0), Vector3.one);
            
            int itemID = item.Key;
            int itemCount = item.Value;
            go.GetComponent<UIDragScrollView>().scrollView = GameObject_Pos_Item.GetComponent<UIScrollView>();
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, itemCount);
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, true, false);
            go.gameObject.name = itemID.ToString();
            UIEventListener.Get(go.gameObject).onHover = ShowTips;
            count++;
            if (Bag.GetInstance.HaveItem(itemID, itemCount))
            {
                isHave_Item = true;
                go.GetComponent<Botton_ItemEXUI>().Sprite_Choose.GetComponent<UISprite>().color = Color.green;
            }
            else
            {
                isHave_Item = false;
                go.GetComponent<Botton_ItemEXUI>().Sprite_Choose.GetComponent<UISprite>().color = Color.red;
            }
        }

        int knowID = SelectDao.GetDao().SelectProduce(make_ID).KnowType;
        int knowLv = SelectDao.GetDao().SelectProduce(make_ID).KnowLv;
        int skillID = SelectDao.GetDao().SelectProduce(make_ID).skillType;
        int skillLv = SelectDao.GetDao().SelectProduce(make_ID).skillLv;

        //int nowKnowLv = Charactor.GetInstance.GetLv_Skill(true, knowID);
        //int nowSkillLv = Charactor.GetInstance.GetLv_Skill(true, skillID);
        ////需要学识技法
        //int num = 0;
        //if (knowID != 0)
        //{
        //    string skillName = SelectDao.GetDao().SelectKnowledge(knowID).name;
        //    Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_SkillEX, GameObject_Pos_Skill.transform, new Vector3(num * 40, 0, 0), Vector3.one);
        //    go.GetComponent<UIDragScrollView>().scrollView = GameObject_Pos_Skill.GetComponent<UIScrollView>();

        //    num++;
        //    if (nowKnowLv != 0)
        //    {
        //        isHave_Know = true;
        //        if (make_Class.KnowLv > nowKnowLv)
        //        {
        //            isEnough_Know = false;
        //            go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 1);
        //        }
        //        else
        //        {
        //            isEnough_Know = true;
        //            go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 2);
        //        }
        //    }
        //    else
        //    {
        //        isHave_Know = false;
        //        go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 2);
        //    }

        //}
        //if (skillID != 0)
        //{
        //    string skillName = SelectDao.GetDao().SelectSkill(skillID).name;
        //    Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_SkillEX, GameObject_Pos_Skill.transform, new Vector3(num * 40, 0, 0), Vector3.one);
        //    go.GetComponent<UIDragScrollView>().scrollView = GameObject_Pos_Skill.GetComponent<UIScrollView>();

        //    num++;
        //    if (nowSkillLv != 0)
        //    {
        //        isHave_Skill = true;
        //        if (make_Class.skillLv > nowSkillLv)
        //        {
        //            isEnough_Skill = false;
        //            go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 1);
        //        }
        //        else
        //        {
        //            isEnough_Skill = true;
        //            go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 2);
        //        }
        //    }
        //    else
        //    {
        //        isHave_Skill = false;
        //        go.GetComponent<Button_SkillEXUI>().SetState(skillName, knowLv, 2);
        //    }
        //}

        int needPower = SelectDao.GetDao().SelectProduce(make_ID).power;
        int needEnergy = SelectDao.GetDao().SelectProduce(make_ID).energy;
        int nowHp = Charactor.GetInstance.hp;
        int nowTemp = Charactor.GetInstance.temp;

        if (needPower > nowHp)
        {
            isHave_Power = false;
        }
        else
        {
            isHave_Power = true;
        }

        //需要人力部分
        Label_Time.SetText(false, TimeManager.GetTime(TimeManager.TimeUnit.Quarter, make_Class.time));
        Label_Power.SetText(false, needPower.ToString());
        Label_Energy.SetText(false, needEnergy.ToString());

        UIEventListener.Get(Button_Make).onClick = OpenMake;


        //计算存放地剩余空间
        if (Bag.GetInstance.IsAhead_OverCount(make_Class.composeItemNum))
        {
            isEnough_Bag = true;
        }
        else
        {
            isEnough_Bag = false;
        }
    }


    void OpenMake(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;

        if (!isHave_Item) {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_11"));
        }
        else if (!isHave_Know)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_13"));
        }
        else  if (!isHave_Skill)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_15"));
        }
        else if (!isHave_Power)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_18"));
        }
        else  if (!isEnough_Bag)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_12"));
        }
        else
        {
            Destroy(go.gameObject);
            if (isEnough_Know)
            {
                isOne = true;
                if (isEnough_Skill)
                {
                    OpenAnimation();
                }
                else
                {
                    Transform sys2 = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
                    sys2.GetComponent<SystemTipsUI>().SetTipDesc( LanguageMgr.GetInstance.GetText("Tips_System_16"), LanguageMgr.GetInstance.GetText("Tips_8"), LanguageMgr.GetInstance.GetText("Tips_7"));
                }
            }
            else
            {
                isOne = false;
                Transform sys = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
                sys.GetComponent<SystemTipsUI>().SetTipDesc( LanguageMgr.GetInstance.GetText("Tips_System_14"), LanguageMgr.GetInstance.GetText("Tips_8"), LanguageMgr.GetInstance.GetText("Tips_7"));
                if (isEnough_Skill)
                {
                    OpenAnimation();
                }
            }
        }
    }

    /// <summary>
    /// 接收系统提示界面的按钮选择
    /// </summary>
    /// <param name="isYes"></param>
    public void ReceiveSystemTips(bool isYes)
    {
        if (isYes)
        {
            if (isOne)
            {
                OpenAnimation();
            }
            else
            {
                Transform sys3 = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
                sys3.GetComponent<SystemTipsUI>().SetTipDesc( LanguageMgr.GetInstance.GetText("Tips_System_16"), LanguageMgr.GetInstance.GetText("Tips_8"), LanguageMgr.GetInstance.GetText("Tips_7"));
                isOne = true;
            }
        }
    }

    //播放读条动画
    void OpenAnimation()
    {
        Transform animation = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_ProgressAnimation, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        AnimationTime = 1 / animation.GetComponent<ProgressAnimationUI>().animationTime;
        animation.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
        StartCoroutine(StartMake());
    }

    IEnumerator StartMake()
    {
        //N秒动画后
        yield return new WaitForSeconds(AnimationTime);
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
        int successPercent = SelectDao.GetDao().SelectProduce(make_ID).success;
        if (isEnough_Know)
        {
            if (isEnough_Skill)
            {
                successPercent = SelectDao.GetDao().SelectProduce(make_ID).success;
            }
            else
            {
                successPercent -= 15;
            }
        }
        else
        {
            if (!isEnough_Skill)
            {
                successPercent -= 30;
            }
            else
            {
                successPercent -= 15;
            }
        }
        //是否成功,传递生产成功逻辑： 添加物品到背包或者商店货架
        if (Random.value * 100 < successPercent)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_Lable2"));
        }
        else
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_Lable5"));
        }
    }

    //显示道具tips
    void ShowTips(GameObject btn, bool isHover)
    {
        int itemID = int.Parse(btn.name);
        if (isHover)
        {
            ItemTip.SetActive(true);
            ItemTip.GetComponent<ItemTipsUI>().itemTipsSet(itemID, false);
        }
        else
        {
            ItemTip.SetActive(false);
        }
    }

    private void ClickControl()
    {
        UIEventListener.Get(Sprite_Back).onClick = Back;
        UIEventListener.Get(Button_Close).onClick = Back;
    }


    void Back(GameObject btn)
    {
        Destroy(ItemTip);
        Destroy(this.gameObject);
    }
}
