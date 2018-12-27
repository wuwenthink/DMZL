// ======================================================================================
// 文 件 名 称：Role_IdentityUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-12-12 21:30:33
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-12-12 21:30:33
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_IdentityUI : MonoBehaviour {
    public GameObject Button_Iden_1;
    public GameObject Button_Iden_2;
    public GameObject Button_Iden_3;
    public GameObject Button_Iden_4;
    public GameObject Button_Iden_5;
    public GameObject Button_Iden_6;
    public GameObject GameObject_Pos_Iden1;
    public GameObject GameObject_Pos_Iden2;
    public GameObject GameObject_Pos_Iden3;
    public GameObject GameObject_Pos_Iden4;
    public GameObject GameObject_Pos_Iden5;
    public GameObject GameObject_Pos_Iden6;

    public UIText Lable_Name;
    public UIText Lable_DesEX;
    public GameObject Button_Type;
    public GameObject Button_Clothes;
    public UIText Label_State;
    public GameObject Button_hide;
    public GameObject Button_More;
    public GameObject GameObject_Pos_Thing;

    GameObject chooseIden;
    Identity chooseIden_One;
    GameObject ItemTip;
    void Start()
    {
        ClickControl();
        CommonFunc.GetInstance.SetUIPanel(gameObject);
        CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
    }

    /// <summary>
    /// 道具弹窗
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isHover"></param>
    void ShowTips(GameObject btn)
    {
        int itemID = SelectDao.GetDao().SelectBusiness_Goods(int.Parse(btn.name)).itemId;
        ItemTip.SetActive(true);
        ItemTip.GetComponent<ItemTipsUI>().itemTipsSet(itemID, false);
    }

    public GameObject RoleUI;
    public GameObject Button_Close;
    public GameObject Button_Role;
    public GameObject Button_Identity;
    public GameObject Button_Know;
    public GameObject Button_Skill;
    public GameObject Button_Relationship;
    public GameObject Window_Role;
    public GameObject Window_Identity;
    public GameObject Window_Know;
    public GameObject Window_Skill;
    public GameObject Window_Relationship;

    private void ClickControl()
    {
        UIEventListener.Get(Button_Close).onClick = back;

        UIEventListener.Get(Button_Role).onClick = Jump_Role;
        UIEventListener.Get(Button_Identity).onClick = Jump_Iden;
        UIEventListener.Get(Button_Know).onClick = Jump_Know;
        UIEventListener.Get(Button_Skill).onClick = Jump_Skill;
        UIEventListener.Get(Button_Relationship).onClick = Jump_Relation;

        UIEventListener.Get(Button_Iden_1).onClick = IdenAll;
        UIEventListener.Get(Button_Iden_2).onClick = StopTip;
        UIEventListener.Get(Button_Iden_3).onClick = StopTip;
        UIEventListener.Get(Button_Iden_4).onClick = StopTip;
        UIEventListener.Get(Button_Iden_5).onClick = StopTip;
        UIEventListener.Get(Button_Iden_6).onClick = StopTip;
    }

    //打开全部职业界面
    void IdenAll(GameObject btn)
    {
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_IdentityAll, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
    }

    //暂未开放提示
    void StopTip(GameObject btn)
    {
        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).GetComponent<LableTipsUI>().SetAll(true, "System_1", "暂未开放");
    }

    void Jump_Role(GameObject btn)
    {
        FindObjectOfType<Role_MainUI>().Open();
        CommonFunc.GetInstance.SetButtonState(Button_Role, false);
        CommonFunc.GetInstance.SetButtonState(Button_Identity, true);
        CommonFunc.GetInstance.SetButtonState(Button_Know, true);
        CommonFunc.GetInstance.SetButtonState(Button_Skill, true);
        CommonFunc.GetInstance.SetButtonState(Button_Relationship, true);
        Window_Role.SetActive(true);
        Window_Identity.SetActive(false);
        Window_Know.SetActive(false);
        Window_Skill.SetActive(false);
        Window_Relationship.SetActive(false);
    }
    void Jump_Iden(GameObject btn)
    {
        FindObjectOfType<Role_IdentityUI>().Open();
        CommonFunc.GetInstance.SetButtonState(Button_Role, true);
        CommonFunc.GetInstance.SetButtonState(Button_Identity, false);
        CommonFunc.GetInstance.SetButtonState(Button_Know, true);
        CommonFunc.GetInstance.SetButtonState(Button_Skill, true);
        CommonFunc.GetInstance.SetButtonState(Button_Relationship, true);
        Window_Role.SetActive(false);
        Window_Identity.SetActive(true);
        Window_Know.SetActive(false);
        Window_Skill.SetActive(false);
        Window_Relationship.SetActive(false);
    }
    void Jump_Know(GameObject btn)
    {
        FindObjectOfType<Role_KnowUI>().Open();
        CommonFunc.GetInstance.SetButtonState(Button_Role, true);
        CommonFunc.GetInstance.SetButtonState(Button_Identity, true);
        CommonFunc.GetInstance.SetButtonState(Button_Know, false);
        CommonFunc.GetInstance.SetButtonState(Button_Skill, true);
        CommonFunc.GetInstance.SetButtonState(Button_Relationship, true);
        Window_Role.SetActive(false);
        Window_Identity.SetActive(false);
        Window_Know.SetActive(true);
        Window_Skill.SetActive(false);
        Window_Relationship.SetActive(false);
    }
    void Jump_Skill(GameObject btn)
    {
        FindObjectOfType<Role_SkillUI>().Open();
        CommonFunc.GetInstance.SetButtonState(Button_Role, true);
        CommonFunc.GetInstance.SetButtonState(Button_Identity, true);
        CommonFunc.GetInstance.SetButtonState(Button_Know, true);
        CommonFunc.GetInstance.SetButtonState(Button_Skill, false);
        CommonFunc.GetInstance.SetButtonState(Button_Relationship, true);
        Window_Role.SetActive(false);
        Window_Identity.SetActive(false);
        Window_Know.SetActive(false);
        Window_Skill.SetActive(true);
        Window_Relationship.SetActive(false);
    }
    void Jump_Relation(GameObject btn)
    {
        FindObjectOfType<Role_RelationUI>().Open();
        CommonFunc.GetInstance.SetButtonState(Button_Role, true);
        CommonFunc.GetInstance.SetButtonState(Button_Identity, true);
        CommonFunc.GetInstance.SetButtonState(Button_Know, true);
        CommonFunc.GetInstance.SetButtonState(Button_Skill, true);
        CommonFunc.GetInstance.SetButtonState(Button_Relationship, false);
        Window_Role.SetActive(false);
        Window_Identity.SetActive(false);
        Window_Know.SetActive(false);
        Window_Skill.SetActive(false);
        Window_Relationship.SetActive(true);
    }

    public void back(GameObject btn)
    {
        Destroy(RoleUI);
    }

    public void Open()
    {
        //int count_1 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[0])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden1.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_1++;
        //}
        //int count_2 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[1])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden2.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_2++;
        //}
        //int count_3 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[2])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden3.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_3++;
        //}
        //int count_4 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[3])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden4.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_4++;
        //}
        //int count_5 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[4])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden5.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_5++;
        //}
        //int count_6 = 0;
        //foreach (var item in Charactor.GetInstance.Dic_Idens[5])
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdentityEX, GameObject_Pos_Iden6.transform, new Vector3(0, count_1 * -43, 0), Vector3.one).gameObject;
        //    go.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.identityName);
        //    go.name = item.Key.ToString();
        //    UIEventListener.Get(go).onClick = ClickAnIdentity;
        //    count_6++;
        //}

    }

    /// <summary>
    /// 点击一个身份
    /// </summary>
    /// <param name="btn"></param>
    private void ClickAnIdentity(GameObject btn)
    {
        NGUITools.DestroyChildren(GameObject_Pos_Thing.transform);

        if (chooseIden != null)
        {
            CommonFunc.GetInstance.SetButtonState(chooseIden, true);
        }
        CommonFunc.GetInstance.SetButtonState(btn, false);
        chooseIden = btn;

        int id = int.Parse(btn.name);
        //Identity iden = SelectDao.GetDao().SelectIdentity(id);

        //Lable_Name.SetText(false, iden.identityName);
        //Lable_DesEX.SetText(false, iden.des);
        //Button_Type.transform.Find("Label_Type").GetComponent<UIText>().SetText(true, "Role_48");
        //Button_Type.name = btn.name;
        //UIEventListener.Get(Button_Type).onClick = OpenIdentityDetails;
        //if (iden.cloth_Chang != 0)
        //{
        //    Button_Clothes.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, Constants.Items_All[iden.cloth_Chang].name);
        //    Button_Clothes.name = iden.cloth_Chang.ToString();
        //    UIEventListener.Get(Button_Clothes).onClick = ShowTips;
        //}
        //else
        //{
        //    Button_Clothes.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //}


        Button_hide.name = btn.name;
        UIEventListener.Get(Button_hide).onClick = HideIdentity;

        Button_More.name = btn.name;
        UIEventListener.Get(Button_More).onClick = OpenIdentityDetails;

        //foreach (var item in Charactor.GetInstance.Dic_Idens.Values)
        //{
        //    if (item.ContainsKey(id))
        //    {
        //        chooseIden_One = item[id];
        //        if (chooseIden_One.idenState == IdentityState.hidden)
        //        {
        //            Label_State.SetText(true, "Role_42");
        //        }
        //        else
        //        {
        //            Label_State.SetText(true, "Role_43");
        //        }
        //    }
        //}

        //if (iden.function != 0)
        //{
        //    foreach (var item in SelectDao.GetDao().SelectIndexMenu_JobByJob(iden.function))
        //    {
        //        int count = 0;
        //        if (item.lv == 1)
        //        {
        //            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_IdenThingEX, GameObject_Pos_Thing.transform, new Vector3(40 * count, 0, 0), Vector3.one).gameObject;
        //            go.transform.Find("Label_ThingName").GetComponent<UIText>().SetText(false, Constants.Items_All[iden.cloth_Chang].name);
        //            count++;
        //        }
        //    }
        //}
        //else
        //{
        //    CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Thing.transform, Vector3.zero, Vector3.one);
        //}



    }

    /// <summary>
    /// 显示隐藏身份
    /// </summary>
    /// <param name="btn"></param>
    private void HideIdentity(GameObject btn)
    {
        int id = int.Parse(Button_More.name);
        //foreach (var item in Charactor.GetInstance.Dic_Idens.Values)
        //{
        //    if (item.ContainsKey(id))
        //    {
        //        chooseIden_One = item[id];
        //        if (chooseIden_One.idenState == IdentityState.hidden)
        //        {
        //            item[id].idenState = IdentityState.show;
        //            Label_State.SetText(true, "Role_43");
        //        }
        //        else
        //        {
        //            item[id].idenState = IdentityState.hidden;
        //            Label_State.SetText(true, "Role_42");
        //        }
        //    }
        //}
       
    }

    /// <summary>
    /// 弹出身份详情
    /// </summary>
    /// <param name="btn"></param>
    private void OpenIdentityDetails(GameObject btn)
    {
        int id = int.Parse(btn.name);
        Transform iden_UI = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_IdentityInfo, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        iden_UI.GetComponent<IdentityInfoUI>().SetInfo(id);
        iden_UI.GetComponent<UIPanel>().depth = gameObject.GetComponent<UIPanel>().depth + 10;
    }


}
