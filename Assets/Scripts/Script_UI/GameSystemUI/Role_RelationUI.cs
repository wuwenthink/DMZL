// ======================================================================================
// 文 件 名 称：RelationshipUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 21:01:37
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 21:01:37
// ======================================================================================
// 功 能 描 述：UI：交游
// ======================================================================================

using System;
using UnityEngine;
using System.Collections.Generic;

public class Role_RelationUI : MonoBehaviour
{
    public GameObject Button_Now;
    public GameObject Button_Short;

    public GameObject GameObject_Info;
    public Transform GameObject_RolePos;
    public Transform Button_RoleEX;
    
    public GameObject GameObject_RoleIconPos;
    public UIText Label_RoleName;

    public UIText Label_WordName;
    public UIText Label_TitleName;
    public UIText Label_Gender;
    public UIText Label_Age;
    public UIText Lable_EX;
    public UISlider ProgressBar_Grow;
    public UIText Label_Grow;
    public UIText Label_NowState;
    public UIText Label_StateType;
    public Transform GameObject_ComPos;
    public Transform Button_ComEX;

    Dictionary<int, Runtime_Relationship> Dic_Fixed;//固定关系
    Dictionary<int, Runtime_Relationship> Dic_Snap;//临时关系

    bool isFixed = false;
    int NpcID = 0;

    GameObject Role_None;
    GameObject Com_None;
    GameObject Choose_Role;
    void Awake()
    {
        //Dic_Fixed = Charactor.GetInstance.Relationship_Fixed;
        //Dic_Snap = Charactor.GetInstance.Relationship_Snap;
    }

    void Start()
    {
        ClickControl();

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

        UIEventListener.Get(Button_Now).onClick = ChangeToNowPage;
        UIEventListener.Get(Button_Short).onClick = ChangeToSnapPage;
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

    /// <summary>
    /// 界面初始化，列表中生成人物，默认显示第一个的信息
    /// </summary>
    public void Open()
    {
        ChangeToNowPage(Button_Now);
    }

    /// <summary>
    /// 切换到当前界面
    /// </summary>
    /// <param name="btn"></param>
    void ChangeToNowPage(GameObject btn)
    {
        NGUITools.DestroyChildren(GameObject_RolePos);
        isFixed = true;
        if (Dic_Fixed.Count == 0)
        {
            Role_None = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Label_NoneEX, GameObject_RolePos.transform, Vector3.zero, Vector3.one).gameObject;
            GameObject_Info.SetActive(false);
        }
        else
        {
            GameObject_Info.SetActive(true);
            Button_RoleEX.gameObject.SetActive(true);
            int row = 0;//行，Y轴
            int count = 0;
            foreach (var item in Dic_Fixed)
            {
                Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_RoleEX, GameObject_RolePos.transform, new Vector3(0, 0 + row * (-47), 0), Vector3.one);
                go.Find("Sprite_RoleIcon").GetComponent<UISprite>().spriteName = RunTime_Data.RolePool[item.Key].headIcon;
                go.Find("Label_RoleName").GetComponent<UIText>().SetText(false, RunTime_Data.RolePool[item.Key].commonName);
                go.Find("Label_RoleLv").GetComponent<UIText>().SetText(false, Dic_Fixed[item.Key].Relationship.name);
                go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
                go.name = item.Key.ToString();
                UIEventListener.Get(go.gameObject).onClick = SetLeft;
                if (count == 0)
                {
                    SetLeft(go.gameObject);
                }
                row++;
            }
        }

