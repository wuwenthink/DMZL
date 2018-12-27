// ======================================================================================
// 文件名         ：    CharactorUI.cs
// 版本号         ：    v1.0.2
// 作者           ：    wuwenthink
// 创建日期       ：
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    角色信息UI
// ======================================================================================

using UnityEngine;

/// <summary>
/// 角色信息UI
/// </summary>
public class Role_MainUI : MonoBehaviour
{
    public GameObject GameObject_RolePicPos;
    public UIText Label_Name;

    public GameObject GameObject_Age;
    public GameObject GameObject_Place;
    public GameObject GameObject_Nature;
    public GameObject GameObject_Gender;
    public GameObject GameObject_TitleName;
    public GameObject GameObject_WordName;
    public GameObject GameObject_Force;
    public GameObject GameObject_Fame;
    public GameObject GameObject_Merit;
    public GameObject GameObject_GoodOrEvil;
    public GameObject GameObject_Lucky;
    public GameObject GameObject_Charm;

    public GameObject Botton_Story;
    public GameObject Botton_Prop;
    public GameObject Botton_Equip;
    public GameObject Botton_Baldric;
    public GameObject GameObject_Story;
    public GameObject GameObject_Prop;
    public GameObject GameObject_Equip;
    public GameObject GameObject_Baldric;

    public GameObject Label_Famous;
    public GameObject Lable_StoryEX;

    public GameObject GameObject_TiZhi;
    public GameObject GameObject_XingDong;
    public GameObject GameObject_WuLi;
    public GameObject GameObject_NaiLi;
    public GameObject GameObject_PoLi;
    public GameObject GameObject_YiLi;
    public GameObject GameObject_MouLue;
    public GameObject GameObject_YingBian;
    public GameObject GameObject_FuZhong;
    public GameObject GameObject_XinQing;
    public GameObject GameObject_JianKang;

    public GameObject Button_Weapon;
    public GameObject Button_Armor;

    public GameObject Button_Part1;
    public GameObject Button_Part2;
    public GameObject Button_Part3;
    
    
    void Awake()
    {

    }

    void Start ()
    {
        ClickControl ();

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

    private void ClickControl ()
    {
        UIEventListener.Get (Button_Close).onClick = back;

        UIEventListener.Get(Button_Role).onClick = Jump_Role;
        UIEventListener.Get(Button_Identity).onClick = Jump_Iden;
        UIEventListener.Get(Button_Know).onClick = Jump_Know;
        UIEventListener.Get(Button_Skill).onClick = Jump_Skill;
        UIEventListener.Get(Button_Relationship).onClick = Jump_Relation;

        UIEventListener.Get(Botton_Story).onClick = Jump_Role_Story;
        UIEventListener.Get(Botton_Prop).onClick = Jump_Role_Prop;
        UIEventListener.Get(Botton_Equip).onClick = Jump_Role_Equip;
        UIEventListener.Get(Botton_Baldric).onClick = Jump_Role_Baldric;
    }
    /*__________________界面内分页__________________________*/
    void Jump_Role_Story(GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Botton_Story, false);
        CommonFunc.GetInstance.SetButtonState(Botton_Prop, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Equip, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Baldric, true);
        GameObject_Story.SetActive(true);
        GameObject_Prop.SetActive(false);
        GameObject_Equip.SetActive(false);
        GameObject_Baldric.SetActive(false);
    }
    void Jump_Role_Prop(GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Botton_Story, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Prop, false);
        CommonFunc.GetInstance.SetButtonState(Botton_Equip, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Baldric, true);
        GameObject_Story.SetActive(false);
        GameObject_Prop.SetActive(true);
        GameObject_Equip.SetActive(false);
        GameObject_Baldric.SetActive(false);
    }
    void Jump_Role_Equip(GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Botton_Story, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Prop, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Equip, false);
        CommonFunc.GetInstance.SetButtonState(Botton_Baldric, true);
        GameObject_Story.SetActive(false);
        GameObject_Prop.SetActive(false);
        GameObject_Equip.SetActive(true);
        GameObject_Baldric.SetActive(false);
    }
    void Jump_Role_Baldric(GameObject btn)
    {
        CommonFunc.GetInstance.SetButtonState(Botton_Story, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Prop, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Equip, true);
        CommonFunc.GetInstance.SetButtonState(Botton_Baldric, false);
        GameObject_Story.SetActive(false);
        GameObject_Prop.SetActive(false);
        GameObject_Equip.SetActive(false);
        GameObject_Baldric.SetActive(true);
    }
    /*__________________界面外分页__________________________*/

    void Jump_Role(GameObject btn) {
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



    /// <summary>
    /// 显示角色信息
    /// </summary>
    public void Open ()
    {
        Charactor Charactor = Charactor.GetInstance;

        // 显示名字和人物图
        Label_Name.SetText(false, Charactor.commonName);



        GameObject_Age.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, (1590 - Charactor.birthday).ToString());
        GameObject_Place.transform.Find("Label_Info").GetComponent<UIText>().SetText(false,  Charactor.place);
        //GameObject_Nature.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelecRole_Nature(Charactor.na).name);
        GameObject_Gender.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.gender);
        //GameObject_Force.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelectForce(Charactor.g).name);
        GameObject_Fame.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.shengyu.ToString());
        GameObject_Merit.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.mingwang.ToString());
        //GameObject_GoodOrEvil.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.GoodOrEvil.ToString());
        //GameObject_Lucky.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.Lucky.ToString());
        //GameObject_Charm.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.Charm.ToString());

        if (Charactor.famous==1)
        {
            Label_Famous.SetActive(true);
        }
        else
        {
            Label_Famous.SetActive(false);
        }

        Lable_StoryEX.GetComponent<UIText>().SetText(false, Charactor.story);

        GameObject_TiZhi.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.tili.ToString());
        GameObject_WuLi.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.wuli.ToString());
        GameObject_NaiLi.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.def.ToString());
        GameObject_PoLi.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.poli.ToString());
        GameObject_YiLi.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.yili.ToString());
        //GameObject_MouLue.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.MouLue.ToString());
        GameObject_FuZhong.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.hunger.ToString());
        GameObject_XinQing.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.health.ToString());
        GameObject_JianKang.transform.Find("Label_Info").GetComponent<UIText>().SetText(false, Charactor.mood.ToString());

        //Button_Weapon
        //Button_Armor

        //Button_Part1
        //Button_Part2
        //Button_Part3


}

}