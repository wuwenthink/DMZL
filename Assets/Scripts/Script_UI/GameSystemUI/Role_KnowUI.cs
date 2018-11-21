// ======================================================================================
// 文件名         ：    KnowledgeUI.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-24
// 最后修改日期   ：    2017-11-18
// ======================================================================================
// 功能描述       ：    学识UI
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 学识UI
/// </summary>
public class Role_KnowUI : MonoBehaviour
{
    public GameObject Button_Title_1;
    public GameObject Button_Title_2;
    public GameObject Button_Title_3;
    public GameObject Button_Title_4;
    public GameObject Button_Title_5;
    public GameObject Botton_Identity1;
    public GameObject Botton_Identity2;
    public GameObject Botton_Identity3;
    public GameObject Botton_Identity4;
    public GameObject Botton_Identity5;

    public GameObject GameObject_BookPos;
    public GameObject Button_BookEX;
    public UISlider ProgressBar_Grow_Read;
    public UIText Label_Grow_Read;
    public GameObject Sprite_BookIcon;
    public UIText Label_BookName_C;
    public UIText Label_BookLv;
    public GameObject GameObject_WayPos_Read;

    public UISlider ProgressBar_Grow_Study;
    public UIText Label_Grow_Study;
    public GameObject GameObject_WayPos_Study;
    public GameObject GameObject_Pos_Thing;
    public GameObject GameObject_DoPos;
    public GameObject GameObject_DoEX;


    private GameObject labelTips;
    int chooseKnow_Book = 0;
    int chooseKnow_Walk = 0;

    private void Awake ()
    {

    }

    void Start()
    {
        ClickControl();
        CommonFunc.GetInstance.Ins_ItemTips(labelTips);
        CommonFunc.GetInstance.SetUIPanel(gameObject);
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
        Destroy(labelTips);       
    }

    public void Open()
    {
        //var KnowLedge = Charactor.GetInstance.KnowLedgeDic;
        //var Iden = Charactor.GetInstance.Dic_Idens;

        //Button_Title_1.name = "1";
        //Button_Title_2.name = "2";
        //Button_Title_3.name = "3";
        //Button_Title_4.name = "4";
        //Button_Title_5.name = "5";

        //Button_Title_1.transform.Find("Label_Title").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectKnowledge(1).name);
        //Button_Title_2.transform.Find("Label_Title").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectKnowledge(2).name);
        //Button_Title_3.transform.Find("Label_Title").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectKnowledge(3).name);
        //Button_Title_4.transform.Find("Label_Title").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectKnowledge(4).name);
        //Button_Title_5.transform.Find("Label_Title").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectKnowledge(5).name);

        //UIEventListener.Get(Button_Title_1).onClick = Refresh;
        //UIEventListener.Get(Button_Title_2).onClick = Refresh;
        //UIEventListener.Get(Button_Title_3).onClick = Refresh;
        //UIEventListener.Get(Button_Title_4).onClick = Refresh;
        //UIEventListener.Get(Button_Title_5).onClick = Refresh;

        ////判断是否有对应的称号身份
        //foreach (var item_know in KnowLedge[1].titleList)
        //{
        //    foreach (var item_iden in Iden[4])
        //    {
        //        if (item_iden.Key == item_know)
        //        {
        //            Botton_Identity1.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item_iden.Value.identityName);
        //            Botton_Identity1.name = item_iden.Key.ToString();
        //            UIEventListener.Get(Botton_Identity1).onClick = Refresh;
        //        }
        //        else
        //        {
        //            Botton_Identity1.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //        }
        //    }
        //}
        //foreach (var item_know in KnowLedge[2].titleList)
        //{
        //    foreach (var item_iden in Iden[4])
        //    {
        //        if (item_iden.Key == item_know)
        //        {
        //            Botton_Identity2.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item_iden.Value.identityName);
        //            Botton_Identity2.name = item_iden.Key.ToString();
        //            UIEventListener.Get(Botton_Identity2).onClick = Refresh;
        //        }
        //        else
        //        {
        //            Botton_Identity2.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //        }
        //    }
        //}
        //foreach (var item_know in KnowLedge[3].titleList)
        //{
        //    foreach (var item_iden in Iden[4])
        //    {
        //        if (item_iden.Key == item_know)
        //        {
        //            Botton_Identity3.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item_iden.Value.identityName);
        //            Botton_Identity3.name = item_iden.Key.ToString();
        //            UIEventListener.Get(Botton_Identity3).onClick = Refresh;
        //        }
        //        else
        //        {
        //            Botton_Identity3.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //        }
        //    }
        //}
        //foreach (var item_know in KnowLedge[4].titleList)
        //{
        //    foreach (var item_iden in Iden[4])
        //    {
        //        if (item_iden.Key == item_know)
        //        {
        //            Botton_Identity4.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item_iden.Value.identityName);
        //            Botton_Identity4.name = item_iden.Key.ToString();
        //            UIEventListener.Get(Botton_Identity4).onClick = Refresh;
        //        }
        //        else
        //        {
        //            Botton_Identity4.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //        }
        //    }
        //}
        //foreach (var item_know in KnowLedge[5].titleList)
        //{
        //    foreach (var item_iden in Iden[4])
        //    {
        //        if (item_iden.Key == item_know)
        //        {
        //            Botton_Identity5.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, item_iden.Value.identityName);
        //            Botton_Identity5.name = item_iden.Key.ToString();
        //            UIEventListener.Get(Botton_Identity5).onClick = Refresh;
        //        }
        //        else
        //        {
        //            Botton_Identity5.transform.Find("Label_Name").GetComponent<UIText>().SetText(true, "Nomal_10");
        //        }
        //    }
        //}

        //Refresh(Button_Title_1);
    }

