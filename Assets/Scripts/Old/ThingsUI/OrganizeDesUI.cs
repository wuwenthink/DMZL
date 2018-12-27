// ======================================================================================
// 文 件 名 称：OrganizeDesUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 12:09:18
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-13 19:46
// ======================================================================================
// 功 能 描 述：UI：店铺信息
// ======================================================================================

using UnityEngine;

public class OrganizeDesUI : MonoBehaviour
{
    public GameObject Button_Close;
    
    public UISprite Sprite_OrgIcon;
    public UIText Label_OrgName;
    public UIText Label_Belong;
    public UIText Label_Size;

    public UIText Label_Trade;
    public Transform GameObject_MoneyPos;

    public UIText Label_People;
    public UIText Label_State;

    // Use this for initialization
    void Start ()
    {
        ClickControl ();

        SetAll (false, 101);
    }

    public void SetAll (bool _shopOrHost, int _shopId)
    {
        var shop = RunTime_Data.ShopDic [_shopId];

        var trade = SelectDao.GetDao ().SelectBusiness_Trade (shop.TradeId);
        var lv = trade.lv;
        switch (lv)
        {
            case 1:
                Label_Size.SetText (true, "Things63");
                break;
            case 2:
                Label_Size.SetText (true, "Things64");
                break;
            case 3:
                Label_Size.SetText (true, "Things65");
                break;
        }
        Label_Trade.SetText (false, trade.name);

        Transform money = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos, Vector3.zero, Vector3.one);
        money.GetComponent<Price_MoneyEXUI> ().SetMoney (trade.taxNum);

        int num = 0;
        foreach (var roleList in shop.Post.Values)
        {
            foreach (var role in roleList)
            {
                num++;
            }
        }
        Label_People.SetText (false, num.ToString ());

        // TODO 生意状态
        Label_State.SetText (false, "");

    }

    private void ClickControl ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }

}
