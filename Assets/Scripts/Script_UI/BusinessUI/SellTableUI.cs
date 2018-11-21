// ======================================================================================
// 文 件 名 称：SellTableUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:50:05
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:50:05
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTableUI : MonoBehaviour {
    public GameObject Label_Title;

    public GameObject GameObject_ScrollList;
    public GameObject GameObject_ItemListPos;
    public GameObject GameObject_ItemIconPos;
    public GameObject Label_ItemName;
    public GameObject Label_ItemNum;
    public GameObject Label_CountNow;

    public GameObject GameObject_MarketPos;
    public UIInput Input_GetNum;
    public GameObject Button_Reduce;
    public GameObject Button_Add;
    public GameObject GameObject_NowPos;
    public GameObject Button_ResetPrice;
    public GameObject Button_ChangePrice;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    GameObject choose_Goods;
    GameObject moneyMarket;
    GameObject moneyNow;
    int NowGoodsID = 0;
    int NowPrice = 0;

    Dictionary<int,Runtime_Goods> Dic_Goods;
    void Awake()
    {
        //Dic_Goods = Charactor.GetInstance.Shops[101].GoodsDic;      

        moneyMarket = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_MarketPos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyMarket.GetComponent<Price_MoneyEXUI>().SetMoney(0);
        moneyNow = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_NowPos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyNow.GetComponent<Price_MoneyEXUI>().SetMoney(0);

    }


    void Start()
    {
        CommonFunc.GetInstance.SetUIPanel(gameObject);
        
        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_ScrollList.GetComponent<UIPanel>().depth + 1;

        GoodsList();
        ClickControl();
    }


    void Update()
    {
        
    }

    /// <summary>
    /// 显示商店货物
    /// </summary>
    public void GoodsList()
    {
        //在列表中生成道具ICON
        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        int count = 0;
        foreach (var item in Dic_Goods)
        {
            if (row % 6 == 0)
            {
                row = 0;
                col++;
            }
            int itemID = SelectDao.GetDao().SelectBusiness_Goods(item.Key).itemId;
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_ItemListPos.transform, new Vector3(0 + col * (-67), 0 + row * (-67), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, item.Value.Count);
            go.name = item.Key.ToString();

            //默认显示第一个的数据
            if (count == 0)
            {
                chooseGoods(go);
            }

            UIEventListener.Get(go).onClick = chooseGoods;
            UIEventListener.Get(go).onHover = ShowTips;
            row++;
            count++;
        }

        //给容量赋值
        //int nowCount = Charactor.GetInstance.Shops[101].Occupy;
        //int allCount = Charactor.GetInstance.Shops[101].Vol;
        //Label_CountNow.GetComponent<UIText>().SetText(false, nowCount + " / " + allCount);
    }


    /// <summary>
    /// 选中商品
    /// </summary>
    /// <param name="btn"></param>
    void chooseGoods(GameObject btn)
    {
        if (choose_Goods != null)
        {
            choose_Goods.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
        }
        btn.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, true, true);
        choose_Goods = btn;

        CommonFunc.GetInstance.SetButtonState(Button_ResetPrice, true);
        CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, true);

        //给左侧道具信息赋值
        int id = int.Parse(btn.name);
        int itemID = SelectDao.GetDao().SelectBusiness_Goods(id).itemId;
        int goodCount = Dic_Goods[id].Count;
        string itemName = Constants.Items_All[itemID].name;
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_ItemIconPos.transform,Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
        go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, goodCount);
        Label_ItemName.GetComponent<UIText>().SetText(false, itemName);
        Label_ItemNum.GetComponent<UIText>().SetText(false, goodCount.ToString());

        //当前商品价格
        int Price = Dic_Goods[id].Price;
        NowPrice = Price;
        moneyMarket.GetComponent<Price_MoneyEXUI>().SetMoney(NowPrice);
        moneyNow.GetComponent<Price_MoneyEXUI>().SetMoney(NowPrice);

        NowGoodsID = id;
        UIEventListener.Get(Button_ResetPrice).onClick = ResetPrice;
        UIEventListener.Get(Button_ChangePrice).onClick = ChangePrice;

        //调整商品价格
        UIEventListener.Get(Button_Reduce).onClick = ReduceCount;
        UIEventListener.Get(Button_Add).onClick = AddCount;
        Input_GetNum.value = NowPrice.ToString();
        Input_GetNum.characterLimit = 10;
        Input_GetNum.inputType = UIInput.InputType.Standard;
        Input_GetNum.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_GetNum.onChange, SetNow_Price);
    }

    //减少数量
    void ReduceCount(GameObject btn)
    {
        Input_GetNum.value = CommonFunc.GetInstance.ChangeMoney(false, NowPrice).ToString();
        if (NowPrice <= 0)
        {
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, false);
            CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, false);
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, true);
            CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, true);
        }
    }
    //增加数量
    void AddCount(GameObject btn)
    {
        Input_GetNum.value = CommonFunc.GetInstance.ChangeMoney(true, NowPrice).ToString();
        if (NowPrice > 0)
        {
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, true);
            CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, true);
        }
    }

    /// <summary>
    /// 实时根据输入框调整当前价格显示
    /// </summary>
    void SetNow_Price()
    {
        if (Input_GetNum.value != "" && !Input_GetNum.value.Contains("-"))
        {

            int Input_Num = int.Parse(Input_GetNum.value);
            if (Input_Num > 0)
            {
                CommonFunc.GetInstance.SetButtonState(Button_Reduce, true);
                CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState(Button_Reduce, false);
                CommonFunc.GetInstance.SetButtonState(Button_ChangePrice, false);
            }
            moneyNow.GetComponent<Price_MoneyEXUI>().SetMoney(Input_Num);
            NowPrice = Input_Num;
        }
    }

    /// <summary>
    /// 重置价格
    /// </summary>
    /// <param name="btn"></param>
    void ResetPrice(GameObject btn)
    {
        //弹出确认提示
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
        go.GetComponent<SystemTipsUI>().SetTipDesc( LanguageMgr.GetInstance.GetText("Tips_System_9"), LanguageMgr.GetInstance.GetText("Tips_5"), LanguageMgr.GetInstance.GetText("Tips_7"));

    }

    /// <summary>
    /// 接收系统提示界面的按钮选择
    /// </summary>
    /// <param name="isYes"></param>
    void ReceiveSystemTips(bool isYes)
    {
        if (isYes)
        {
            int marketPrice = Dic_Goods[NowGoodsID].MarketPrice;
            moneyMarket.GetComponent<Price_MoneyEXUI>().SetMoney(marketPrice);
            NowPrice = marketPrice;
            Input_GetNum.value = NowPrice.ToString();
            moneyNow.GetComponent<Price_MoneyEXUI>().SetMoney(NowPrice);
        }
    }

    /// <summary>
    /// 改变价格
    /// </summary>
    /// <param name="btn"></param>
    void ChangePrice(GameObject btn)
    {
        moneyMarket.GetComponent<Price_MoneyEXUI>().SetMoney(NowPrice);
        Input_GetNum.value = NowPrice.ToString();
        moneyNow.GetComponent<Price_MoneyEXUI>().SetMoney(NowPrice);
    }

    /// <summary>
    /// 道具弹窗
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isHover"></param>
    void ShowTips(GameObject btn, bool isHover)
    {
        int itemID = SelectDao.GetDao().SelectBusiness_Goods(int.Parse(btn.name)).itemId;
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

    private void ClickControl()
    {
        UIEventListener.Get(Sprite_Back).onClick = Back;
        UIEventListener.Get(Button_Close).onClick = Back;
    }

    void Back(GameObject btn)
    {
        Destroy(this.gameObject);
    }


}