    //弹出身份信息界面
    void IdenInfo(GameObject btn)
    {
        int id = int.Parse(btn.name);
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_IdentityInfo, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<IdentityInfoUI>().SetInfo(id);
    }

    private void Refresh (GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Button_Title_1, true);
        CommonFunc.GetInstance.SetButtonState(Button_Title_2, true);
        CommonFunc.GetInstance.SetButtonState(Button_Title_3, true);
        CommonFunc.GetInstance.SetButtonState(Button_Title_4, true);
        CommonFunc.GetInstance.SetButtonState(Button_Title_5, true);
        CommonFunc.GetInstance.SetButtonState(btn, false);

        //var knowlegeStore = Charactor.GetInstance.KnowLedgeDic;
        //var charactor = Charactor.GetInstance;

        //NGUITools.DestroyChildren(GameObject_BookPos.transform);
        //NGUITools.DestroyChildren(GameObject_WayPos_Study.transform);
        //NGUITools.DestroyChildren(GameObject_Pos_Thing.transform);
        //Button_BookEX.SetActive(true);
        //GameObject_DoEX.SetActive(true);

        //// 获取书的类型
        //int class_s = int.Parse(btn.name);
        //KnowledgeType class_k = (KnowledgeType)class_s;

        ////读万卷书
        //List<int> know_all = new List<int>();//全部该分类学识书籍
        //foreach (var item in SelectDao.GetDao().SelectKnowByClass(class_s))
        //{
        //    if (item.aheadType == (KnowledgeType)class_s)
        //    {
        //        know_all.Add(item.id);
        //    }
        //}
        //List<int> know_have = new List<int>();//已拥有学识书籍
        //int num_book = -1;
        //// 获取已学过的该类型的书
        //foreach (var know in knowlegeStore.Values)
        //{
        //    if (know.aheadType == class_k && know.knowledgeClass == KnowledgeClass.每类书籍)
        //    {
        //        GameObject book = CommonFunc.GetInstance.UI_Instantiate(Button_BookEX.transform, GameObject_BookPos.transform, new Vector3(++num_book % 4 * 140, -num_book / 4 * 120, 0), Vector3.one).gameObject;
        //        book.name = know.id.ToString();

        //        book.transform.Find("Label_BookName").GetComponent<UIText>().SetText(false, know.name);
        //        //string level = Constants.ProficiencyLevel[Charactor.GetInstance.GetLv_Skill(true, know.id)];

