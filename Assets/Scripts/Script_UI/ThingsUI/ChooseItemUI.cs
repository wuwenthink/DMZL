// ======================================================================================
// 文 件 名 称：ChooseItemUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-31 17:42:00
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-31 17:42:00
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections.Generic;
using UnityEngine;

public class ChooseItemUI : MonoBehaviour
{

    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_Pos_All;
    public GameObject Button_Location;
    public GameObject Button_ChooseMoney;
    public GameObject GameObject_MoneyPos;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    public GameObject GameObject_ChooseItem;
    public GameObject Sprite_Back_Item;
    public GameObject GameObject_ItemIconPos;
    public GameObject Label_ItemName;
    public GameObject Label_ItemNum;
    public UIInput Input_GetNum_Item;
    public GameObject Button_Reduce_Item;
    public GameObject Button_Add_Item;
    public GameObject Button_OK_Item;

    public GameObject GameObject_ChooseMoney;
    public GameObject Sprite_Back_Money;
    public GameObject GameObject_HavePos;
    public GameObject GameObject_NowPos;
    public UIInput Input_GetNum_Money;
    public GameObject Button_Reduce_Money;
    public GameObject Button_Add_Money;
    public GameObject Button_OK_Money;

    GameObject ItemTip;
    GameObject ItemEX;
    int itemID = 0;
    int itemCount = 0;
    int chooseItemCount = 0;
    int chooseMoneyCount = 0;
    Dictionary<int, ItemInBag> Dic_Goods;
    GameObject moneyChoose;
    GameObject moneyHave;
    int haveMoney = 0;//玩家拥有金钱
    void Awake ()
    {
        Dic_Goods = Bag.GetInstance.itemDic;

        haveMoney = Bag.GetInstance.money;
    }

    void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel (gameObject);

        ItemTip = CommonFunc.GetInstance.Ins_ItemTips (ItemTip);
        ItemTip.GetComponent<UIPanel> ().depth = GameObject_ScrollList_All.GetComponent<UIPanel> ().depth + 1;

