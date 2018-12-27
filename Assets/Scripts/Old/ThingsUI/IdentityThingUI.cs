// ======================================================================================
// 文 件 名 称：IdentityThingUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-19 18:43:12
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-19 18:43:12
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityThingUI : MonoBehaviour {

    public GameObject Button_Org;
    public UIText Label_Org;

    public GameObject GameObject_IdenInfo;
    public GameObject Botton_Prof;
    public UIText Label_Name_Prof;
    public GameObject Botton_Officer;
    public UIText Label_Name_Officer;

    public GameObject GameObject_ButtonInfo;
    public UIText Label_Job;
    public GameObject GameObject_ThingPos;
    public GameObject GameObject_Open;
    public GameObject GameObject_OpenPos;

    GameObject chooseGo;

    List<IndexMenu_Job> list_Main;//当前职位的全部菜单按钮
    List<IndexMenu_Job> list_1;//一级菜单的全部按钮
    List<IndexMenu_Job> list_2;//当前二级菜单的全部按钮

    int job_ID;
    void Awake()
    {
        list_Main = new List<IndexMenu_Job>();
        list_1 = new List<IndexMenu_Job>();
        list_2 = new List<IndexMenu_Job>();
    }

    void Start () {

	}
	

	void Update () {
		
	}

    /// <summary>
    /// 切换界面显示状态
    /// </summary>
    /// <param name="info">是否显示身份信息界面状态</param>
    /// <param name="office_IdenID">职业ID</param>
    /// <param name="job_IdenID">职事ID</param>
    public void WindowState(bool info,int office_IdenID,int job_IdenID)
    {
        if (info)
        {
            GameObject_IdenInfo.SetActive(true);
            GameObject_ButtonInfo.SetActive(false);
        }
        else
        {
            GameObject_IdenInfo.SetActive(false);
            GameObject_ButtonInfo.SetActive(true);
        }

        //身份信息赋值
        //int orgID = SelectDao.GetDao().SelectIdentity(job_IdenID).orgId;
        //Button_Org.name = orgID.ToString();
        //UIEventListener.Get(Button_Org).onClick = OrgInfo;
        //Label_Org.SetText(false, SelectDao.GetDao().SelectOrganize(orgID).name);

        //Botton_Prof.name = office_IdenID.ToString();
        //UIEventListener.Get(Botton_Prof).onClick = IdenInfo;
        //Label_Name_Prof.SetText(false, SelectDao.GetDao().SelectIdentity(office_IdenID).identityName);

        //Botton_Officer.name = job_IdenID.ToString();
        //UIEventListener.Get(Botton_Officer).onClick = IdenInfo;
        //Label_Name_Prof.SetText(false, SelectDao.GetDao().SelectIdentity(job_IdenID).identityName);

        //职事选单赋值
        GameObject_Open.SetActive(false);
        list_Main = SelectDao.GetDao().SelectIndexMenu_JobByJob(job_IdenID);
        foreach (var item in list_Main)
        {
            if (item.lv == 1)
            {
                list_1.Add(item);
            }
        }

        int row = 0;
        foreach (var item in list_1)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdenThingEX, GameObject_ThingPos.transform, new Vector3(0, row * 70, 0), Vector3.one);
            go.GetComponentInChildren<UIText>().SetText(false, item.name);
            go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
            go.gameObject.name = item.id.ToString();
            UIEventListener.Get(go.gameObject).onClick = OpenMenu;

            row++;
        }
    }

    //打开机构信息界面
    void OrgInfo(GameObject btn)
    {
        int id = int.Parse(btn.name);
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_OrganizeInfo, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).GetComponent<OrganizeInfoUI>().SetInfo(id);
    }
    //打开身份信息界面
    void IdenInfo(GameObject btn)
    {
        int id = int.Parse(btn.name);
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_IdentityInfo, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).GetComponent<IdentityInfoUI>().SetInfo(id);
    }

    /// <summary>
    /// 打开二级菜单
    /// </summary>
    /// <param name="btn"></param>
    void OpenMenu(GameObject btn)
    {
        GameObject_Open.SetActive(true);
        NGUITools.DestroyChildren(GameObject_OpenPos.transform);

        if (chooseGo!=null)
        {
            chooseGo.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        chooseGo = btn;

        int id = int.Parse(btn.name);
        foreach (var item in list_Main)
        {
            if (item.behind == id)
            {
                list_2.Add(item);
            }
        }
        int count = list_2.Count;

        int col = 0;
        foreach (var item in list_2)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdenThingEX, GameObject_OpenPos.transform, new Vector3(col*60,0,0), Vector3.one);
            go.GetComponentInChildren<UIText>().SetText(false, item.name);
            go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
            go.gameObject.name = item.window.ToString();
            UIEventListener.Get(go.gameObject).onClick = OpenWindow;

            col++;
        }
    }

    /// <summary>
    /// 打开对应界面
    /// </summary>
    /// <param name="btn"></param>
    void OpenWindow(GameObject btn)
    {
        GameObject_Open.SetActive(false);
        int windowIndex = int.Parse(btn.name);

        Transform go = CommonFunc.GetInstance.UI_Instantiate(WindowPath(windowIndex), FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        //根据界面不同传递不同参数
    }

    string WindowPath(int id)
    {
        string path = "";
        switch (id)
        {
            case 1://
                path = Data_Static.UIpath_OrganizeDes;
                break;
            case 2://
                path = Data_Static.UIpath_OrganizeAdmin;
                break;
            case 3://
                path = Data_Static.UIpath_OrgTask_Fixed;
                break;
            case 4://
                path = Data_Static.UIpath_OrgTask_Get;
                break;
            case 5://
                path = Data_Static.UIpath_Holiday;
                break;
            case 6://
                path = Data_Static.UIpath_Holiday;
                break;
            case 7://
                path = Data_Static.UIpath_SellTable;
                break;
            case 8://
                path = "";
                break;
            case 9://
                path = Data_Static.UIpath_OrgRest_Shop;
                break;
            case 10://
                path = Data_Static.UIpath_ContactLeader;
                break;
        }
        return path;
    }


}
