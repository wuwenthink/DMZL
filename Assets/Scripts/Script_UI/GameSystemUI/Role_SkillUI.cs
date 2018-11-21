// ======================================================================================
// 文件名         ：    SkillUI.cs
// 版本号         ：    v1.3.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-28
// 最后修改日期   ：    2017-11-18
// ======================================================================================
// 功能描述       ：    技法UI
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技法UI
/// </summary>
public class Role_SkillUI : MonoBehaviour
{
    // GameObject_MainMenu
    // 类目按钮
    public GameObject Button_MainSkill1;
    public UIText Label_MSName_1;
    public UIText Label_MSNum_1;
    public GameObject Button_MainSkill2;
    public UIText Label_MSName_2;
    public UIText Label_MSNum_2;
    public GameObject Button_MainSkill3;
    public UIText Label_MSName_3;
    public UIText Label_MSNum_3;
    public GameObject Button_MainSkill4;
    public UIText Label_MSName_4;
    public UIText Label_MSNum_4;
    public GameObject Button_MainSkill5;
    public UIText Label_MSName_5;
    public UIText Label_MSNum_5;
    public GameObject Button_MainSkill6;
    public UIText Label_MSName_6;
    public UIText Label_MSNum_6;   

    // 左侧两个按钮
    public GameObject Button_RecipeList;
    
    // 技法滚动框
    public Transform GameObject_SkillPos;

    public GameObject Button_SkillEX;

    // 详细信息--介绍
    public UIText Lable_DescEX;

    // 学习
    public UISlider ProgressBar_Grow;
    public UIText Label_Grow;
    public Transform GameObject_WayPos;
    public GameObject GameObject_Pos_Thing;

    // 开启配方
    public Transform GameObject_Pos_MakeRecipe;
    public GameObject Button_MakeRecipeEX;
    

    private Transform UI_Root;

    private GameObject labelTips;
    
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

        UIEventListener.Get(Button_MainSkill1).onClick = Refresh;
        UIEventListener.Get(Button_MainSkill2).onClick = Refresh;
        UIEventListener.Get(Button_MainSkill3).onClick = Refresh;
        UIEventListener.Get(Button_MainSkill4).onClick = Refresh;
        UIEventListener.Get(Button_MainSkill5).onClick = Refresh;
        UIEventListener.Get(Button_MainSkill6).onClick = Refresh;
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
        Button_MainSkill1.name = "1";
        Button_MainSkill2.name = "2";
        Button_MainSkill3.name = "3";
        Button_MainSkill4.name = "4";
        Button_MainSkill5.name = "5";
        Button_MainSkill6.name = "6";

        Label_MSName_1.SetText(false, SelectDao.GetDao().SelectSkill(1).name);
        Label_MSName_2.SetText(false, SelectDao.GetDao().SelectSkill(2).name);
        Label_MSName_3.SetText(false, SelectDao.GetDao().SelectSkill(3).name);
        Label_MSName_4.SetText(false, SelectDao.GetDao().SelectSkill(4).name);
        Label_MSName_5.SetText(false, SelectDao.GetDao().SelectSkill(5).name);
        Label_MSName_6.SetText(false, SelectDao.GetDao().SelectSkill(6).name);

        var charactor = Charactor.GetInstance;
        //进度
        //List<int> know_all_1 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(1)) { know_all_1.Add(item.id); }
        //List<int> know_have_1 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 1) know_have_1.Add(item.id); }

        //List<int> know_all_2 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(2)) { know_all_2.Add(item.id); }
        //List<int> know_have_2 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 2) know_have_2.Add(item.id); }

        //List<int> know_all_3 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(3)) { know_all_3.Add(item.id); }
        //List<int> know_have_3 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 3) know_have_3.Add(item.id); }

        //List<int> know_all_4 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(4)) { know_all_4.Add(item.id); }
        //List<int> know_have_4 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 4) know_have_4.Add(item.id); }

        //List<int> know_all_5 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(5)) { know_all_5.Add(item.id); }
        //List<int> know_have_5 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 5) know_have_5.Add(item.id); }

        //List<int> know_all_6 = new List<int>();//该分类全部技能数量
        //foreach (var item in SelectDao.GetDao().SelectSkillByClass(6)) { know_all_6.Add(item.id); }
        //List<int> know_have_6 = new List<int>();//该分类已拥有技能数量
        //foreach (var item in charactor.SkillDic.Values) { if (item.aheadClass == 6) know_have_6.Add(item.id); }


        //Label_MSNum_1.SetText(false, know_have_1.Count+" / "+ know_all_1.Count);
        //Label_MSNum_2.SetText(false, know_have_2.Count + " / " + know_all_2.Count);
        //Label_MSNum_3.SetText(false, know_have_3.Count + " / " + know_all_3.Count);
        //Label_MSNum_4.SetText(false, know_have_4.Count + " / " + know_all_4.Count);
        //Label_MSNum_5.SetText(false, know_have_5.Count + " / " + know_all_5.Count);
        //Label_MSNum_6.SetText(false, know_have_6.Count + " / " + know_all_6.Count);

