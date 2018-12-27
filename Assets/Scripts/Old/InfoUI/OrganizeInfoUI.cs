// ======================================================================================
// 文件名         ：    OrganizeInfoUI.cs
// 版本号         ：    v1.0.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-18
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    机构信息UI
// ======================================================================================

using UnityEngine;

/// <summary>
/// 机构信息UI
/// </summary>
public class OrganizeInfoUI : MonoBehaviour
{
    public GameObject Button_Close;

    public UIText Label_OrgName;
    public UISprite Sprite_OrgIcon;
    public GameObject Button_Force;
    public GameObject Button_Up;

    public UIText Lable_Desc;

    public Transform GameObject_OrgPos;
    public GameObject Button_UOEX;

    public GameObject Button_Leader;
    public Transform GameObject_MemPos;
    public GameObject Button_MemberEX;
    

    private Transform UI_Root;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();


        UI_Root = FindObjectOfType<UIRoot> ().transform;

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        if (RunTime_Data.UI_Pool ["org"] != null)
            Destroy (RunTime_Data.UI_Pool ["org"]);
        RunTime_Data.UI_Pool ["org"] = gameObject;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Force).onClick = ClickForce;
        UIEventListener.Get (Button_Up).onClick = ClickOrg;
        UIEventListener.Get (Button_Leader).onClick = ClickIdentity;
    }

    private void ClickOrg (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform org_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_OrganizeInfo, UI_Root.transform, Vector3.one, Vector3.zero);
        org_UI.GetComponent<OrganizeInfoUI> ().SetInfo (id);
        org_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickIdentity (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform iden_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_IdentityInfo, UI_Root.transform, Vector3.one, Vector3.zero);
        iden_UI.GetComponent<IdentityInfoUI> ().SetInfo (id);
        iden_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickForce (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform force_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ForceInfo, UI_Root.transform, Vector3.one, Vector3.zero);
        force_UI.GetComponent<ForceInfoUI> ().SetInfo (id);
        force_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

    public void SetInfo (int id)
    {
        SelectDao selectDao = SelectDao.GetDao ();

        Organize org = selectDao.SelectOrganize (id);

        // 机构名称
        Label_OrgName.SetText(false, org.name);

        // 机构图标
        // TOUPDATE 图集应该修改
        //Sprite_OrgIcon.atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
        //Sprite_OrgIcon.spriteName = org.icon;

        // TOUPDATE 隶属势力
        //Button_Force.GetComponentInChildren<UISprite>().atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
        //Button_Force.GetComponentInChildren<UISprite>().spriteName = Constants.Constants.Force_All[org.force].icon;
        Button_Force.name = selectDao.SelectForce (org.force).id.ToString ();

        // 上级机构
        Button_Up.GetComponentInChildren<UILabel> ().text = selectDao.SelectOrganize (org.upId).name;
        Button_Up.name = org.upId.ToString ();
        if (org.upId == 0)
            Button_Up.GetComponent<UIButton> ().enabled = false;

        // 介绍
        Lable_Desc.SetText(false, org.des);

        // 组成机构
        int num = -1;
        var orgList = SelectDao.GetDao ().SelectAllOrganize ();
        foreach (Organize orgItem in orgList)
        {
            if (orgItem.upId == id)
            {
                GameObject clone = Instantiate (Button_UOEX);
                clone.name = orgItem.id.ToString ();

                clone.GetComponentInChildren<UILabel> ().text = orgItem.name;

                clone.transform.parent = GameObject_OrgPos;
                clone.transform.localScale = Vector3.one;
                clone.transform.localPosition = new Vector3 (40 * ++num, 0, 0);

                UIEventListener.Get (clone).onClick = ClickOrg;
            }
        }
        Button_UOEX.SetActive (false);

        // 成员
        if (org.leader != 0)
        {
            //Button_Leader.GetComponentInChildren<UILabel> ().text = SelectDao.GetDao ().SelectIdentity (org.leader).identityName;
            Button_Leader.name = org.leader.ToString ();
        }
        else
            Button_Leader.GetComponentInChildren<UILabel> ().text = "无";

        num = -1;
        //var idenList = SelectDao.GetDao ().SelectAllIndetity ();
        //foreach (Identity iden in idenList)
        //{
        //    if (iden.orgId == id)
        //    {
        //        GameObject clone = Instantiate (Button_MemberEX);
        //        clone.name = iden.id.ToString ();

        //        clone.GetComponentInChildren<UILabel> ().text = iden.identityName;

        //        clone.transform.parent = GameObject_MemPos;
        //        clone.transform.localScale = Vector3.one;
        //        clone.transform.localPosition = new Vector3 (40 * ++num, 0, 0);

        //        UIEventListener.Get (clone).onClick = ClickIdentity;
        //    }
        //}
        Button_MemberEX.SetActive (false);
    }

}