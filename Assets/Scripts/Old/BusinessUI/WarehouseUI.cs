// ======================================================================================
// 文 件 名 称：WarehouseUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 23:26:47
// 最 后 修 改：xic
// 更 新 时 间：2017-11-08
// ======================================================================================
// 功 能 描 述：UI：货仓信息
// ======================================================================================
using System.Collections.Generic;
using UnityEngine;

public class WarehouseUI : MonoBehaviour
{

    public Transform GameObject_Pos_All;
    public Transform Button_ItemEX;
    public GameObject Button_Building;
    public UIText Label_Building;
    public GameObject Button_Update;
    public GameObject Button_Person;
    public UIText Label_Person;
    public GameObject Button_Hire;

    public UIText Label_Size;
    public UIText Label_Loss;
    public UIText Label_Damaged;
    public UIText Label_Fire;
    public UIText Label_Robber;
    public UIText Label_Dirty;

    public GameObject GameObject_Update;
    public GameObject Sprite_Back_Update;
    public GameObject Button_Repair;
    public GameObject Button_Size;
    public GameObject Button_Fire;
    public GameObject Button_Robber;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;

    Transform Money_Repair;
    Transform Money_Size;
    Transform Money_Fire;
    Transform Money_Robber;

    int money_Repair;
    int money_Size;
    int money_Fire;
    int money_Robber;