        Refresh(Button_MainSkill1);
    }


    private void Refresh(GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill1, true);
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill2, true);
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill3, true);
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill4, true);
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill5, true);
        CommonFunc.GetInstance.SetButtonState(Button_MainSkill6, true);
        CommonFunc.GetInstance.SetButtonState(btn, false);


        int ahead = 0;
        char[] ch = btn.name.ToCharArray();
        if (btn.name.StartsWith("Button"))
            ahead = int.Parse(ch[ch.Length - 1].ToString());
        else
            ahead = int.Parse(ch[0].ToString());

        //var skillStore = Charactor.GetInstance.SkillDic;

        Button_SkillEX.SetActive(true);


        //int num = -1;
        //foreach (var skill in skillStore.Values)
        //{
        //    if (skill.type != SkillType.标准技法 && skill.aheadClass == ahead)
        //    {
        //        GameObject clone = CommonFunc.GetInstance.UI_Instantiate(Button_SkillEX.transform, GameObject_SkillPos.transform, new Vector3(0, -60 * ++num), Vector3.one).gameObject;
        //        clone.name = skill.id.ToString();

        //        clone.transform.Find("Label_SkillName").GetComponent<UILabel>().text = skill.name;
        //        //clone.transform.Find("Label_SkillLv").GetComponent<UILabel>().text = Constants.ProficiencyLevel[Charactor.GetInstance.GetLv_Skill(false, skill.id)];

        //        UIEventListener.Get(clone).onClick = SkillInfo;
        //    }
        //}
        Button_SkillEX.SetActive(false);

        SkillInfo(Button_MainSkill1);
    }

    void SkillInfo(GameObject btn)
    {
        //int id = int.Parse(btn.name);
        //var skillStore = Charactor.GetInstance.SkillDic;
        //var charactor = Charactor.GetInstance;

        //Button_MakeRecipeEX.SetActive(true);

        //int level = charactor.GetLv_Skill(false, id);

        // 介绍

        //Lable_DescEX.SetText(false, SelectDao.GetDao().SelectSkill(id).des);

        //// 学习

        //ProgressBar_Grow.value = (float)skillStore[id].Exp / skillStore[id].grow.growData[9].exp;
        ////Label_Grow.SetText(false, Constants.ProficiencyLevel[level]);

        //int num = 0;
        //foreach (string str in skillStore[id].growWay)
        //{
        //    GameObject clone = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_WayEX, GameObject_WayPos.transform, new Vector3(50 * (num - 1), 0, 0), Vector3.one).gameObject;
        //    clone.name = "way" + "_" + id + "_" + ++num;            

        //    clone.transform.Find("Label_Way").GetComponent<UILabel>().text = num.ToString();

        //    UIEventListener.Get(clone).onHover = OpenLabelTips;
        //}

        //Dictionary<int, int> propDic = charactor.CalculatePropertyIncrement(false, id);
        //if (propDic != null && propDic.Count > 0)
        //{
        //    num = -1;
        //    foreach (int key in propDic.Keys)
        //    {
        //        Transform clone = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_PropEX, GameObject_Pos_Thing.transform, new Vector3(-105 + ++num * 50, 0, 0), Vector3.one);

        //        clone.name = "prop_" + key;

        //        //clone.Find("Label_PropName").GetComponent<UILabel>().text = Constants.RoleProp[(RoleInfo)key];

        //        // TOUPDATE 应该改成文字显示
        //        clone.Find("Label_PropAdd").GetComponent<UIText>().SetText(false, propDic[key].ToString());
        //    }
        //}
        //else
        //{
        //    Transform none = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Label_NoneEX, GameObject_Pos_Thing.transform, new Vector3(0, -10, 0), Vector3.one);
        //    none.name = "prop";
        //}

        //// 开启配方

        //num = -1;
        //foreach (int lv in skillStore[id].grow.growData.Keys)
        //{
        //    if (lv > level)
        //        break;
        //    if (skillStore[id].grow.growData[lv].produce != null)
        //    {
        //        foreach (int produceId in skillStore[id].grow.growData[lv].produce)
        //        {
        //            GameObject clone = CommonFunc.GetInstance.UI_Instantiate(Button_MakeRecipeEX.transform, GameObject_Pos_MakeRecipe.transform, new Vector3(40 * ++num, 0, 0), Vector3.one).gameObject;
        //            clone.name = produceId.ToString();                   
        //            clone.GetComponentInChildren<UILabel>().text = SelectDao.GetDao().SelectProduce(produceId).name;

        //            UIEventListener.Get(clone).onClick = ClickProduce;
        //        }
        //    }
        //}
        Button_MakeRecipeEX.SetActive(false);
    }

    private void OpenRecipe (GameObject btn)
    {
        GameObject produce = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Making, UI_Root, Vector3.one, Vector3.zero).gameObject;
        produce.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        produce.GetComponent<MakingUI> ().SetInfo(int.Parse(btn.name));
    }




    private void ClickProduce (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform produce = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Making, UI_Root, Vector3.zero, Vector3.one);
        produce.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        produce.GetComponent<MakingUI> ().SetInfo (id);
    }

    private void OpenLabelTips (GameObject btn, bool isOver)
    {
        if (isOver)
        {
            labelTips.gameObject.SetActive (true);
            string [] reg = btn.name.Split ('_');
            int id = int.Parse (reg [1]);
            int count = int.Parse (reg [2]) - 1;
            //labelTips.GetComponent<LableTipsUI> ().SetAll (true, Charactor.GetInstance.SkillDic [id].growWay [count]);
        }
        else
        {
            labelTips.gameObject.SetActive (false);
        }
    }

}