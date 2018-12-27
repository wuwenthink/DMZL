// ======================================================================================
// 文件名         ：    IdentityInfoUI.cs
// 版本号         ：    v1.1.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-18
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    身份详细信息UI
// ======================================================================================

using UnityEngine;

/// <summary>
/// 身份详细信息UI
/// </summary>
public class IdentityInfoUI : MonoBehaviour
{
    public GameObject Button_DesBack;

    public UIText Label_Identity;
    public UIText Label_Type;
    public GameObject Button_Org;
    public UIText Lable_EX;
    public GameObject GameObject_Pos_Skill;
    public GameObject GameObject_Pos_Duty;
    public GameObject GameObject_Pos_Salary;
    public GameObject GameObject_Pos_Need;
    public GameObject Botton_NeedEX;  

    private Transform UI_Root;

    private GameObject lableTips;

    // 初始化的方法
    private void Start ()
    {

        UI_Root = FindObjectOfType<UIRoot> ().transform;

        ClickControl ();

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        if (RunTime_Data.UI_Pool ["iden"] != null)
            Destroy (RunTime_Data.UI_Pool ["iden"]);
        RunTime_Data.UI_Pool ["iden"] = gameObject;
    }

    public void SetInfo (int id)
    {
        // 获取身份
        //Identity identity = SelectDao.GetDao ().SelectIdentity (id);

        //// 身份类别、状态、名称
        //Label_Type.SetText(false, identity.type.ToString ());
        //// TOUPDATE 状态待做
        ////GameObject_Title.transform.Find("Label_IState").GetComponent<UILabel>().text
        //Label_Identity.SetText(false, identity.identityName);

        //// 介绍
        //Lable_EX.SetText(false, identity.des);

        //// 机构
        //Button_Org.GetComponentInChildren<UILabel> ().text = identity.orgName;
        //Button_Org.name = identity.orgId.ToString ();

        //if (identity.orgId == 0)
        //    Button_Org.GetComponent<UIButton> ().enabled = false;

        //// TOUPDATE 学识
        //if (identity.skill == null)
        //{
        //    GameObject none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Skill.transform, Vector3.one, Vector3.zero).gameObject;
        //}
        //else
        //{

        //}

        //// TOUPDATE 职能
        //if (identity.function == null)
        //{
        //    GameObject none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Duty.transform, Vector3.one, Vector3.zero).gameObject;
        //}
        //else
        //{
        //}

        //// 任务需求
        //bool flag = false;
        //int num = -1;
        //if (identity.isSign)
        //{
        //    GameObject task = Instantiate (Botton_NeedEX);
        //    task.transform.parent = GameObject_Pos_Need.transform;
        //    task.transform.localScale = Vector3.one;
        //    task.transform.localPosition = new Vector3 (86, 48 - 40 * ++num, 0);

        //    task.GetComponentInChildren<UILabel> ().text = "入籍";

        //    Transform yes = task.transform.Find ("Botton_Choose/Sprite_Yes");

        //    // TOUPDATE 这里要判断角色是否已经入籍，改变yes是否显示

        //    yes.gameObject.SetActive (false);

        //    flag = true;
        //}
        //if (identity.need != null)
        //{
        //    foreach (int taskId in identity.need)
        //    {
        //        GameObject task = Instantiate (Botton_NeedEX);
        //        task.transform.parent = GameObject_Pos_Need.transform;
        //        task.transform.localScale = Vector3.one;
        //        task.transform.localPosition = new Vector3 (86, 48 - 40 * ++num, 0);

        //        // TOUPDATE 这里应该显示任务名
        //        task.GetComponentInChildren<UILabel> ().text = taskId.ToString ();

        //        Transform yes = task.transform.Find ("Botton_Choose/Sprite_Yes");

        //        // TOUPDATE 判断任务是否已经完成，改变yes是否显示

        //        yes.gameObject.SetActive (false);
        //    }

        //    flag = true;
        //}
        //if (!flag)
        //{
        //    GameObject none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Need.transform, Vector3.one, Vector3.zero).gameObject;
        //}

        //// 福利俸禄
        //if (identity.salary == null)
        //{
        //    GameObject none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Salary.transform, Vector3.one, Vector3.zero).gameObject;
        //}
        //else
        //{
        //    num = -1;
        //    foreach (int itemId in identity.salary.Keys)
        //    {
        //        GameObject item = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, GameObject_Pos_Salary.transform, Vector3.one, new Vector3 (++num * 60, 0, 0)).gameObject;
        //        item.name = itemId.ToString ();

        //        item.transform.Find ("Sprite_SalaryIcon").GetComponentInChildren<UISprite> ().spriteName = Constants.Items_All [itemId].icon;

        //        item.GetComponentInChildren<UILabel> ().text = identity.salary [itemId].ToString ();

        //        UIEventListener.Get (item).onClick = ClickItem;
        //    }
        //}


    }

    /// <summary>
    /// 点击福利中的道具
    /// </summary>
    /// <param name="btn"></param>
    private void ClickItem (GameObject btn)
    {

    }

    private void ClickControl ()
    {
        UIEventListener.Get (Button_DesBack).onClick = Back;

        UIEventListener.Get (Button_Org).onClick = OpenOrg;
    }

    /// <summary>
    /// 点击机构
    /// </summary>
    /// <param name="btn"></param>
    private void OpenOrg (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject org = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_OrganizeInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        org.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        org.GetComponent<OrganizeInfoUI> ().SetInfo (id);
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

}