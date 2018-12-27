// ======================================================================================
// 文 件 名 称：IdentityAll.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 21:01:07
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 21:01:07
// ======================================================================================
// 功 能 描 述：选择身份，领取对应任务。
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityAllUI : MonoBehaviour {
    public GameObject GameObject_List;
    public GameObject GameObject_JobPos1;
    public GameObject GameObject_JobPos2;
    public GameObject Button_JobEX;

    public GameObject GameObject_Info;
    public GameObject GameObject_IdenScroll;
    public GameObject GameObject_IdenPos;
    public GameObject Button_IdenEX;
    public GameObject GameObject_DescScroll;
    public GameObject Lable_DescEX;
    public GameObject GameObject_OpenScroll;
    public GameObject GameObject_OpenPos;
    public GameObject Botton_NeedEX;
    public GameObject Button_Next;
    public GameObject Button_BackMenu;
    public GameObject GameObject_Trade;
    public GameObject GameObject_TypePos;
    public GameObject Button_TypeEX;
    public GameObject GameObject_TradeScroll;
    public GameObject GameObject_TradePos;
    public GameObject Button_TradeEX;
    public GameObject Button_Task;
    public GameObject Sprite_TradeBlack;

    public GameObject Button_DesBack;
    public GameObject Sprite_Black;

    GameObject GameObject_AlreadyChoose;

    int tradeID;

    void Start () {
        GameObject_List.SetActive(true);
        GameObject_Info.SetActive(false);
        GameObject_Trade.SetActive(false);

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        tradeID = 0;

        CreatJobButton();
        ClickControl();


    }
	

	void Update () {

	}


    /// <summary>
    /// 生成列表按钮
    /// </summary>
    void CreatJobButton()
    {
        Button_JobEX.SetActive(true);

        //赋值：职业身份10001：百姓
        GameObject go_10001 = CommonFunc.GetInstance.UI_Instantiate(Button_JobEX.transform, GameObject_JobPos1.transform, Vector3.zero, Vector3.one).gameObject;
        go_10001.name = "10001";
        SetJobButton(go_10001);

        //赋值：职业身份：其他
        int row = -1;//行，Y轴
        int col = 0;//列，X轴
        //foreach (var item in SelectDao.GetDao().SelectIndetityByType(1))
        //{
        //    if (item.id == 10001)
        //    {
        //        continue;
        //    }
        //    if (col%5==0)
        //    {
        //        col = 0;
        //        row++;
        //    }
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_JobEX.transform, GameObject_JobPos2.transform, new Vector3(0 + col * (-100), +row * (-70), 0), Vector3.one).gameObject;
        //    go.name = item.id.ToString();
        //    SetJobButton(go);
        //    col++;
        //}

        Button_JobEX.SetActive(false);
    }

    /// <summary>
    /// 点击职业按钮
    /// </summary>
    /// <param name="btn"></param>
    void SetJobButton(GameObject btn)
    {
        int id = int.Parse(btn.name);
        foreach (Transform child in btn.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Label_Job")
            {
                //child.GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectIdentity(id).identityName);
            }
            if (child.name == "Sprite_JobIcon")
            {
                //child.GetComponent<UISprite>().spriteName = SelectDao.GetDao().SelectIdentity(id).icon;
            }
            if (child.name == "Sprite_Choose")
            {
                child.gameObject.GetComponent<UISprite>().enabled = false;
            }
        }
        UIEventListener.Get(btn).onClick = OpenInfo;
        UIEventListener.Get(btn).onHover = CommonFunc.GetInstance.ButtonSelected_Hover;
    }


    /// <summary>
    /// 打开职业对应身份界面
    /// </summary>
    /// <param name="btn"></param>
    void OpenInfo(GameObject btn)
    {
        GameObject_List.SetActive(false);
        GameObject_Info.SetActive(true);
        UIEventListener.Get(Button_BackMenu).onClick = BackMain;
        Button_IdenEX.SetActive(true);        

        NGUITools.DestroyChildren(GameObject_IdenPos.transform);

        int idenID = int.Parse(btn.name);
        int count = 0;

        //foreach (var item in SelectDao.GetDao().SelectIndetityByFront(idenID))
        {
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_IdenEX.transform, GameObject_IdenPos.transform,Vector3.zero, Vector3.one).gameObject;
            //go.name = item.id.ToString();
            //int id = item.id;

            //默认显示第一个的数据
            if (count == 0)
            {
                SelectIdenButton(go);
            }

            foreach (Transform child in go.GetComponentsInChildren<Transform>())
            {
                if (child.name == "Label_IdenName")
                {
                    //child.GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectIdentity(id).identityName);
                }
                if (child.name == "Sprite_IdenIcon")
                {
                    //child.GetComponent<UISprite>().spriteName = SelectDao.GetDao().SelectIdentity(id).icon;
                }
                if (child.name == "Sprite_Choose")
                {
                    child.gameObject.GetComponent<UISprite>().enabled = false;
                }
            }

            count++;
            UIEventListener.Get(go).onClick = SelectIdenButton;
        }
        GameObject_IdenPos.GetComponent<UIGrid>().enabled = true;


        Button_IdenEX.SetActive(false);
    }

    /// <summary>
    /// 显示身份详情
    /// </summary>
    /// <param name="btn"></param>
    void SelectIdenButton(GameObject btn)
    {
        Botton_NeedEX.SetActive(true);        

        int idenID = int.Parse(btn.name);

        NGUITools.DestroyChildren(GameObject_OpenPos.transform);

        //Lable_DescEX.GetComponent<UIText>().SetText(false,SelectDao.GetDao().SelectIdentity(idenID).des);
        //if (SelectDao.GetDao().SelectIdentity(idenID).isSign)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Botton_NeedEX.transform, GameObject_OpenPos.transform, Vector3.zero, Vector3.one).gameObject;
        //    go.GetComponentInChildren<UIText>().SetText(true, "Identity_1");
        //}
        //if (SelectDao.GetDao().SelectIdentity(idenID).needMoney>0)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Botton_NeedEX.transform, GameObject_OpenPos.transform, Vector3.zero, Vector3.one).gameObject;
        //    go.GetComponentInChildren<UIText>().SetText(true, "Identity_2");
        //}
        //if (SelectDao.GetDao().SelectIdentity(idenID).needKnow != null)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Botton_NeedEX.transform, GameObject_OpenPos.transform, Vector3.zero, Vector3.one).gameObject;
        //    go.GetComponentInChildren<UIText>().SetText(true, "Identity_3");
        //}
        //if (SelectDao.GetDao().SelectIdentity(idenID).needSkill != null)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Botton_NeedEX.transform, GameObject_OpenPos.transform, Vector3.zero, Vector3.one).gameObject;
        //    go.GetComponentInChildren<UIText>().SetText(true, "Identity_4");
        //}
        //if (SelectDao.GetDao().SelectIdentity(idenID).needBuilding != null)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Botton_NeedEX.transform, GameObject_OpenPos.transform, Vector3.zero, Vector3.one).gameObject;
        //    go.GetComponentInChildren<UIText>().SetText(true, "Identity_5");
        //}
        GameObject_OpenPos.GetComponent<UIGrid>().enabled = true;
        Botton_NeedEX.SetActive(false);

        //判断是否为伙计
        if (idenID == 34001)
        {
            Button_Next.GetComponentInChildren<UIText>().SetText(true, "Identity_7");
            UIEventListener.Get(Button_Next).onClick = OpenIdenTask;
        }
        else
        {
            Button_Next.GetComponentInChildren<UIText>().SetText(true, "Identity_6");
            Button_Next.name = idenID.ToString();
            UIEventListener.Get(Button_Next).onClick = OpenTrade;
        }


    }

    /// <summary>
    /// 开启选择行当界面
    /// </summary>
    /// <param name="btn"></param>
    void OpenTrade(GameObject btn)
    {
        GameObject_Trade.SetActive(true);
        Button_TypeEX.SetActive(true);
        int idenID = int.Parse(btn.name);

        NGUITools.DestroyChildren(GameObject_TypePos.transform);

        int count = 0;

        //if (idenID == 34002)
        //{
        //    string[] types = SelectDao.GetDao().SelectSystem_Config(43).value.Split(';');
        //    foreach (string item in types)
        //    {
        //        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_TypeEX.transform, GameObject_TypePos.transform, Vector3.zero, Vector3.one).gameObject;
        //        go.transform.name = idenID + "_" + item;
        //        if (item == "1")
        //        {
        //            go.GetComponentInChildren<UIText>().SetText(true, "Trade1");
        //        }
        //        else if(item == "2")
        //        {
        //            go.GetComponentInChildren<UIText>().SetText(true, "Trade2");
        //        }
        //        UIEventListener.Get(go).onClick = TradeType;
        //        //默认显示第一个的数据
        //        if (count == 0)
        //        {
        //            TradeType(go);
        //        }
        //        count++;
        //    }
        //    GameObject_TypePos.GetComponent<UIGrid>().enabled = true;           
        //}
        //else if (idenID == 34003)
        //{
        //    string[] types = SelectDao.GetDao().SelectSystem_Config(44).value.Split(';');
        //    foreach (string item in types)
        //    {
        //        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_TypeEX.transform, GameObject_TypePos.transform, Vector3.zero, Vector3.one).gameObject;
        //        go.transform.name = idenID + "_" + item;
        //        if (item == "1")
        //        {
        //            go.GetComponentInChildren<UIText>().SetText(true, "Trade3");
        //        }
        //        else if (item == "2")
        //        {
        //            go.GetComponentInChildren<UIText>().SetText(true, "Trade4");
        //        }
        //        else if (item == "3")
        //        {
        //            go.GetComponentInChildren<UIText>().SetText(true, "Trade5");
        //        }
        //        UIEventListener.Get(go).onClick = TradeType;
        //        //默认显示第一个的数据
        //        if (count == 0)
        //        {
        //            TradeType(go);
        //        }
        //        count++;
        //    }
        //    GameObject_TypePos.GetComponent<UIGrid>().enabled = true;
        //}
        Button_TypeEX.SetActive(false);
        UIEventListener.Get(Sprite_TradeBlack).onClick = CloseTrade;
    }


    /// <summary>
    /// 显示行当对应页签的内容
    /// </summary>
    /// <param name="btn"></param>
    void TradeType(GameObject btn)
    {
        string[] typeID = btn.name.Split('_');
        Button_TradeEX.SetActive(true);
        
        NGUITools.DestroyChildren(GameObject_TradePos.transform);

        int num = 0;//计数器
        foreach (Business_Trade item in SelectDao.GetDao().SelectBusiness_TradeByIden(int.Parse(typeID[0])))
        {
            if(item.type == int.Parse(typeID[1]))
            {
                GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_TradeEX.transform, GameObject_TradePos.transform, Vector3.zero, Vector3.one).gameObject;
                go.GetComponentInChildren<UIText>().SetText(false, item.name);
                foreach (Transform child in go.GetComponentsInChildren<Transform>())
                {
                    if (child.name == "Sprite_TradeIcon")
                    {
                        child.GetComponent<UISprite>().spriteName = item.icon;
                    }
                    if (child.name == "Sprite_Choose")
                    {
                        child.GetComponent<UISprite>().enabled = false;
                    }
                }
                go.name = item.id.ToString();

                if (num == 0)
                {
                    tradeID = item.id;
                }
                UIEventListener.Get(go).onClick = ChooseTrade;
            }
        }
        GameObject_TradePos.GetComponent<UIGrid>().enabled = true;
        Button_TradeEX.SetActive(false);
    }

    /// <summary>
    /// 选中行当图标
    /// </summary>
    /// <param name="btn"></param>
    void ChooseTrade(GameObject btn)
    {
        if (GameObject_AlreadyChoose != null)
        {
            GameObject_AlreadyChoose.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        GameObject_AlreadyChoose = btn;
        if (tradeID != 0)
        {
            CommonFunc.GetInstance.SetButtonState(Button_Task, true);
            UIEventListener.Get(Button_Task).onClick = OpenIdenTask;
        }
    }

    /// <summary>
    /// 开启身份事务
    /// </summary>
    /// <param name="btn"></param>
    void OpenIdenTask(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_NewTips,FindObjectOfType<UIRoot>().transform,Vector3.zero,Vector3.one).gameObject;

        string title = LanguageMgr.GetInstance.GetText("Tips_Title_2");
        int taskid = SelectDao.GetDao().SelectBusiness_Trade(tradeID).taskID;
        string des = SelectDao.GetDao().SelectTask(taskid).name;
        go.GetComponent<NewTipsUI>().SetNew(null,title, des);
    }


    void CloseTrade(GameObject btn)
    {
        GameObject_Trade.SetActive(false);
    }

    private void ClickControl()
    {
        UIEventListener.Get(Sprite_Black).onClick = Back;
        UIEventListener.Get(Button_DesBack).onClick = Back;
    }


    void BackMain(GameObject btn)
    {
        GameObject_Info.SetActive(false);
        GameObject_List.SetActive(true);
    }



    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="btn"></param>
    void Back(GameObject btn)
    {
        Destroy(this.gameObject);
    }


}
