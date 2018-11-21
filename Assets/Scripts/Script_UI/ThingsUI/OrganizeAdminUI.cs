// ======================================================================================
// 文 件 名 称：OrganizeAdminUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 17:45:54
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-15 20:58
// ======================================================================================
// 功 能 描 述：UI：店铺（人事管理）职位一览
// ======================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeAdminUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public UIText Label_OrgName;

    public GameObject Button_Belong;
    public UIText Label_Belong;

    public Transform GameObject_PartPos;
    public Transform GameObject_PartEX;
    public Transform Button_RoleIconEX;

    public GameObject Button_Recruit;
    public UIText Label_Recruit;

    private string [] buttonText;
    private List<ButtonTipsUI.ButtonText> buttonPost;
    private List<ButtonTipsUI.ButtonText> awardButton;
    private List<ButtonTipsUI.ButtonText> punishButton;
    private string [] fireText;
    private int _selectRoleId; // 当前选中的人物ID
    private bool isPower; // 是否拥有任命权（即店主或掌柜）
    private Transform _selectRole;
    private int shopId;

    private void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel (gameObject);

        ClickControl ();

        SetAll (101);
    }

    /// <summary>
    /// 设置店铺职位一览信息
    /// </summary>
    /// <param name="_shopId">店铺id</param>
    public void SetAll (int _shopId)
    {
        shopId = _shopId;
        var common = CommonFunc.GetInstance;
        var shop = RunTime_Data.ShopDic [_shopId];
        var roleDic = RunTime_Data.RolePool;
        var dao = SelectDao.GetDao ();
        var charactor = Charactor.GetInstance;


        Label_OrgName.SetText (false, shop.Name);
        Button_Belong.name = shop.HostId.ToString ();
        Label_Belong.SetText(false, roleDic[shop.HostId].commonName);
        if (shop.HostId == -1)
            common.SetButtonState (Button_Belong, false);

        ShowPost ();

        var lm = LanguageMgr.GetInstance;
        //var charactorPost = charactor.GetPostId (_shopId);
        //var postInfo = dao.SelecRole_Job (charactorPost);
        //// 如果主角是店主或者掌柜，可以招募
        //if (charactorPost == 301 || charactorPost == 303)
        //{
        //    isPower = true;
        //    Label_Recruit.SetText (true, "Things73");
        //    UIEventListener.Get (Button_Recruit).onClick = Recruit;

        //    string [] temp = postInfo.power.Split (';');
        //    buttonPost = new List<ButtonTipsUI.ButtonText> ();
        //    foreach (string str in temp)
        //    {
        //        var id = int.Parse (str);

        //        buttonPost.Add (new ButtonTipsUI.ButtonText (id, dao.SelecRole_Job (id).name));
        //    }
        //    awardButton = new List<ButtonTipsUI.ButtonText>
        //    {
        //        new ButtonTipsUI.ButtonText(4, lm.GetText("Things78")),
        //        new ButtonTipsUI.ButtonText(5, lm.GetText("Relation11")),
        //        new ButtonTipsUI.ButtonText(6, lm.GetText("Tips_6"))
        //    };
        //    punishButton = new List<ButtonTipsUI.ButtonText>
        //    {
        //        new ButtonTipsUI.ButtonText(7, lm.GetText("Things79")),
        //        new ButtonTipsUI.ButtonText(8, lm.GetText("Things80")),
        //        new ButtonTipsUI.ButtonText(6, lm.GetText("Tips_6"))
        //    };
        //    fireText = new string [4];
        //    fireText [0] = lm.GetText ("Tips_9");
        //    fireText [1] = lm.GetText ("Tips_System_10");
        //    fireText [2] = lm.GetText ("Tips_8");
        //    fireText [3] = lm.GetText ("Tips_7");
        //}
        // 否则只能申请职位
        //else
        //{
        //    isPower = false;
        //    Label_Recruit.SetText (true, "Things72");
        //    UIEventListener.Get (Button_Recruit).onClick = ApplyPost;
        //}

        buttonText = new string [4];
        buttonText [0] = lm.GetText ("Things74");
        buttonText [1] = lm.GetText ("Things75");
        buttonText [2] = lm.GetText ("Things76");
        buttonText [3] = lm.GetText ("Things77");
    }

    // 清空职位列表
    private void ClearAll ()
    {
        foreach (Transform child in GameObject_PartPos)
        {
            if (child.name != GameObject_PartEX.name && child.name != GameObject_PartPos.name)
                Destroy (child.gameObject);
        }
    }

    // 显示职位列表
    private void ShowPost ()
    {
        var common = CommonFunc.GetInstance;
        var dao = SelectDao.GetDao ();
        var charactor = Charactor.GetInstance;
        var tempPost = Temp_Data.GetInstance.TempEmployee;
        var roleDic = RunTime_Data.RolePool;

        GameObject_PartEX.gameObject.SetActive (true);
        Button_RoleIconEX.gameObject.SetActive (true);

        var postList = tempPost.ContainsKey (shopId) ? tempPost [shopId] : RunTime_Data.ShopDic [shopId].Post;
        int num = -1;
        foreach (var post in postList.Keys)
        {
            Transform duty = common.UI_Instantiate (GameObject_PartEX, GameObject_PartPos, new Vector3 (0, -100 * ++num), Vector3.one);
            duty.Find ("Label_RoleIdentity").GetComponent<UIText> ().SetText (false, dao.SelecRole_Job (post).name);

            Transform pos = duty.Find ("GameObject_PartScroll/GameObject_RolePos");

            int numOfRole = -1;
            foreach (var worker in postList [post].Values)
            {
                Transform roleGo = common.UI_Instantiate (Button_RoleIconEX, pos, new Vector3 (130 * ++numOfRole, 0), Vector3.one);

                roleGo.name = worker.Id + "_" + post;

                roleGo.Find("Sprite_RoleIcon").GetComponent<UISprite>().spriteName = roleDic[worker.Id].headIcon;
                roleGo.Find ("Label_RoleName").GetComponent<UIText> ().SetText (false, worker.Name);
                if (worker.Id == -1)
                {
                    roleGo.Find ("Label_RoleRelation ").GetComponent<UIText> ().SetText (true, "Things54");
                    common.SetButtonState (roleGo.gameObject, false);
                }
                else
                {
                    //var relation = charactor.Relationship_Fixed.ContainsKey(worker.Id) ? charactor.Relationship_Fixed[worker.Id].Relationship.name : charactor.Relationship_Snap[worker.Id].Relationship.name;
                    //roleGo.Find("Label_RoleRelation ").GetComponent<UIText>().SetText(false, relation);
                    UIEventListener.Get (roleGo.gameObject).onClick = ShowButtons;
                }

            }
        }
        GameObject_PartEX.gameObject.SetActive (false);
        Button_RoleIconEX.gameObject.SetActive (false);
    }

    // TODO 申请职位
    private void ApplyPost (GameObject go)
    {
        throw new NotImplementedException ();
    }

    // TODO 招募
    private void Recruit (GameObject go)
    {
        throw new NotImplementedException ();
    }

    // 点选人物，（主角是店主或掌柜）弹出按钮组，（其他职位）直接显示人物信息
    private void ShowButtons (GameObject go)
    {
        _selectRoleId = int.Parse (go.name.Split ('_') [0]);
        _selectRole = go.transform;
        if (_selectRoleId == -1)
            return;
        if (isPower)
        {
            // 合伙人只能查看信息
            if (int.Parse (go.name.Split ('_') [1]) == 302)
            {
                Transform roleInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_RoleInfo, transform, Vector3.zero, Vector3.one);
                roleInfo.GetComponent<RoleInfoUI> ().SetInfo (_selectRoleId);
                roleInfo.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
            }
            // 其他职位弹出选项
            else
            {
                var buttonTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ButtonTips, transform, Vector3.zero, Vector3.one).gameObject;
                buttonTips.GetComponent<ButtonTipsUI> ().SetAll (true,buttonText);
                var p = go.transform.parent;
                go.transform.SetParent (buttonTips.transform.parent);
                buttonTips.transform.localPosition = go.transform.localPosition + new Vector3 (155, -65);
                go.transform.SetParent (p);
            }
        }
        else
        {
            Transform roleInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_RoleInfo, transform, Vector3.zero, Vector3.one);
            roleInfo.GetComponent<RoleInfoUI> ().SetInfo (_selectRoleId);
            roleInfo.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        }
    }

    // 接收点选的按钮
    private void ReceiveButtonState (int _clickButton)
    {
        switch (_clickButton)
        {
            // 人物信息
            case 0:
                Transform roleInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_RoleInfo, transform, Vector3.zero, Vector3.one);
                roleInfo.GetComponent<RoleInfoUI> ().SetInfo (_selectRoleId);
                roleInfo.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
            // 调整职位
            case 1:
                ShowButtonTips (buttonPost.ToArray ());
                break;
            // 奖励
            case 2:
                ShowButtonTips (awardButton.ToArray ());
                break;
            // 处罚
            case 3:
                ShowButtonTips (punishButton.ToArray ());
                break;
            // 提薪
            case 4:
                Transform changePay = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ChangePay, transform, Vector3.zero, Vector3.one);
                changePay.GetComponent<ChangePayUI> ().SetAll (shopId, _selectRoleId, true);
                changePay.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
            // TODO 赠礼
            case 5:
                Transform gift = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Gift, transform, Vector3.zero, Vector3.one);
                gift.GetComponent<GiftUI> ().Open (_selectRoleId);
                gift.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
            // 返回第一组按钮组
            case 6:
                ShowButtonTips (buttonText);
                break;
            // 降薪
            case 7:
                changePay = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ChangePay, transform, Vector3.zero, Vector3.one);
                changePay.GetComponent<ChangePayUI> ().SetAll (shopId, _selectRoleId, false);
                changePay.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
            // 开革
            case 8:
                Transform systemTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_SystemTips, transform, Vector3.zero, Vector3.one);
                //systemTips.GetComponent<SystemTipsUI> ().SetTipDesc (fireText [0], string.Format (fireText [1], RunTime_Data.RolePool [_selectRoleId].CommonName), fireText [2], fireText [3]);
                systemTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
            // 职位调整--返回值为调整后的职位ID
            default:
                var old = int.Parse (_selectRole.name.Split ('_') [1]);
                if (old == _clickButton)
                    return;
                Temp_Data.GetInstance.AdjustEmployee (shopId, _selectRoleId, old, _clickButton);
                ClearAll ();
                ShowPost ();
                Transform labelTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, transform, Vector3.zero, Vector3.one);
                labelTips.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable8"));
                labelTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
                break;
        }
    }

    // 接收systemTips选择结果
    private void ReceiveSystemTips (bool _yes)
    {
        if (_yes)
        {
            Temp_Data.GetInstance.AdjustEmployee (shopId, _selectRoleId, int.Parse (_selectRole.name.Split ('_') [1]), -1);
            ClearAll ();
            ShowPost ();
        }
    }

    // 显示按钮组
    private void ShowButtonTips (string [] _arr)
    {
        var buttonTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ButtonTips, transform, Vector3.zero, Vector3.one);
        buttonTips.GetComponent<ButtonTipsUI> ().SetAll (true, _arr);
        var p = _selectRole.parent;
        _selectRole.SetParent (buttonTips.transform.parent);
        buttonTips.localPosition = _selectRole.localPosition + new Vector3 (155, -65);
        _selectRole.SetParent (p);
    }

    // 显示按钮组
    private void ShowButtonTips (ButtonTipsUI.ButtonText [] _arr)
    {
        var buttonTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ButtonTips, transform, Vector3.zero, Vector3.one);
        buttonTips.GetComponent<ButtonTipsUI> ().SetAll (true, _arr);
        var p = _selectRole.parent;
        _selectRole.SetParent (buttonTips.transform.parent);
        buttonTips.localPosition = _selectRole.localPosition + new Vector3 (155, -65);
        _selectRole.SetParent (p);
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Belong).onClick = OpenOwnerInfo;
    }

    // 开放店主信息UI
    private void OpenOwnerInfo (GameObject go)
    {
        int id = int.Parse (go.name);
        Transform roleInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_RoleInfo, transform, Vector3.zero, Vector3.one);
        roleInfo.GetComponent<RoleInfoUI> ().SetInfo (id);
        roleInfo.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}