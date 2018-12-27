// ======================================================================================
// 文 件 名 称：AccountUI.cs
// 版 本 编 号：v1.0.1
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:05:43
// 最 后 修 改：xic
// 更 新 时 间：2017-11-13 19:10
// ======================================================================================
// 功 能 描 述：账簿UI
// ======================================================================================
using System.Collections.Generic;
using UnityEngine;

public class AccountUI : MonoBehaviour
{
    public GameObject Button_Out;
    public GameObject Button_In;
    public GameObject Button_WareHouse;

    public UIText Label_Type1;
    public UIText Label_Type2;
    public UIText Label_Type3;

    public GameObject GameObject_ScrollList;
    public Transform GameObject_Pos;
    public Transform GameObject_AccountEX;
    public Transform GameObject_WareHouseEX;

    public Transform GameObject_MoneyPos;
    public UIText Label_AllNum;

    public GameObject GameObject_All;
    public UIPopupList PopupList_SwitchWarehouse;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    // 记录选中的标签页
    private GameObject seletedButton;
    RunTime_Data runTime;
    private void Start ()
    {
        ClickControl ();

        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        EventDelegate.Add (PopupList_SwitchWarehouse.onChange, ShowSelectWareHouse);
        PopupList_SwitchWarehouse.gameObject.SetActive (false);

        // 默认情况：显示支出
        ShowOut (Button_Out);
    }

