// ======================================================================================
// 文 件 名 称：DealUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:31:05
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:31:05
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealUI : MonoBehaviour {
    public GameObject Label_SystemName;

    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_Pos_All;
    public GameObject Label_Title_All;

    public GameObject GameObject_ScrollList_ChooseItem;
    public GameObject GameObject_Pos_ChooseItem;
    public GameObject Label_Title_ChooseItem;
    public GameObject GameObject_MarketPos;
    public GameObject Button_Haggle;
    public GameObject Button_DoDeal;
    public GameObject GameObject_HavePos;
    public GameObject PopupList_Location;

    public GameObject GameObject_GoodsInfo;
    public GameObject GameObject_ItemIconPos;
    public GameObject Label_ItemName;
    public GameObject Label_ItemNum;
    public GameObject GameObject_MoneyPos;
    public GameObject Button_Reduce;
    public GameObject Button_Add;
    public UIInput Input_GetNum;
    public GameObject Button_Choose;
    public GameObject Sprite_GoodsBack;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    Dictionary<int, GameObject> GoodsDic;
    Dictionary<int,int> chooseDic;
    int itemCount = 999;//临时数据，需要当前存量。
    int haveMoney = 0;//玩家拥有金钱

    GameObject moneyAll;
    GameObject moneyHave;
    int ItemPrice = 0;//道具的金钱

    GameObject NowMoney;
    GameObject ItemEX;
    int GoodsCount = 0;

    bool isEnough_Money;//金钱是否充足 
    bool isEnough_Bag;//背包空间是否充足 

    void Awake()
    {
        GoodsDic = new Dictionary<int, GameObject>();
        chooseDic = new Dictionary<int, int>();

        haveMoney = Bag.GetInstance.money;

        //当前玩家拥有的金钱
        moneyHave = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_HavePos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyHave.GetComponent<Price_MoneyEXUI>().SetMoney(haveMoney);
        //选择商品总价
        moneyAll = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_MarketPos.transform, Vector3.zero, Vector3.one).gameObject;
        moneyAll.GetComponent<Price_MoneyEXUI>().SetMoney(0);

        NowMoney = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one).gameObject;

        ItemEX = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_ItemIconPos.transform, Vector3.zero, Vector3.one).gameObject;
    }

    void Start()
    {
        GameObject_GoodsInfo.SetActive(false);
        CommonFunc.GetInstance.SetButtonState(Button_Haggle, false);
        CommonFunc.GetInstance.SetButtonState(Button_DoDeal, false);

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_ScrollList_All.GetComponent<UIPanel>().depth + 1;

        ClickControl();
    }


    void Update()
    {

    }


    /// <summary>
    /// 显示交易货物
    /// </summary>
    /// <param name="list_Goods">货物ITEM的ID列表</param>
    public void GoodsList(List<int> list_Goods)
    {     
        //在列表中生成道具ICON
        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        foreach (var item in list_Goods)
        {
            if (row % 6 == 0)
            {
                row = 0;
                col++;
            }
            int itemID = SelectDao.GetDao().SelectBusiness_Goods(item).itemId;
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_All.transform, new Vector3(0 + col * (-65), 0 + row * (-65), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, itemCount);
            go.name = item.ToString();
            CommonFunc.GetInstance.LableColor(go.GetComponentInChildren<UILabel>());
            GoodsDic.Add(item, go);

            UIEventListener.Get(go).onClick = chooseGoods;
            UIEventListener.Get(go).onHover = ShowTips;
            row++;
        }

        RefreshChooseList();
    }

    /// <summary>
    /// 弹出选择商品数量界面
    /// </summary>
    /// <param name="btn"></param>
    void chooseGoods(GameObject btn)
    {
        GameObject_GoodsInfo.SetActive(true);
        GameObject_GoodsInfo.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 5;

        int itemID = SelectDao.GetDao().SelectBusiness_Goods(int.Parse(btn.name)).itemId;
        ItemEX.GetComponent<Botton_ItemEXUI>().Show_Part(true,false,false,false);
        ItemEX.GetComponent<Botton_ItemEXUI>().SetItem(itemID, 0);
        Sprite_GoodsBack.name = btn.name;
        
        GoodsCount = int.Parse(btn.GetComponentInChildren<UILabel>().text);
        Item item = Constants.Items_All[itemID];
        Label_ItemName.GetComponent<UIText>().SetText(false, item.name);
        Label_ItemNum.GetComponent<UIText>().SetText(false, GoodsCount.ToString());
        //Sprite_Trend.transform.Rotate(Vector3.down);
        //Sprite_Trend.GetComponent<UISprite>().color = Color.green;
        //float TrendNum = 0.25f;
        //Label_TrendNum.GetComponent<UIText>().SetText(false, (TrendNum*100).ToString() + "%");

        //临时价格，需要根据商品基础价格—波动后—地区改变—店铺改变—其他影响计算后得出。
        ItemPrice = SelectDao.GetDao().SelectBusiness_Goods(int.Parse(btn.name)).normalPrice;
        //输入初始化
        Input_GetNum.value = 0.ToString();
        Input_GetNum.characterLimit = 10;
        Input_GetNum.inputType = UIInput.InputType.Standard;
        Input_GetNum.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_GetNum.onChange, SetNow);

        SetNow();

        //判断商品数量是否大于0
        if (GoodsCount > 0)
        {
            Button_Reduce.name = btn.name;
            Button_Add.name = btn.name;
            UIEventListener.Get(Button_Reduce).onClick = ReduceCount;
            UIEventListener.Get(Button_Add).onClick = AddCount;
            Button_Choose.name = btn.name;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState(Button_Add, false);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, false);
        }

        UIEventListener.Get(Sprite_GoodsBack).onClick = GoodsBack;        
    }
    //减少数量
    void ReduceCount(GameObject btn)
    {
        int Input_Num = int.Parse(Input_GetNum.value);
        if (Input_Num <= 0)
        {
            Input_GetNum.value = 0.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, false);
            CommonFunc.GetInstance.SetButtonState(Button_Choose, false);
        }
        else
        {
            Input_Num = Input_Num - 1;
            Input_GetNum.value = Input_Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add, true);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce,true);
            CommonFunc.GetInstance.SetButtonState(Button_Choose, true);
            UIEventListener.Get(Button_Choose).onClick = ChooseItem;
        }
        SetNow();
    }
    //增加数量
    void AddCount(GameObject btn)
    {
        int Input_Num = int.Parse(Input_GetNum.value);
        if (Input_Num >= itemCount)
        {
            Input_GetNum.value = itemCount.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add, false);
        }
        else if (Input_Num >= 0)
        {
            Input_Num = Input_Num + 1;
            Input_GetNum.value = Input_Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add, true);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce, true);
            CommonFunc.GetInstance.SetButtonState(Button_Choose, true);
            UIEventListener.Get(Button_Choose).onClick = ChooseItem;
        }
        SetNow();
    }

    /// <summary>
    /// 根据公式计算当前的数值
    /// </summary>
    void SetNow()
    {
        if (Input_GetNum.value != "" && !Input_GetNum.value.Contains("-"))
        {
            int Input_Num = int.Parse(Input_GetNum.value);
            if (Input_Num > GoodsCount)
            {
                Input_GetNum.value = GoodsCount.ToString();
            }
            else if (Input_Num <= 0)
            {
                Input_GetNum.value = 0.ToString();
            }
            if (Input_Num>0)
            {
                CommonFunc.GetInstance.SetButtonState(Button_Choose, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState(Button_Choose, false);
            }
            int itemPrice = ItemPrice * int.Parse(Input_GetNum.value);
            NowMoney.GetComponent<Price_MoneyEXUI>().SetMoney(itemPrice);
        }
    }

    //选中物品
    void ChooseItem(GameObject btn)
    {
        GameObject_GoodsInfo.SetActive(false);

        //判断是添加新的商品，还是添加已经选择商品的数量
        int itemID = int.Parse(btn.name);
        int Count = int.Parse(Input_GetNum.value);

        int nowCount = int.Parse(GoodsDic[itemID].GetComponentInChildren<UILabel>().text);
        if (chooseDic.ContainsKey(itemID))
        {
            nowCount -= Count;
            GoodsDic[itemID].GetComponentInChildren<UILabel>().text = nowCount.ToString();
            CommonFunc.GetInstance.LableColor(GoodsDic[itemID].GetComponentInChildren<UILabel>());

            chooseDic[itemID] += Count;
        }
        else
        {
            nowCount -= Count;
            GoodsDic[itemID].GetComponentInChildren<UILabel>().text = nowCount.ToString();
            CommonFunc.GetInstance.LableColor(GoodsDic[itemID].GetComponentInChildren<UILabel>());

            chooseDic.Add(itemID, Count);
        }
        RefreshChooseList();
    }

    /// <summary>
    /// 刷新选中列表
    /// </summary>
    int RefreshChooseList()
    {
        NGUITools.DestroyChildren(GameObject_Pos_ChooseItem.transform);
        int MoneyAll = 0;

        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        //根据DIC中的数据生成选中的物品的列表
        foreach (var item in chooseDic)
        {
            if (row % 4 == 0)
            {
                row = 0;
                col++;
            }
            int itemID = SelectDao.GetDao().SelectBusiness_Goods(item.Key).itemId;

            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_ChooseItem.transform, new Vector3(0 + col * (-65), 0 + row * (-65), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, item.Value);
            go.name = item.Key.ToString();
            row++;

            //临时价格，需要根据商品基础价格—波动后—地区改变—店铺改变—其他影响计算后得出。
            int itemPrice = SelectDao.GetDao().SelectBusiness_Goods(item.Key).normalPrice * item.Value;
            MoneyAll += itemPrice;

            UIEventListener.Get(go).onClick = deleteChoose;
            UIEventListener.Get(go).onHover = ShowTips;
        }

        if (MoneyAll > 0)
        {
            moneyAll.GetComponent<Price_MoneyEXUI>().SetMoney(MoneyAll);


            //判断总价格是否超过当前玩家资金总量
            if (MoneyAll > haveMoney)
            {
                moneyAll.GetComponent<Price_MoneyEXUI>().SetMoneyColor(false);
            }
            else
            {
                moneyAll.GetComponent<Price_MoneyEXUI>().SetMoneyColor(true);
            }

            //判断其负重和大小是否超过玩家存放点（背包和仓库）的当前容量

            CommonFunc.GetInstance.SetButtonState(Button_Haggle, true);
            CommonFunc.GetInstance.SetButtonState(Button_DoDeal, true);

            UIEventListener.Get(Button_Haggle).onClick = OpenHaggle;
            UIEventListener.Get(Button_DoDeal).onClick = DoDeal;

            return MoneyAll;
        }
        else
        {
            moneyAll.GetComponent<Price_MoneyEXUI>().SetMoney(0);

            CommonFunc.GetInstance.SetButtonState(Button_Haggle, false);
            CommonFunc.GetInstance.SetButtonState(Button_DoDeal, false);

            return 0;
        }
    }

    /// <summary>
    /// 从列表和DIC中删除点击对象
    /// </summary>
    /// <param name="btn"></param>
    void deleteChoose(GameObject btn)
    {
        int itemID = int.Parse(btn.name);
        chooseDic.Remove(itemID);

        int Count = int.Parse(btn.GetComponentInChildren<UILabel>().text);
        int nowCount = int.Parse(GoodsDic[itemID].GetComponentInChildren<UILabel>().text);
        nowCount += Count;
        GoodsDic[itemID].GetComponentInChildren<UILabel>().text = nowCount.ToString();


        RefreshChooseList();
    }

    /// <summary>
    /// 开启还价界面
    /// </summary>
    /// <param name="btn"></param>
    void OpenHaggle(GameObject btn)
    {
        //获得商店负责人的角色ID

        //给角色头像、角色名称、性格、与自己关系赋值

        //给还价界面传值
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Haggle, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<HaggleUI>().Open(RefreshChooseList());
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;

    }

    /// <summary>
    /// 完成交易
    /// </summary>
    /// <param name="btn"></param>
    void DoDeal(GameObject btn)
    {
        int All_Count = 0;
        foreach (var item in chooseDic)
        {
            All_Count += chooseDic[item.Key];
        }
        //计算存放地剩余空间
        if (Bag.GetInstance.IsAhead_OverCount(All_Count))
        {
            isEnough_Bag = true;
        }
        else
        {
            isEnough_Bag = false;
        }
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
        if (!isEnough_Bag)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_12"));
        }
        else if (!isEnough_Money)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, "System_1", LanguageMgr.GetInstance.GetText("Tips_System_20"));
        }
        else
        {
            //扣除玩家金钱

            //添加道具进入目标地点

            //商店中减少对应道具数量，如果数量为0,则提示缺货

        }


    }

    //显示道具tips
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

    void GoodsBack(GameObject btn)
    {
        GameObject_GoodsInfo.SetActive(false);
    }

    void Back(GameObject btn)
    {
        chooseDic.Clear();
        GoodsDic.Clear();
        Destroy(ItemTip);
        Destroy(this.gameObject);
    }

}
