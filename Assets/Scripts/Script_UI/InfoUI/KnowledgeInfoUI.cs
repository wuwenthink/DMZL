// ======================================================================================
// 文件名         ：    KnowledgeInfoUI.cs
// 版本号         ：    v1.1.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-25
// 最后修改日期   ：    2017-11-18
// ======================================================================================
// 功能描述       ：    学识信息UI
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 学识信息UI
/// </summary>
public class KnowledgeInfoUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public GameObject GameObject_MainClass;
    public GameObject GameObject_Book;

    public UILabel Label_Name;

    public UILabel Label_Writer;

    public UILabel Lable_DescEX;

    public GameObject Button_Now;
    public Transform GameObject_AllPos;
    public GameObject Button_AllEX;

    public UISprite Sprite_BookIcon;
    public UILabel Label_Name_Book;

    public UILabel Label_Writer_book;
    public UILabel Lable_DescEX_book;

    public UISlider ProgressBar_Grow;
    public UILabel Label_Grow;
    public GameObject Button_WayEX;
    public Transform GameObject_WayPos;
    public UILabel Label_Des_Grow;
    public GameObject GameObject_PropPos;

    public UILabel Label_Open;
    public Transform GameObject_OpenPos;
    public GameObject Button_OpenEX;


    private Transform UI_Root;

    private GameObject labelTips;

    private Dictionary<int, string> ProficiencyLevel;

    private void Awake ()
    {

        UI_Root = FindObjectOfType<UIRoot> ().transform;
    }

    // 初始化的方法
    private void Start ()
    {
        ClickController ();

        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        labelTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        labelTips.SetActive (false);
    }

    public void SetInfo (int _id)
    {
        ////var knowlegeStore = Charactor.GetInstance.KnowLedgeDic;
        //var charactor = Charactor.GetInstance;
        //ProficiencyLevel = Constants.ProficiencyLevel;

        //var selectDao = SelectDao.GetDao ();

        //// 学识大类
        //if (_id <= 5)
        //{
        //    GameObject_MainClass.SetActive (true);
        //    GameObject_Book.SetActive (false);

        //    Knowledge knowledge = selectDao.SelectKnowledge (_id);

        //    // 大类名称和介绍
        //    Label_Name.text = knowledge.name;
        //    Label_Writer.text = knowledge.author;
        //    Lable_DescEX.text = knowledge.des;

        //    // 称号

        //    // 当前称号
        //    int level = charactor.GetLv_Skill(true, _id);
        //    int identityId_now = knowlegeStore [_id].grow.growData [level].title;
        //    if (identityId_now == 0)
        //    {
        //        while (level >= 0)
        //        {
        //            if (knowlegeStore [_id].grow.growData [--level].title != 0)
        //            {
        //                identityId_now = knowlegeStore [_id].grow.growData [level].title;
        //                break;
        //            }
        //        }
        //        if (identityId_now != 0)
        //            Button_Now.GetComponentInChildren<UILabel> ().text = selectDao.SelectIdentity (identityId_now).identityName;
        //    }
        //    else
        //        Button_Now.GetComponentInChildren<UILabel> ().text = selectDao.SelectIdentity (identityId_now).identityName;
        //    Button_Now.name = identityId_now.ToString ();

        //    // 所有称号
        //    int num = -1;
        //    foreach (int lv in knowlegeStore [_id].grow.growData.Keys)
        //    {
        //        int identityId = knowlegeStore [_id].grow.growData [lv].title;
        //        if (identityId == 0)
        //            continue;

        //        GameObject ex = Instantiate (Button_AllEX);
        //        ex.name = identityId.ToString ();

        //        ex.transform.Find ("Label_All").GetComponent<UILabel> ().text = selectDao.SelectIdentity (identityId).identityName;

        //        ex.transform.parent = GameObject_AllPos;
        //        ex.transform.localScale = Vector3.one;
        //        ex.transform.localPosition = new Vector3 (0, ++num * -30, 0);

        //        UIEventListener.Get (ex).onClick = ClickIdentity;
        //    }
        //    Button_AllEX.SetActive (false);
        //}

        //// 具体书籍
        //else
        //{
        //    GameObject_MainClass.SetActive (false);
        //    GameObject_Book.SetActive (true);

        //    Knowledge knowledge = selectDao.SelectKnowledge (_id);

        //    KnowledgeType type = knowledge.aheadType;

        //    // 书名和图片
        //    Sprite_BookIcon.atlas = FindObjectOfType<GameObject_Static> ().UIAtlas_Icon_Item1;
        //    // TOUPDATE 木有图
        //    //Sprite_BookIcon.spriteName = Constants.Constants.Items_All[knowlege_All[_id].itemId].icon;
        //    Label_Name_Book.text = knowledge.name;

        //    // 介绍
        //    Label_Writer_book.text = knowledge.author;
        //    Lable_DescEX_book.text = knowledge.des;

        //    // 程度
        //    ProgressBar_Grow.value = (float) knowlegeStore [_id].Exp / knowlegeStore [(int) type + 10].grow.growData [9].exp;
        //    Label_Grow.text = ProficiencyLevel [Charactor.GetInstance.GetLv_Skill(true, _id)];

        //    // 途径
        //    int num = 0;
        //    foreach (string str in knowlegeStore [(int) type + 10].growWay)
        //    {
        //        GameObject clone = Instantiate (Button_WayEX);
        //        clone.name = "way" + "_" + (int) type + "_" + ++num;

        //        clone.transform.parent = GameObject_WayPos;
        //        clone.transform.localScale = Vector3.one;
        //        clone.transform.localPosition = new Vector3 (60 * (num - 1), 0, 0);

        //        clone.transform.Find ("Label_Way").GetComponent<UILabel> ().text = num.ToString ();

        //        UIEventListener.Get (clone).onHover = OpenLabelTips;
        //    }
        //    Button_WayEX.SetActive (false);

        //    // 成长
        //    Label_Des_Grow.text = LanguageMgr.GetInstance.GetText ("skill_51");
        //    Dictionary<int, int> propDic = Charactor.GetInstance.CalculatePropertyIncrement (true, (int) type + 10);
        //    if (propDic != null && propDic.Count > 0)
        //    {
        //        num = -1;
        //        foreach (int key in propDic.Keys)
        //        {
        //            GameObject clone = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_PropEX, GameObject_PropPos.transform, Vector3.one, new Vector3 (-105 + ++num * 50, 0, 0)).gameObject;

        //            //clone.transform.Find ("Label_PropName").GetComponent<UILabel> ().text = Constants.RoleProp [(RoleInfo) key];

        //            // TOUPDATE 应该改成文字显示
        //            clone.transform.Find ("Label_PropAdd").GetComponent<UILabel> ().text = propDic [key].ToString ();
        //        }
        //    }
        //    else
        //    {
        //        GameObject none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_PropPos.transform, new Vector3 (.8f, .8f), new Vector3 (0, -5, 0)).gameObject;
        //    }

        //    // 开启技法
        //    Label_Open.text = LanguageMgr.GetInstance.GetText ("skill_73");

        //    int level = Charactor.GetInstance.GetLv_Skill(true, _id);
        //    num = -1;
        //    Knowledge_Grow know_Grow = knowledge.grow;
        //    for (int lv = 0; lv <= level; lv++)
        //    {
        //        if (know_Grow.growData [lv].skill != null)
        //        {
        //            foreach (int skillId in knowlegeStore [_id].grow.growData [lv].skill)
        //            {
        //                GameObject clone = Instantiate (Button_OpenEX);
        //                clone.name = skillId.ToString ();

        //                clone.transform.parent = GameObject_OpenPos;
        //                clone.transform.localScale = Vector3.one;
        //                clone.transform.localPosition = new Vector3 (++num * 40, 0, 0);

        //                clone.transform.Find ("Label_OpenName").GetComponent<UILabel> ().text = selectDao.SelectSkill (skillId).name;

        //                UIEventListener.Get (clone).onClick = ClickSkill;
        //            }
        //        }
        //    }

        //    Button_OpenEX.SetActive (false);
        //}
    }

    private void OpenLabelTips (GameObject btn, bool isOver)
    {
        if (isOver)
        {
            labelTips.SetActive (true);
            string [] reg = btn.name.Split ('_');
            int type = int.Parse (reg [1]);
            int id = int.Parse (reg [2]);
            //labelTips.GetComponent<LableTipsUI> ().SetAll (true, Charactor.GetInstance.KnowLedgeDic [type + 10].growWay [id - 1]);
        }
        else
        {
            labelTips.SetActive (false);
        }
    }

    private void ClickSkill (GameObject btn)
    {
        //GameObject skill_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Skill, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        //skill_UI.GetComponent<SkillUI> ().ClickSkillClass (btn);
        //skill_UI.GetComponent<SkillUI> ().SetInfo (int.Parse (btn.name));
        //skill_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Now).onClick = ClickIdentity;
    }

    private void Back (GameObject btn)
    {
        Destroy (labelTips);
        Destroy (gameObject);
    }

    private void ClickIdentity (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject iden_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_IdentityInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        iden_UI.GetComponent<IdentityInfoUI> ().SetInfo (id);
        iden_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }
}