    // 显示支出信息
    private void ShowOut (GameObject btn)
    {
        SwitchButtonState (btn);
        ClearAll ();

        GameObject_AccountEX.gameObject.SetActive (true);
        GameObject_WareHouseEX.gameObject.SetActive (false);
        PopupList_SwitchWarehouse.gameObject.SetActive (false);
        GameObject_All.SetActive (true);

        CommonFunc commonFunc = CommonFunc.GetInstance;

        Label_Type1.SetText (true, "Things58");
        Label_Type2.SetText (true, "Things59");
        Label_Type3.SetText (true, "Things60");

        // 查一下所有的支出
        var list = Bussiness_Account.GetInstance.ExpenseList;
        int num = -1;
        int cost = 0;
        foreach (var item in list)
        {
            Transform t = commonFunc.UI_Instantiate (GameObject_AccountEX, GameObject_Pos, new Vector3 (0, -55 * ++num), Vector3.one);
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, item.Time.ToString ());
            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, item.Event);
            GameObject money = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, t.Find ("GameObject_MoneyPos").transform, Vector3.zero, Vector3.one).gameObject;
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (item.Cost);
            cost += item.Cost;
        }
        GameObject_AccountEX.gameObject.SetActive (false);

        GameObject go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<Price_MoneyEXUI> ().SetMoney (cost);
        Label_AllNum.SetText (false, cost.ToString ());
    }

    // 显示收入信息
    private void ShowIn (GameObject btn)
    {
        SwitchButtonState (btn);
        ClearAll ();

        GameObject_AccountEX.gameObject.SetActive (true);
        GameObject_WareHouseEX.gameObject.SetActive (false);
        PopupList_SwitchWarehouse.gameObject.SetActive (false);
        GameObject_All.SetActive (true);

        CommonFunc commonFunc = CommonFunc.GetInstance;

        Label_Type1.SetText (true, "Things58");
        Label_Type2.SetText (true, "Things59");
        Label_Type3.SetText (true, "Things60");

        // 查一下所有的收入
        var list = Bussiness_Account.GetInstance.IncomeList;
        int num = -1;
        int cost = 0;
        foreach (var item in list)
        {
            Transform t = commonFunc.UI_Instantiate (GameObject_AccountEX, GameObject_Pos, new Vector3 (0, -55 * ++num), Vector3.one);
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, item.Time.ToString ());
            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, item.Event);
            GameObject money = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, t.Find ("GameObject_MoneyPos").transform, Vector3.zero, Vector3.one).gameObject;
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (item.Cost);
            cost += item.Cost;
        }
        GameObject_AccountEX.gameObject.SetActive (false);

        GameObject go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<Price_MoneyEXUI> ().SetMoney (cost);
        Label_AllNum.SetText (false, cost.ToString ());
    }

    // 显示仓库信息
    private void ShowWareHouse (GameObject btn)
    {
        SwitchButtonState (btn);
        ClearAll ();

        GameObject_AccountEX.gameObject.SetActive (false);
        GameObject_WareHouseEX.gameObject.SetActive (true);
        PopupList_SwitchWarehouse.gameObject.SetActive (true);
        GameObject_All.SetActive (false);

        Label_Type1.SetText (true, "Things58");
        Label_Type2.SetText (true, "Things61");
        Label_Type3.SetText (true, "Things62");

        var wareNamelist = new List<string> ();
        var wareIdlist = new List<int> ();
        var wareDic = runTime.Warehouses;
        foreach (var item in wareDic.Values)
        {
            wareNamelist.Add (item.name);
            wareIdlist.Add (item.Id);
        }

        PopupList_SwitchWarehouse.items = wareNamelist;
        PopupList_SwitchWarehouse.keepValue = true;
        PopupList_SwitchWarehouse.value = wareNamelist [0];

        ShowSelectWareHouse ();

    }

    // 切换仓库
    private void ShowSelectWareHouse ()
    {
        ClearAll ();
        CommonFunc commonFunc = CommonFunc.GetInstance;

        GameObject_WareHouseEX.gameObject.SetActive (true);

        var wareNamelist = new List<string> ();
        var wareIdlist = new List<int> ();
        var wareDic = runTime.Warehouses;
        foreach (var item in wareDic.Values)
        {
            wareNamelist.Add (item.name);
            wareIdlist.Add (item.Id);
        }

        int index = wareNamelist.IndexOf (PopupList_SwitchWarehouse.value);

        // 查一下选中仓库的情况
        var list = wareDic [wareIdlist [index]].WareRecordList;
        int num = -1;
        foreach (var item in list)
        {
            Transform t = commonFunc.UI_Instantiate (GameObject_WareHouseEX, GameObject_Pos, new Vector3 (0, -55 * ++num), Vector3.one);
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, item.Time.ToString ());
            var change = item.Change > 0 ? "+" + item.Change : item.Change.ToString ();
            t.Find ("Label_ChangeNum").GetComponent<UIText> ().SetText (false, change + " / " + wareDic [wareIdlist [index]].GoodsDic [item.GoodsId].Count);
            commonFunc.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, t.Find ("GameObject_ItemPos"), Vector3.zero, Vector3.one);
            // TODO 改图标
        }
        GameObject_WareHouseEX.gameObject.SetActive (false);
    }

    // 清除信息
    private void ClearAll ()
    {
        foreach (Transform child in GameObject_Pos)
        {
            if (child.name != GameObject_Pos.name && child.name != GameObject_AccountEX.name && child.name != GameObject_WareHouseEX.name)
                Destroy (child.gameObject);
        }
        foreach (Transform child in GameObject_MoneyPos)
        {
            if (child.name != GameObject_MoneyPos.name && child.name != GameObject_AccountEX.name && child.name != GameObject_WareHouseEX.name)
                Destroy (child.gameObject);
        }
    }

    // 切换按钮状态
    private void SwitchButtonState (GameObject btn)
    {
        if (btn == seletedButton)
            return;
        if (seletedButton)
        {
            seletedButton.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
            seletedButton.GetComponent<UIButton> ().enabled = true;
        }
        btn.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        btn.GetComponent<UIButton> ().enabled = false;
        seletedButton = btn;
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Out).onClick = ShowOut;
        UIEventListener.Get (Button_In).onClick = ShowIn;
        UIEventListener.Get (Button_WareHouse).onClick = ShowWareHouse;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}