        Button_RoleEX.gameObject.SetActive(false);
    }

    /// <summary>
    /// 切换到临时界面
    /// </summary>
    /// <param name="btn"></param>
    void ChangeToSnapPage(GameObject btn)
    {
        NGUITools.DestroyChildren(GameObject_RolePos);
        isFixed = false;
        if (Dic_Snap.Count == 0)
        {
            Role_None = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Label_NoneEX, GameObject_RolePos.transform, Vector3.zero, Vector3.one).gameObject;
            GameObject_Info.SetActive(false);
        }
        else
        {
            GameObject_Info.SetActive(true);
            Button_RoleEX.gameObject.SetActive(true);
            int row = 0;//行，Y轴
            int count = 0;
            foreach (var item in Dic_Snap)
            {
                Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_RoleEX, GameObject_RolePos.transform, new Vector3(0, 0 + row * (-47), 0), Vector3.one);
                go.Find("Sprite_RoleIcon").GetComponent<UISprite>().spriteName = RunTime_Data.RolePool[item.Key].headIcon;
                go.Find("Label_RoleName").GetComponent<UIText>().SetText(false, RunTime_Data.RolePool[item.Key].commonName);
                go.Find("Label_RoleLv").GetComponent<UIText>().SetText(false, Dic_Snap[item.Key].Relationship.name);
                go.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
                go.name = item.Key.ToString();
                UIEventListener.Get(go.gameObject).onClick = SetLeft;
                if (count == 0)
                {
                    SetLeft(go.gameObject);
                }
                row++;
            }
        }
        Button_RoleEX.gameObject.SetActive(false);
    }

    //给详情赋值
    void SetLeft(GameObject btn)
    {
        if (Choose_Role != null)
        {
            Choose_Role.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        Choose_Role = btn;

        NGUITools.DestroyChildren(GameObject_RoleIconPos.transform);
        NGUITools.DestroyChildren(GameObject_ComPos);
        NpcID = int.Parse(btn.name);
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_RoleIcon, GameObject_RoleIconPos.transform, Vector3.zero, Vector3.one);
        go.Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = RunTime_Data.RolePool[NpcID].headIcon;

        Label_RoleName.SetText(false, RunTime_Data.RolePool[NpcID].commonName);
        Label_Gender.SetText(false, RunTime_Data.RolePool[NpcID].gender);
        Label_Age.SetText(false, RunTime_Data.RolePool[NpcID].birthday.ToString());
        Lable_EX.SetText(false, RunTime_Data.RolePool[NpcID].story);
        string[] id_s;
        if (isFixed)
        {
            SetProgressBar(Dic_Fixed[NpcID].Exp);
            Label_Grow.SetText(false, Dic_Fixed[NpcID].Relationship.name);
            Label_NowState.SetText(false, Dic_Fixed[NpcID].GetStateWord(Dic_Fixed[NpcID].RelationshipState));
            id_s = Dic_Fixed[NpcID].Relationship.exchange.Split(';');
        }
        else
        {
            SetProgressBar(Dic_Snap[NpcID].Exp);
            Label_Grow.SetText(false, Dic_Snap[NpcID].Relationship.name);
            Label_NowState.SetText(false, LanguageMgr.GetInstance.GetText("Relation3"));
            id_s = Dic_Snap[NpcID].Relationship.exchange.Split(';');
        }
        Label_StateType.SetText(false, "临时关系");
        //解锁互动功能

        if (id_s[0] == "0")
        {
            Com_None = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Label_NoneEX, GameObject_ComPos.transform, Vector3.zero, Vector3.one).gameObject;
        }
        else
        {
            Button_ComEX.gameObject.SetActive(true);
            List<int> list_LvID = new List<int>();
            foreach (var item in id_s)
            {
                list_LvID.Add(int.Parse(item));
            }
            int col = 0;
            foreach (var item in list_LvID)
            {
                Transform tf = CommonFunc.GetInstance.UI_Instantiate(Button_ComEX, GameObject_ComPos.transform, new Vector3(col* 70, 0,0), Vector3.one);
                tf.GetComponentInChildren<UIText>().SetText(false, LanguageMgr.GetInstance.GetText("Relation" + (item + 8).ToString()));
                tf.gameObject.name = (item).ToString();
                col++;
            }
        }

        Button_ComEX.gameObject.SetActive(false);
    }

    /// <summary>
    /// 计算进度条的值
    /// </summary>
    /// <param name="exp"></param>
    void SetProgressBar(int exp)
    {
        ProgressBar_Grow.value = (float)(exp + 100f) /2f/ 100f;
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}
