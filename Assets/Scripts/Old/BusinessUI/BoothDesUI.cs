// ======================================================================================
// 文 件 名 称：BoothDesUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:12:32
// 最 后 修 改：xic
// 更 新 时 间：2017-11-13 20:47
// ======================================================================================
// 功 能 描 述：UI：摊位信息
// ======================================================================================
using System;
using UnityEngine;

public class BoothDesUI : MonoBehaviour
{
    public UISprite Sprite_OrgIcon;
    public UIText Label_OrgName;
    public UIText Label_Num;
    public UIText Label_Owner;
    public UIText Label_Area;
    public UIText Label_State;
    public UIText Label_Place;
    public UIText Label_Popularity;
    public Transform GameObject_MoneyPos;

    public GameObject Button_Close;
    //public GameObject Sprite_Back;

    private void Start ()
    {
        ClickControl ();

        SetAll (101);
    }

    public void SetAll (int _shopId)
    {
        var shop = RunTime_Data.ShopDic [_shopId];
        var dao = SelectDao.GetDao ();

        // TODO Sprite_OrgIcon赋值
        Label_OrgName.SetText (false, shop.Name);
        Label_Num.SetText (false, shop.Id.ToString ());
        var host = shop.HostId == -1 ? Charactor.GetInstance.commonName : RunTime_Data.RolePool [shop.HostId].commonName;
        Label_Owner.SetText (false, host);
        Label_Area.SetText (false, dao.SelectBusiness_Area (shop.BusinessAreaId).name);
        // TODO 生意状态
        Label_State.SetText (false, "");
        var enveroment = dao.SelectBusiness_Environment (shop.BusinessEnveromentId);
        Label_Place.SetText (false, enveroment.name);
        var popular = enveroment.popular;
        Label_Popularity.SetText (false, GetPopular (popular));
        GameObject money = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one).gameObject;
        money.GetComponent<Price_MoneyEXUI> ().SetMoney (enveroment.booth_price);
    }

    // 人气度的文字显示
    private string GetPopular (int popular)
    {
        if (popular >= 80)
            return "高";
        else if (popular >= 60)
            return "中高";
        else if (popular >= 40)
            return "中";
        else if (popular >= 20)
            return "中低";
        else
            return "低";
    }

    private void ClickControl ()
    {
        //UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
        
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}