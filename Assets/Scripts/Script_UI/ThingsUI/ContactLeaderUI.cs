// ======================================================================================
// 文 件 名 称：ContactLeaderUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 20:41:06
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-13 21:07
// ======================================================================================
// 功 能 描 述：UI：联系上级
// ======================================================================================

using System;
using UnityEngine;

public class ContactLeaderUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public GameObject GameObject_RoleIconPos;
    public UIText Label_RoleName;

    public GameObject Button_Nature;
    public UIText Label_Nature;

    public UIText Label_NowState;

    public GameObject Button_Letter;
    public GameObject Button_Place;

    void Start ()
    {
        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        ClickControl ();

        //SetAll (RunTime_Data.ShopDic [102].HostId);
    }

    /// <summary>
    /// 设置上司的信息
    /// </summary>
    /// <param name="_id">你上级的id</param>
    public void SetAll (int _id)
    {
        var boss = RunTime_Data.RolePool [_id];

        Label_RoleName.SetText (false, boss.commonName);
        //Label_Nature.SetText (false, SelectDao.GetDao ().SelecRole_Nature (boss.Nature).name);

        Label_NowState.SetText (false, "你俩的关系");
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Letter).onClick = ClickLetter;
        UIEventListener.Get (Button_Place).onClick = ClickPlace;
        UIEventListener.Get (Button_Nature).onClick = ClickNature;
    }

    // 打开性格信息UI
    private void ClickNature (GameObject go)
    {
        throw new NotImplementedException ();
    }

    // 传唤店东到店
    private void ClickPlace (GameObject go)
    {
        throw new NotImplementedException ();
    }

    // Email店东
    private void ClickLetter (GameObject go)
    {
        Transform t = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ButtonTips, transform, Vector3.zero, Vector3.one);
        var lm = LanguageMgr.GetInstance;
        t.GetComponent<ButtonTipsUI> ().SetAll (true, lm.GetText ("Tips_Button1"), lm.GetText ("Tips_Button2"), lm.GetText ("Tips_Button3"), lm.GetText ("Tips_Button4"));
    }

    // 接收按钮点击情况
    private void ReceiveButtonState (int clickButtonIndex)
    {
        switch (clickButtonIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}