        //        UIEventListener.Get(book).onClick = BookInfo;
        //        know_have.Add(know.id);
        //    }
        //}

        //ProgressBar_Grow_Read.value = (float)know_have.Count / know_all.Count;
        //Label_Grow_Read.SetText(false, know_have.Count + " / "+ know_all.Count);
        //Button_BookEX.SetActive(false);

        //// 行千里路

        //// 阶段
        //ProgressBar_Grow_Study.value = (float) knowlegeStore [class_s + 10].Exp / knowlegeStore [class_s + 10].grow.growData [9].exp;
        ////Label_Grow_Study.SetText(false, Constants.ProficiencyLevel [charactor.GetLv_Skill(true, class_s + 10)]);

        //// 途径
        //chooseKnow_Walk = class_s + 10;
        //int num_way_walk = 0;
        //foreach (string str in knowlegeStore [class_s + 10].growWay)
        //{
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_WayEX, GameObject_WayPos_Read.transform, new Vector3(45 * num_way_walk, 0, 0), Vector3.one).gameObject;
        //    go.transform.GetComponentInChildren<UIText>().SetText(false, (num_way_walk + 1).ToString());
        //    go.name = num_way_walk.ToString();
        //    UIEventListener.Get (go).onHover = OpenLabelTips_Walk;
        //}

        //// 成长
        //Dictionary<int, int> propDic = charactor.CalculatePropertyIncrement (true, class_s + 10);
        //if (propDic != null && propDic.Count > 0)
        //{
        //    int num_prop_walk = -1;
        //    foreach (int key in propDic.Keys)
        //    {
        //        Transform clone = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_PropEX, GameObject_Pos_Thing.transform, Vector3.one, new Vector3 (-105 + ++num_prop_walk * 50, 0, 0));

        //        //clone.Find ("Label_PropName").GetComponent<UILabel> ().text = Constants.RoleProp [(RoleInfo) key];

        //        // TOUPDATE 应该改成文字显示
        //        clone.Find ("Label_PropAdd").GetComponent<UILabel> ().text = propDic [key].ToString ();
        //    }
        //}
        //else
        //{
        //    Transform none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Thing.transform, Vector3.zero, Vector3.one);
        //}

        //历练的记录

        GameObject_DoEX.SetActive (false);
    }

    void BookInfo(GameObject btn)
    {
        Knowledge know = SelectDao.GetDao().SelectKnowledge(int.Parse(btn.name));
        Label_BookName_C.SetText(false, know.name);
        //Label_BookLv.SetText(false, Constants.ProficiencyLevel[Charactor.GetInstance.GetLv_Skill(true, know.id)]);
        chooseKnow_Book = know.id;
        Sprite_BookIcon.GetComponent<UISprite>().spriteName = Constants.Items_All[know.itemId].icon;
        int count = 0;
        foreach (var item in know.growWay)
        {
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_WayEX, GameObject_WayPos_Read.transform, new Vector3(45*count,0,0), Vector3.one).gameObject;
            go.transform.GetComponentInChildren<UIText>().SetText(false,(count + 1).ToString());
            go.name = count.ToString();
            UIEventListener.Get(go).onHover = OpenLabelTips_Book;
        }
    }

    private void OpenLabelTips_Book (GameObject btn, bool isOver)
    {
        Knowledge know = SelectDao.GetDao().SelectKnowledge(chooseKnow_Book);
        if (isOver)
        {
            labelTips.GetComponent<LableTipsUI> ().SetAll (true, "System_1", know.growWay[int.Parse(btn.name)]);
        }
        else
        {
            labelTips.SetActive (false);
        }
    }

    private void OpenLabelTips_Walk(GameObject btn, bool isOver)
    {
        Knowledge know = SelectDao.GetDao().SelectKnowledge(chooseKnow_Walk);
        if (isOver)
        {
            labelTips.GetComponent<LableTipsUI>().SetAll(true, "System_1", know.growWay[int.Parse(btn.name)]);
        }
        else
        {
            labelTips.SetActive(false);
        }
    }


}