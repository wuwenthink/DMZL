// ======================================================================================
// 文件名         ：    AreaInfoUI.cs
// 版本号         ：    v1.0.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-21
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    地区信息UI
// ======================================================================================

using UnityEngine;

/// <summary>
/// 地区信息UI
/// </summary>
public class AreaInfoUI : MonoBehaviour
{    
    public GameObject Button_Close;

    public GameObject Sprite_AreaIcon;
    public UILabel Label_AreaName;

    public GameObject Button_Force;

    public UILabel Label_AdminName;
    public GameObject Button_Up;
    public GameObject Button_Center;
    public GameObject Button_Leader;

    public GameObject Botton_Desc;
    public GameObject Botton_Org;
    public GameObject Botton_People;
    public GameObject Botton_Finance;

    public GameObject GameObject_Desc;
    public GameObject GameObject_Org;
    public GameObject GameObject_People;
    public GameObject GameObject_Finance;

    public UILabel Lable_HistoryEX;
    public UILabel Lable_GeographyEX;

    public GameObject Button_AdminMax;
    public GameObject Button_MilMax;
    public GameObject Button_EduMax;
    public Transform GameObject_OtherPos;
    public GameObject Button_OtherEX;

    public GameObject GameObject_AreaNum;
    public GameObject GameObject_PeopleNum;
    public GameObject GameObject_Money;
    public GameObject GameObject_Food;

    public UILabel Label_Tex_Food;
    public UILabel Label_Tex_Business;
    


    private Transform UI_Root;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();


