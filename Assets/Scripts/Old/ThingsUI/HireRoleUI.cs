// ======================================================================================
// 文 件 名 称：HireRoleUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-18 10:50:24
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-18 10:50:24
// ======================================================================================
// 功 能 描 述：UI：牙行招募/申请工作
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

public class HireRoleUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_DesBack;

    public GameObject GameObject_MainList;
    public GameObject GameObject_Way;
    public GameObject GameObject_WorkApply;

    public Transform Button_JobEX;

    // MainList
    public Transform GameObject_Pos_Jiang;
    public Transform GameObject_Pos_Pu;
    public Transform GameObject_Pos_Za;
    public Transform GameObject_Pos_Other;

    // RoleList
    public UIText Label_DesTitle_Type;
    public Transform GameObject_Pos_Way;
    public Transform Button_WayEx;
    public GameObject Button_AddTask;

    // Apply
    public GameObject Button_Job;
    public UIText Label_Job;
    public GameObject Button_Iden;
    public UIText Label_Iden;
    public Transform GameObject_NeedPos;
    public Transform GameObject_MarketPos;
    public GameObject Button_Apply;
    public GameObject Sprite_Back_Apply;


    private Transform _selectIden; // 当前选中的身份
    private bool _recruit; // 招聘？申请？
    private int brokerId;
    private Transform _selectPerson;
    private List<People> _peopleList;
    private People _person; // 选中的人

    private void Start ()
    {
        ClickControl ();

        SetAll (301, false);
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="recruit">招聘？申请？</param>
    public void SetAll (int _brokerId, bool recruit)
    {
        //_recruit = recruit;
        //brokerId = _brokerId;

        //var dao = SelectDao.GetDao ();
        //var comm = CommonFunc.GetInstance;

        //string [] iden = dao.SelectSystem_Config (66).value.Split (';');
        //int num = -1;
        //foreach (var id in iden)
        //{
        //    Transform clone = comm.UI_Instantiate (Button_JobEX, GameObject_Pos_Jiang, new Vector3 (0, -40 * ++num), Vector3.one);
        //    clone.name = id.ToString ();
        //    //var detail = dao.SelectIdentity (int.Parse (id));
        //    //clone.Find ("Label_Job").GetComponent<UIText> ().SetText (false, detail.identityName);
        //    //clone.Find ("Sprite_JobIcon").GetComponent<UISprite> ().spriteName = detail.icon;

        //    UIEventListener.Get (clone.gameObject).onClick = SelectOne;
        //}

        //iden = dao.SelectSystem_Config (67).value.Split (';');
        //num = -1;
        //foreach (var id in iden)
        //{
        //    Transform clone = comm.UI_Instantiate (Button_JobEX, GameObject_Pos_Pu, new Vector3 (0, -40 * ++num), Vector3.one);
        //    clone.name = id.ToString ();
        //    //var detail = dao.SelectIdentity (int.Parse (id));
        //    //clone.Find ("Label_Job").GetComponent<UIText> ().SetText (false, detail.identityName);
        //    //clone.Find ("Sprite_JobIcon").GetComponent<UISprite> ().spriteName = detail.icon;

        //    UIEventListener.Get (clone.gameObject).onClick = SelectOne;
        //}

        //iden = dao.SelectSystem_Config (68).value.Split (';');
        //num = -1;
        //foreach (var id in iden)
        //{
        //    Transform clone = comm.UI_Instantiate (Button_JobEX, GameObject_Pos_Za, new Vector3 (0, -40 * ++num), Vector3.one);
        //    clone.name = id.ToString ();
        //    //var detail = dao.SelectIdentity (int.Parse (id));
        //    //clone.Find ("Label_Job").GetComponent<UIText> ().SetText (false, detail.identityName);
        //    //clone.Find ("Sprite_JobIcon").GetComponent<UISprite> ().spriteName = detail.icon;

        //    UIEventListener.Get (clone.gameObject).onClick = SelectOne;
        //}

        //iden = dao.SelectSystem_Config (69).value.Split (';');
        //num = -1;
        //foreach (var id in iden)
        //{
        //    Transform clone = comm.UI_Instantiate (Button_JobEX, GameObject_Pos_Other, new Vector3 (0, -40 * ++num), Vector3.one);
        //    clone.name = id.ToString ();
        //    //var detail = dao.SelectIdentity (int.Parse (id));
        //    //clone.Find ("Label_Job").GetComponent<UIText> ().SetText (false, detail.identityName);
        //    //clone.Find ("Sprite_JobIcon").GetComponent<UISprite> ().spriteName = detail.icon;

        //    UIEventListener.Get (clone.gameObject).onClick = SelectOne;
        //}

        //Button_JobEX.gameObject.SetActive (false);
    }

    // 显示应聘的人
    private void ShowRoles (int _idenId)
    {
        //_peopleList = RunTime_Data.BrokerDic [brokerId].GetPeople (_idenId);
        var comm = CommonFunc.GetInstance;

        Button_WayEx.gameObject.SetActive (true);

        //Label_DesTitle_Type.SetText (false, SelectDao.GetDao ().SelectIdentity (_idenId).identityName);

        int num = -1;
        foreach (var item in _peopleList)
        {
            Transform clone = comm.UI_Instantiate (Button_WayEx, GameObject_Pos_Way, new Vector3 (0, -70 * ++num), Vector3.one);
            //clone.name = item.role.Id.ToString ();
            if (item.gotten)
            {
                clone.Find ("GameObject_AlreadyChoose").gameObject.SetActive (true);
                comm.SetButtonState (_selectPerson.gameObject, false);
            }
            Transform info = clone.Find ("GameObject_Info");

            Transform icon = comm.UI_Instantiate(Data_Static.UIpath_Button_RoleIcon, info.Find("GameObject_RoleIconPos"), Vector3.zero, Vector3.one);
            icon.Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = item.role.headIcon;
            info.Find("Label_RoleName").GetComponent<UIText>().SetText(false, item.role.commonName);
            info.Find("Label_Gender").GetComponent<UIText>().SetText(false, item.role.gender);
            info.Find("Label_Age").GetComponent<UIText>().SetText(false, item.role.birthday.ToString());

            //foreach (var skill in item.role.SkillDic.Values)
            //{
            //    if (skill.type == SkillType.标准技法)
            //        continue;
            //    Transform skillButton = comm.UI_Instantiate (Data_Static.UIpath_Button_SkillEX, info.Find ("GameObject_SkillPos"), Vector3.zero, Vector3.one);
            //    //skillButton.GetComponent<Button_SkillEXUI> ().SetState (skill.name, item.role.GetLv_Skill(false, skill.id), -1);
            //    skillButton.GetComponent<UIButton> ().enabled = false;
            //}

            Transform money = comm.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, info.Find ("GameObject_MoneyPos"), Vector3.zero, Vector3.one);
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (item.salary);

            UIEventListener.Get (clone.gameObject).onClick = SelectRole;
        }

        Button_WayEx.gameObject.SetActive (false);

        UIEventListener.Get (Button_AddTask).onClick = AddTask;

    }

    // 添加一个招募消息
    private void AddTask (GameObject go)
    {
        if (!_selectPerson || _person.gotten)
            return;

        // 添加消息
        var news = NewsStore.GetInstance.GetNews (2001);
        _person.gotten = true;

        var lm = LanguageMgr.GetInstance;

        // 提示成功
        Transform newsTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_NewTips, transform, Vector3.zero, Vector3.one);
        newsTips.GetComponent<NewTipsUI> ().SetNew (null, lm.GetText ("Tips_Title_1"), news.name);

        _selectPerson.Find ("GameObject_AlreadyChoose").gameObject.SetActive (true);
        CommonFunc.GetInstance.SetButtonState (_selectPerson.gameObject, false);
    }

    // 在人物列表中点选一个人
    private void SelectRole (GameObject go)
    {
        if (_selectPerson)
            _selectPerson.Find ("Sprite_NowChoose").gameObject.SetActive (false);
        _selectPerson = go.transform;

        foreach (var item in _peopleList)
        {
            if (item.role.id == int.Parse(go.name))
            {
                _person = item;
                break;
            }
        }
        if (_person != null)
        {
            _selectPerson.Find ("Sprite_NowChoose").gameObject.SetActive (true);

        }
    }



    // 打开申请
    private void ShowApply (int _idenId)
    {
        var dao = SelectDao.GetDao ();
        //var iden = dao.SelectIdentity(_idenId);
        var comm = CommonFunc.GetInstance;
        var charactor = Charactor.GetInstance;

        UIEventListener.Get (Sprite_Back_Apply).onClick = ApplyBack;

        //Label_Job.SetText (false, dao.SelectIdentity (iden.frontId).identityName);
        //Button_Job.name = iden.frontId.ToString ();
        //UIEventListener.Get (Button_Job).onClick = ClickIdentity;
        //Label_Iden.SetText (false, iden.identityName);
        //Button_Iden.name = iden.id.ToString ();
        //UIEventListener.Get (Button_Iden).onClick = ClickIdentity;

        bool _allowToApply = true;

        int num = -1;
        //if (iden.needKnow != null && iden.needKnow.Count > 0)
        //{
        //    foreach (var knowId in iden.needKnow)
        //    {
        //        var know = dao.SelectKnowledge (knowId [0]);
        //        Transform clone = comm.UI_Instantiate (Data_Static.UIpath_Button_SkillEX, GameObject_NeedPos, new Vector3 (60 * ++num, 0), Vector3.one);
        //        clone.GetComponent<UIButton> ().enabled = false;
        //        //var lv = charactor.GetLv_Skill(true, knowId [0]);
        //        //clone.GetComponent<Button_SkillEXUI> ().SetState (know.name, lv, (lv >= knowId [1]) ? 1 : 2);
        //        //if (lv < knowId [1])
        //        //    _allowToApply = false;
        //    }
        //}

        //if (iden.needSkill != null && iden.needSkill.Count > 0)
        //{
        //    foreach (var skillneed in iden.needSkill)
        //    {
        //        var skill = dao.SelectSkill (skillneed [0]);
        //        Transform clone = comm.UI_Instantiate (Data_Static.UIpath_Button_SkillEX, GameObject_NeedPos, new Vector3 (60 * ++num, 0), Vector3.one);
        //        clone.GetComponent<UIButton> ().enabled = false;
        //        //var lv = charactor.GetLv_Skill(false, skillneed [0]);
        //        //clone.GetComponent<Button_SkillEXUI> ().SetState (skill.name, lv, (lv >= skillneed [1]) ? 1 : 2);
        //        //if (lv < skillneed [1])
        //        //    _allowToApply = false;
        //    }
        //}

        if (num == -1)
        {
            Transform clone = comm.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, GameObject_NeedPos, new Vector3 (60 * ++num, 0), Vector3.one);
        }

        var salary = comm.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MarketPos, Vector3.zero, Vector3.one);
        //salary.GetComponent<Price_MoneyEXUI> ().SetMoney (iden.salary [0]);

        if (_allowToApply)
            UIEventListener.Get (Button_Apply).onClick = Apply;
        else
            comm.SetButtonState (Button_Apply, false);
    }

    // 关闭申请界面
    private void ApplyBack (GameObject go)
    {
        foreach (Transform child in GameObject_NeedPos)
        {
            if (child.name != GameObject_NeedPos.name)
                Destroy (child.gameObject);
        }
        foreach (Transform child in GameObject_MarketPos)
        {
            if (child.name != GameObject_MarketPos.name)
                Destroy (child.gameObject);
        }

        GameObject_WorkApply.SetActive (false);
    }

    // 申请工作
    private void Apply (GameObject go)
    {
        var lm = LanguageMgr.GetInstance;
        var dao = SelectDao.GetDao ();

        // 已有工作提示
        //if (Charactor.GetInstance.WorkInShopId != -1 || Charactor.GetInstance.Shops.Count > 0)
        //{
        //    Transform labelTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, transform, Vector3.zero, Vector3.one);
        //    labelTips.GetComponent<LableTipsUI> ().SetAll (true, lm.GetText ("Tips_Lable9"));
        //    return;
        //}

        // 记录要去的店
        Business_Runtime_Shop chooseShop = null;
        // 安排一家商店
        foreach (var shop in RunTime_Data.ShopDic.Values)
        {
            //if (shop.BusinessAreaId == RunTime_Data.BrokerDic [brokerId].BusinessAreaId)
            //{
            //    var trade = dao.SelectBusiness_Trade (shop.TradeId);
            //    foreach (var post in shop.Post.Keys)
            //    {
            //        if (dao.SelecRole_Job (post).iden != int.Parse (_selectIden.name)
            //            || shop.Post [post].Count >= trade.list_Job [post])
            //            continue;

            //        chooseShop = shop;
            //        break;
            //    }
            //}
        }
        if (chooseShop == null)
        {
            Debug.Log ("没有商店");
            return;
        }

        // 添加消息
        var news = NewsStore.GetInstance.GetNews (2002);

        // 提示成功
        Transform newsTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_NewTips, transform, Vector3.zero, Vector3.one);
        newsTips.GetComponent<NewTipsUI> ().SetNew (null, lm.GetText ("Tips_Title_1"), news.name);
    }

    // 查看身份具体信息
    private void ClickIdentity (GameObject go)
    {
        var identityInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_IdentityInfo, transform, Vector3.zero, Vector3.one);
        identityInfo.GetComponent<IdentityInfoUI> ().SetInfo (int.Parse (go.name));
        identityInfo.GetComponent<UIPanel> ().depth = transform.GetComponent<UIPanel> ().depth + 10;
    }

    // 在职事列表中选中一个身份
    private void SelectOne (GameObject go)
    {
        if (_selectIden)
            _selectIden.Find ("Sprite_Choose").gameObject.SetActive (false);
        _selectIden = go.transform;
        _selectIden.Find ("Sprite_Choose").gameObject.SetActive (true);

        if (_recruit)
        {
            GameObject_MainList.SetActive (false);
            GameObject_Way.SetActive (true);
            ShowRoles (int.Parse (go.name));
        }
        else
        {
            GameObject_WorkApply.SetActive (true);
            ShowApply (int.Parse (go.name));
        }
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_DesBack).onClick = Back;
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}