        ItemEX = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, GameObject_ItemIconPos.transform, Vector3.zero, Vector3.one).gameObject;
        //当前玩家拥有的金钱
        moneyHave = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_HavePos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyHave.GetComponent<Price_MoneyEXUI> ().SetMoney (haveMoney);
        //选择商品总价
        moneyChoose = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_NowPos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyChoose.GetComponent<Price_MoneyEXUI> ().SetMoney (chooseMoneyCount);

        GameObject_ChooseItem.SetActive (false);
        GameObject_ChooseMoney.SetActive (false);

        ClickControl ();

        GoodsList ();
    }


    void Update ()
    {

    }

    /// <summary>
    /// 显示可选物品
    /// </summary>
    public void GoodsList ()
    {
        //在列表中生成道具ICON
        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        foreach (var item in Dic_Goods)
        {
            if (row % 4 == 0)
            {
                row = 0;
                col++;
            }
            GameObject go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, GameObject_Pos_All.transform, new Vector3 (0 + col * (-77), 0 + row * (-77), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI> ().Show_Part (true, true, false, false);
            go.GetComponent<Botton_ItemEXUI> ().SetItem (item.Key, item.Value.count);
            go.name = item.Key.ToString ();

            UIEventListener.Get (go).onClick = chooseItem;
            UIEventListener.Get (go).onHover = ShowTips;
            row++;
        }


        //显示金钱
        GameObject moneyMarket = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyMarket.GetComponent<Price_MoneyEXUI> ().SetMoney (haveMoney);
        UIEventListener.Get (Button_ChooseMoney).onClick = chooseMoney;
    }


    // 选择商品打开选择数量界面
    void chooseItem (GameObject btn)
    {
        GameObject_ChooseItem.SetActive (true);
        CommonFunc.GetInstance.SetButtonState (Button_Add_Item, true);
        CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, true);
        CommonFunc.GetInstance.SetButtonState (Button_OK_Item, true);
        GameObject_ChooseItem.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 5;

        itemID = int.Parse (btn.name);
        ItemEX.GetComponent<Botton_ItemEXUI> ().Show_Part (true, false, false, false);
        ItemEX.GetComponent<Botton_ItemEXUI> ().SetItem (itemID, 0);

        itemCount = Dic_Goods [itemID].count;
        Item item = Constants.Items_All [itemID];
        Label_ItemName.GetComponent<UIText> ().SetText (false, item.name);
        Label_ItemNum.GetComponent<UIText> ().SetText (false, itemCount.ToString ());

        //输入初始化
        Input_GetNum_Item.value = 0.ToString ();
        Input_GetNum_Item.characterLimit = 10;
        Input_GetNum_Item.inputType = UIInput.InputType.Standard;
        Input_GetNum_Item.validation = UIInput.Validation.Integer;
        EventDelegate.Add (Input_GetNum_Item.onChange, SetNow_Item);

        SetNow_Item ();

        //判断商品数量是否大于0
        if (itemCount > 0)
        {
            Button_Reduce_Item.name = btn.name;
            Button_Add_Item.name = btn.name;
            UIEventListener.Get (Button_Reduce_Item).onClick = ReduceCount_Item;
            UIEventListener.Get (Button_Add_Item).onClick = AddCount_Item;
            Button_OK_Item.name = btn.name;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Add_Item, false);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, false);
        }

        UIEventListener.Get (Button_OK_Item).onClick = chooseItemFinished;
    }

    //减少物品数量
    void ReduceCount_Item (GameObject btn)
    {
        int Input_Num = int.Parse (Input_GetNum_Item.value);
        if (Input_Num <= 0)
        {
            Input_GetNum_Item.value = 0.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, false);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Item, false);
        }
        else
        {
            Input_Num = Input_Num - 1;
            Input_GetNum_Item.value = Input_Num.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add_Item, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, true);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Item, true);
            UIEventListener.Get (Button_OK_Item).onClick = chooseItemFinished;
        }
        chooseItemCount = int.Parse (Input_GetNum_Item.value);
    }

    //增加物品数量
    void AddCount_Item (GameObject btn)
    {
        int Input_Num = int.Parse (Input_GetNum_Item.value);
        if (Input_Num >= itemCount)
        {
            Input_GetNum_Item.value = itemCount.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add_Item, false);
        }
        else if (Input_Num >= 0)
        {
            Input_Num = Input_Num + 1;
            Input_GetNum_Item.value = Input_Num.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add_Item, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, true);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Item, true);
        }
        chooseItemCount = int.Parse (Input_GetNum_Item.value);
    }
    //更新输入数据
    void SetNow_Item ()
    {
        if (Input_GetNum_Item.value != "" && !Input_GetNum_Item.value.Contains ("-"))
        {
            int Input_Num = int.Parse (Input_GetNum_Item.value);
            if (Input_Num > itemCount)
            {
                Input_GetNum_Item.value = itemCount.ToString ();
            }
            else if (Input_Num <= 0)
            {
                Input_GetNum_Item.value = 0.ToString ();
            }
            if (Input_Num > 0)
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, true);
                CommonFunc.GetInstance.SetButtonState (Button_OK_Item, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce_Item, false);
                CommonFunc.GetInstance.SetButtonState (Button_OK_Item, false);
            }
        }
        chooseItemCount = int.Parse (Input_GetNum_Item.value);
    }

    //打开选择金钱数量界面
    void chooseMoney (GameObject btn)
    {
        GameObject_ChooseMoney.SetActive (true);
        CommonFunc.GetInstance.SetButtonState (Button_Add_Money, true);
        CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, true);
        CommonFunc.GetInstance.SetButtonState (Button_OK_Money, true);
        GameObject_ChooseMoney.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 5;

        //输入初始化
        Input_GetNum_Money.value = haveMoney.ToString ();
        Input_GetNum_Money.characterLimit = 10;
        Input_GetNum_Money.inputType = UIInput.InputType.Standard;
        Input_GetNum_Money.validation = UIInput.Validation.Integer;
        EventDelegate.Add (Input_GetNum_Money.onChange, SetNow_Money);

        moneyHave.GetComponent<Price_MoneyEXUI> ().SetMoney (haveMoney);
        moneyChoose.GetComponent<Price_MoneyEXUI> ().SetMoney (chooseMoneyCount);

        SetNow_Item ();
        SetNow_Money ();

        //判断商品数量是否大于0
        if (haveMoney > 0)
        {
            Button_Reduce_Item.name = btn.name;
            Button_Add_Item.name = btn.name;
            UIEventListener.Get (Button_Reduce_Money).onClick = ReduceCount_Money;
            UIEventListener.Get (Button_Add_Money).onClick = AddCount_Money;
            Button_OK_Money.name = btn.name;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Add_Money, false);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, false);
        }

        UIEventListener.Get (Button_OK_Money).onClick = chooseMoneyFinished;
    }

    //减少数量
    void ReduceCount_Money (GameObject btn)
    {
        chooseMoneyCount = CommonFunc.GetInstance.ChangeMoney (false, chooseMoneyCount);
        Input_GetNum_Money.value = chooseMoneyCount.ToString ();
        if (chooseMoneyCount <= 0)
        {
            chooseMoneyCount = 0;
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, false);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Money, false);
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, true);
            CommonFunc.GetInstance.SetButtonState (Button_Add_Money, true);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Money, true);
        }
    }
    //增加数量
    void AddCount_Money (GameObject btn)
    {
        chooseMoneyCount = CommonFunc.GetInstance.ChangeMoney (true, chooseMoneyCount);
        if (chooseMoneyCount >= haveMoney)
        {
            chooseMoneyCount = haveMoney;
            CommonFunc.GetInstance.SetButtonState (Button_Add_Money, false);
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, true);
            CommonFunc.GetInstance.SetButtonState (Button_Add_Money, true);
            CommonFunc.GetInstance.SetButtonState (Button_OK_Money, true);
        }
        Input_GetNum_Money.value = chooseMoneyCount.ToString ();
    }

    /// <summary>
    /// 实时根据输入框调整当前价格显示
    /// </summary>
    void SetNow_Money ()
    {
        if (Input_GetNum_Money.value != "" && !Input_GetNum_Money.value.Contains ("-"))
        {
            int Input_Num = int.Parse (Input_GetNum_Money.value);

            if (Input_Num > 0)
            {
                if (Input_Num > haveMoney)
                {
                    Input_Num = haveMoney;
                    Input_GetNum_Money.value = haveMoney.ToString ();
                }
                CommonFunc.GetInstance.SetButtonState (Button_Add_Money, true);
                CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, true);
                CommonFunc.GetInstance.SetButtonState (Button_OK_Money, true);
            }
            else
            {
                Input_Num = haveMoney = 0;
                Input_GetNum_Money.value = 0.ToString ();
                CommonFunc.GetInstance.SetButtonState (Button_Add_Money, false);
                CommonFunc.GetInstance.SetButtonState (Button_Reduce_Money, false);
                CommonFunc.GetInstance.SetButtonState (Button_OK_Money, false);
            }
            moneyChoose.GetComponent<Price_MoneyEXUI> ().SetMoney (Input_Num);
            chooseMoneyCount = Input_Num;
        }
    }

    /// <summary>
    /// 道具弹窗
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isHover"></param>
    void ShowTips (GameObject btn, bool isHover)
    {
        int itemID = int.Parse (btn.name);
        if (isHover)
        {
            ItemTip.SetActive (true);
            ItemTip.GetComponent<ItemTipsUI> ().itemTipsSet (itemID, false);
        }
        else
        {
            ItemTip.SetActive (false);
        }
    }

    //选择完成：道具
    void chooseItemFinished (GameObject btn)
    {
        //发送道具ID，数量
        string news = itemID.ToString () + ";" + chooseItemCount.ToString ();
        FindObjectOfType<UIRoot> ().gameObject.BroadcastMessage ("ReceiveChooseItem", news);
        Back (Sprite_Back);
    }

    //选择完成：金钱
    void chooseMoneyFinished (GameObject btn)
    {
        //发送选择金钱数量
        FindObjectOfType<UIRoot> ().gameObject.BroadcastMessage ("ReceiveChooseMoney", chooseMoneyCount);
        Back (Sprite_Back);
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Sprite_Back_Item).onClick = itemBack;
        UIEventListener.Get (Sprite_Back_Money).onClick = moneyBack;
    }


    void itemBack (GameObject btn)
    {
        GameObject_ChooseItem.SetActive (false);
    }

    void moneyBack (GameObject btn)
    {
        GameObject_ChooseMoney.SetActive (false);
    }


    void Back (GameObject btn)
    {
        Destroy (this.gameObject);
        Destroy (ItemTip);
    }

}