        UI_Root = FindObjectOfType<UIRoot> ().transform;

        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        if (RunTime_Data.UI_Pool ["area"] != null)
            Destroy (RunTime_Data.UI_Pool ["area"]);
        RunTime_Data.UI_Pool ["area"] = gameObject;
    }

    public void SetInfo (int _id)
    {
        var selectDao = SelectDao.GetDao ();
        Map_Area area = selectDao.SelectArea (_id);

        // TOUPDATE 地区图标 Sprite_AreaIcon

        //// 地区名
        //Label_AreaName.text = area.areaName;

        //// TOUPDATE 隶属势力
        ////Button_Force.GetComponentInChildren<UISprite>().atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
        ////Button_Force.GetComponentInChildren<UISprite>().spriteName = Constants.Constants.Force_All[area.runtime_Data.force].icon;
        //Button_Force.name = area.runtime_Data.所属势力id.ToString ();

        //// 行政单位
        //Label_AdminName.text = area.adminName;

        //// 基础属性
        //Button_Up.GetComponentInChildren<UILabel> ().text = selectDao.SelectArea (area.aheadId).areaName;
        //Button_Up.name = area.aheadId.ToString ();

        //Button_Center.GetComponentInChildren<UILabel> ().text = area.center;

        //if (area.runtime_Data.最高官职身份id != 0)
        //{
        //    Button_Leader.GetComponentInChildren<UILabel> ().text = selectDao.SelectIdentity (area.runtime_Data.最高官职身份id).identityName;
        //    Button_Leader.name = area.runtime_Data.最高官职身份id.ToString ();
        //}

        //// 介绍
        //Lable_HistoryEX.text = area.history;
        //Lable_GeographyEX.text = area.geography;

        //// 机构
        //if (area.runtime_Data.最高行政机构id != 0)
        //{
        //    Button_AdminMax.GetComponentInChildren<UILabel> ().text = selectDao.SelectOrganize (area.runtime_Data.最高行政机构id).name;
        //    Button_AdminMax.name = area.runtime_Data.最高行政机构id.ToString ();
        //}

        //if (area.runtime_Data.最高军事机构id != 0)
        //{
        //    Button_MilMax.GetComponentInChildren<UILabel> ().text = selectDao.SelectOrganize (area.runtime_Data.最高军事机构id).name;
        //    Button_MilMax.name = area.runtime_Data.最高军事机构id.ToString ();
        //}

        //if (area.runtime_Data.最高学府机构id != 0)
        //{
        //    Button_EduMax.GetComponentInChildren<UILabel> ().text = selectDao.SelectOrganize (area.runtime_Data.最高学府机构id).name;
        //    Button_EduMax.name = area.runtime_Data.最高学府机构id.ToString ();
        //}

        //if (area.runtime_Data.其他机构id != null && area.runtime_Data.其他机构id.Count > 0)
        //{
        //    int num = -1;
        //    foreach (int orgId in area.runtime_Data.其他机构id)
        //    {
        //        GameObject org = Instantiate (Button_OtherEX);
        //        org.name = orgId.ToString ();

        //        org.transform.parent = GameObject_OtherPos;
        //        org.transform.localScale = Vector3.one;
        //        org.transform.localPosition = new Vector3 (0, -30 * ++num, 0);

        //        org.GetComponentInChildren<UILabel> ().text = selectDao.SelectOrganize (orgId).name;

        //        UIEventListener.Get (org).onClick = ClickOrganize;
        //    }
        //}
        //Button_OtherEX.SetActive (false);

        //// 民情
        //GameObject_AreaNum.transform.Find ("Label_Value").GetComponent<UILabel> ().text = area.runtime_Data.地亩;
        //GameObject_PeopleNum.transform.Find ("Label_Value").GetComponent<UILabel> ().text = area.runtime_Data.丁口;
        //GameObject_Money.transform.Find ("Label_Value").GetComponent<UILabel> ().text = area.runtime_Data.存银;
        //GameObject_Food.transform.Find ("Label_Value").GetComponent<UILabel> ().text = area.runtime_Data.存粮;

        //// 财政
        //Label_Tex_Food.text = area.runtime_Data.田稅百分比;
        //Label_Tex_Business.text = area.runtime_Data.商税百分比;
    }

    private void ClickOrganize (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject org_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_OrganizeInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        org_UI.GetComponent<OrganizeInfoUI> ().SetInfo (id);
        org_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Force).onClick = ClickForce;

        UIEventListener.Get (Botton_Desc).onClick = ClickDesc;
        UIEventListener.Get (Botton_Org).onClick = ClickOrg;
        UIEventListener.Get (Botton_People).onClick = ClickPeople;
        UIEventListener.Get (Botton_Finance).onClick = ClickFinance;

        UIEventListener.Get (Button_Up).onClick = ClickArea;
        UIEventListener.Get (Button_Leader).onClick = ClickIndentity;

        UIEventListener.Get (Button_AdminMax).onClick = ClickOrganize;
        UIEventListener.Get (Button_MilMax).onClick = ClickOrganize;
        UIEventListener.Get (Button_EduMax).onClick = ClickOrganize;
    }

    private void ClickIndentity (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject iden_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_IdentityInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        iden_UI.GetComponent<IdentityInfoUI> ().SetInfo (id);
        iden_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickArea (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject area_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_AreaInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        area_UI.GetComponent<AreaInfoUI> ().SetInfo (id);
        area_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

    private void ClickForce (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject force_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ForceInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        force_UI.GetComponent<ForceInfoUI> ().SetInfo (id);
        force_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickDesc (GameObject btn)
    {
        Botton_Desc.GetComponent<UIButton> ().enabled = false;
        Botton_Org.GetComponent<UIButton> ().enabled = true;
        Botton_People.GetComponent<UIButton> ().enabled = true;
        Botton_Finance.GetComponent<UIButton> ().enabled = true;

        Botton_Desc.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        Botton_Org.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_People.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Finance.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;

        GameObject_Desc.SetActive (true);
        GameObject_Org.SetActive (false);
        GameObject_People.SetActive (false);
        GameObject_Finance.SetActive (false);
    }

    private void ClickOrg (GameObject btn)
    {
        Botton_Desc.GetComponent<UIButton> ().enabled = true;
        Botton_Org.GetComponent<UIButton> ().enabled = false;
        Botton_People.GetComponent<UIButton> ().enabled = true;
        Botton_Finance.GetComponent<UIButton> ().enabled = true;

        Botton_Desc.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Org.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        Botton_People.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Finance.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;

        GameObject_Desc.SetActive (false);
        GameObject_Org.SetActive (true);
        GameObject_People.SetActive (false);
        GameObject_Finance.SetActive (false);
    }

    private void ClickPeople (GameObject btn)
    {
        Botton_Desc.GetComponent<UIButton> ().enabled = true;
        Botton_Org.GetComponent<UIButton> ().enabled = true;
        Botton_People.GetComponent<UIButton> ().enabled = false;
        Botton_Finance.GetComponent<UIButton> ().enabled = true;

        Botton_Desc.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Org.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_People.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        Botton_Finance.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;

        GameObject_Desc.SetActive (false);
        GameObject_Org.SetActive (false);
        GameObject_People.SetActive (true);
        GameObject_Finance.SetActive (false);
    }

    private void ClickFinance (GameObject btn)
    {
        Botton_Desc.GetComponent<UIButton> ().enabled = true;
        Botton_Org.GetComponent<UIButton> ().enabled = true;
        Botton_People.GetComponent<UIButton> ().enabled = true;
        Botton_Finance.GetComponent<UIButton> ().enabled = false;

        Botton_Desc.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Org.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_People.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Finance.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;

        GameObject_Desc.SetActive (false);
        GameObject_Org.SetActive (false);
        GameObject_People.SetActive (false);
        GameObject_Finance.SetActive (true);
    }

}