    Business_Runtime_Warehouse warehouse;
    void Awake()
    {
        CommonFunc.GetInstance.SetUIPanel(gameObject);
        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_Pos_All.GetComponent<UIPanel>().depth + 1;


        Money_Repair = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, Button_Repair.transform.Find("GameObject_MoneyPos"), Vector3.zero, Vector3.one);
        Money_Size = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, Button_Size.transform.Find("GameObject_MoneyPos"), Vector3.zero, Vector3.one);
        Money_Fire = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, Button_Fire.transform.Find("GameObject_MoneyPos"), Vector3.zero, Vector3.one);
        Money_Robber = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, Button_Robber.transform.Find("GameObject_MoneyPos"), Vector3.zero, Vector3.one);
    }

    private void Start ()
    {
        GameObject_Update.SetActive(false);

        //SetAll(Charactor.GetInstance.Warehouses[201]);//Test


        ClickControl ();
    }

    /// <summary>
    /// 开启界面初始化
    /// </summary>
    /// <param name="ware_Now"></param>
    public void SetAll(Business_Runtime_Warehouse ware_Now)
    {
        warehouse = ware_Now;
        //道具列表
        foreach (var item in ware_Now.GoodsDic)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_ItemEX, GameObject_Pos_All, Vector3.zero, Vector3.one);
            Transform itemIcon = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, go.Find("GameObject_ItemIconPos"), Vector3.zero, Vector3.one);
            int itemID = SelectDao.GetDao().SelectBusiness_Goods(item.Key).itemId;
            itemIcon.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
            itemIcon.GetComponent<Botton_ItemEXUI>().SetItem(itemID, 0);
            itemIcon.gameObject.name = itemID.ToString();
            UIEventListener.Get(itemIcon.gameObject).onHover = ShowTips;

            go.Find("Label_ItemName").GetComponent<UIText>().SetText(false, Constants.Items_All[itemID].name);
            go.Find("Label_Num").GetComponent<UIText>().SetText(false, item.Value.Count.ToString());
            go.gameObject.name = item.Key.ToString();
        }
        RefreshProp();
    }

    /// <summary>
    /// 刷新界面属性
    /// </summary>
    void RefreshProp()
    {
        Button_ItemEX.gameObject.SetActive(false);
        //左侧属性
        Label_Building.SetText(false, SelectDao.GetDao().SelectBusiness_Warehouse(warehouse.type).name);
        Label_Person.SetText(false, RunTime_Data.RolePool[warehouse.Person].commonName);
        Label_Size.SetText(false, warehouse.GetNow_Vol().ToString());
        Label_Damaged.SetText(false, warehouse.Demaged + " %");
        Label_Loss.SetText(false, warehouse.Loss + " %");
        Label_Fire.SetText(false, warehouse.GetNow_Fire().ToString());
        Label_Robber.SetText(false, warehouse.GetNow_Robber().ToString());
        Label_Dirty.SetText(false, warehouse.Dirty + " %");
    }

    /// <summary>
    /// 打开升级界面
    /// </summary>
    /// <param name="btn"></param>
    void OpenUpdate(GameObject btn)
    {
        GameObject_Update.SetActive(true);
        //整修
        if (warehouse.Demaged == 0)
        {
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, "Max");
            Button_Repair.transform.Find("Label_Value").GetComponent<UILabel>().color = Color.red;
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "- 0 %");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(0);
        }
        else
        {
            int vol = warehouse.Demaged;
            money_Repair = SelectDao.GetDao().SelectBusiness_Warehouse(warehouse.type).maintainPrice;
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, warehouse.Demaged.ToString());
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "- " + vol + " %");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(money_Repair);
        }

        //升级容量
        string[] lvUp_Vol = SelectDao.GetDao().SelectBusiness_Warehouse(warehouse.type).volValue.Split(';');
        if (warehouse.VolLevel == int.Parse(lvUp_Vol[1]))
        {
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, "Max");
            Button_Repair.transform.Find("Label_Value").GetComponent<UILabel>().color = Color.red;
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ 0");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(0);
        }
        else
        {
            int vol = int.Parse(lvUp_Vol[0]);
            money_Size = int.Parse(lvUp_Vol[2]);
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, warehouse.GetNow_Vol().ToString());
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ " + vol + " %");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(money_Size);
        }

        //升级防火
        string[] lvUp_Fire = SelectDao.GetDao().SelectBusiness_Warehouse(warehouse.type).fireValue.Split(';');
        if (warehouse.VolLevel == int.Parse(lvUp_Fire[1]))
        {
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, "Max");
            Button_Repair.transform.Find("Label_Value").GetComponent<UILabel>().color = Color.red;
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ 0");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(0);
        }
        else
        {
            int vol = int.Parse(lvUp_Fire[0]);
            money_Fire = int.Parse(lvUp_Fire[2]);
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, warehouse.GetNow_Fire().ToString());
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ " + vol + " %");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(money_Fire);
        }
        //升级防盗
        string[] lvUp_Robber = SelectDao.GetDao().SelectBusiness_Warehouse(warehouse.type).robberValue.Split(';');
        if (warehouse.VolLevel == int.Parse(lvUp_Robber[1]))
        {
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, "Max");
            Button_Repair.transform.Find("Label_Value").GetComponent<UILabel>().color = Color.red;
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ 0");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(0);
        }
        else
        {
            int vol = int.Parse(lvUp_Robber[0]);
            money_Robber = int.Parse(lvUp_Robber[2]);
            Button_Repair.transform.Find("Label_Value").GetComponent<UIText>().SetText(false, warehouse.GetNow_Robber().ToString());
            Button_Repair.transform.Find("Label_Add").GetComponent<UIText>().SetText(false, "+ " + vol + " %");
            Money_Repair.GetComponent<Price_MoneyEXUI>().SetMoney(money_Robber);
        }
    }

    /// <summary>
    /// 升级—整修
    /// </summary>
    /// <param name="btn"></param>
    void LvUp_Repair(GameObject btn)
    {
        if (money_Repair > Bag.GetInstance.money)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
            go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_20"));
        }
        else
        {
            warehouse.Demaged = 0;
        }
        OpenUpdate(btn);
    }
    /// <summary>
    /// 升级—容量
    /// </summary>
    /// <param name="btn"></param>
    void LvUp_Vol(GameObject btn)
    {
        if (money_Size > Bag.GetInstance.money)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
            go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_20"));
        }
        else
        {
            warehouse.VolLevel ++;
        }
        OpenUpdate(btn);
    }
    /// <summary>
    /// 升级—防火
    /// </summary>
    /// <param name="btn"></param>
    void LvUp_Fire(GameObject btn)
    {
        if (money_Fire > Bag.GetInstance.money)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
            go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_20"));
        }
        else
        {
            warehouse.FireLevel++;
        }
        OpenUpdate(btn);
    }
    /// <summary>
    /// 升级—防盗
    /// </summary>
    /// <param name="btn"></param>
    void LvUp_Robber(GameObject btn)
    {
        if (money_Robber > Bag.GetInstance.money)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
            go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_20"));
        }
        else
        {
            warehouse.RobberLevel++;
        }
        OpenUpdate(btn);
    }


    //显示道具tips
    void ShowTips(GameObject btn, bool isHover)
    {
        int itemID = int.Parse(btn.name);
        if (isHover)
        {
            ItemTip.SetActive(true);
            ItemTip.GetComponent<ItemTipsUI>().itemTipsSet(itemID, false);
        }
        else
        {
            ItemTip.SetActive(false);
        }
    }

    /// <summary>
    /// 查看建筑信息
    /// </summary>
    /// <param name="btn"></param>
    void BuildingInfo(GameObject btn)
    {

    }

    /// <summary>
    /// 查看个人信息
    /// </summary>
    /// <param name="btn"></param>
    void RoleInfo(GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_RoleInfo, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        go.GetComponent<RoleInfoUI>().SetInfo(warehouse.Person);
    }
    /// <summary>
    /// 雇佣人手
    /// </summary>
    /// <param name="btn"></param>
    void Hire(GameObject btn)
    {

    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
        UIEventListener.Get(Sprite_Back_Update).onClick = UpdateBack;
        UIEventListener.Get(Button_Update).onClick = OpenUpdate;

        UIEventListener.Get(Button_Building).onClick = BuildingInfo;
        UIEventListener.Get(Button_Person).onClick = RoleInfo;
        UIEventListener.Get(Button_Hire).onClick = Hire;

        UIEventListener.Get(Button_Repair).onClick = LvUp_Repair;
        UIEventListener.Get(Button_Size).onClick = LvUp_Vol;
        UIEventListener.Get(Button_Fire).onClick = LvUp_Fire;
        UIEventListener.Get(Button_Robber).onClick = LvUp_Robber;
    }

    void UpdateBack(GameObject btn)
    {
        RefreshProp();
        GameObject_Update.SetActive(false);
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}