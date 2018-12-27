// ======================================================================================
// 文 件 名 称：MarketInfoUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 21:51:42
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 21:51:42
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInfoUI : MonoBehaviour {
    public GameObject Label_SystemName;

    public GameObject GameObject_ChooseGood;
    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_Pos_All;
    public GameObject Button_AskWay;
    public GameObject Button_AskPrice;

    public GameObject GameObject_Way;
    public GameObject GameObject_ScrollList_Way;
    public GameObject GameObject_Pos_Way;
    public GameObject Button_WayEx;
    public GameObject Button_AddNews_Way;
    public GameObject Button_Back_Way;

    public GameObject GameObject_Price;
    public GameObject GameObject_ScrollList_Price;
    public GameObject GameObject_Pos_Price;
    public GameObject Button_PriceEx;
    public GameObject Button_AddNews_Price;
    public GameObject Button_Back_Price;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    
    List<GameObject> ChooseList_Price;

    GameObject choose_Goods;

    void Awake()
    {
        ChooseList_Price = new List<GameObject>();
    }

    void Start()
    {
        //设置初始界面内容
        GameObject_ChooseGood.SetActive(true);
        GameObject_Way.SetActive(false);
        GameObject_Price.SetActive(false);
        

        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        ItemTip.GetComponent<UIPanel>().depth = GameObject_ScrollList_All.GetComponent<UIPanel>().depth + 1;
        
        GoodsList(1);//test

        ClickControl();
    }


    void Update()
    {

    }

    /// <summary>
    /// 显示牙行货物
    /// </summary>
    /// <param name="BrokeID"></param>
    public void GoodsList(int BrokeID)
    {
        //设置按钮初始状态为无效
        CommonFunc.GetInstance.SetButtonState(Button_AskWay, false);
        CommonFunc.GetInstance.SetButtonState(Button_AskWay, false);
        //获得牙行商品种类
        int Brole_TypeCount = SelectDao.GetDao().SelectBusiness_Broker(BrokeID).type_num;
        //获得总的商品种类
        //string[] All_TypeCount_s = SelectDao.GetDao().SelectSystem_Config(65).value.Split(';');
        List<int> All_TypeCount = new List<int>();
        //foreach (string item in All_TypeCount_s)
        //{
        //    All_TypeCount.Add(int.Parse(item));
        //}
        //创建N个随机数作为索引值，添加到新的list中
        List<int> list_Random = new List<int>();
        for (int i = 0; i < Brole_TypeCount; i++)
        {
            list_Random.Add(All_TypeCount[Random.Range(0, All_TypeCount.Count)]);
        }
        //根据获得的随机商品类型获得所有商品
        List<int> list_Goods = new List<int>();
        foreach (int item in list_Random)
        {
            List<Business_Goods> list_goodsClass = SelectDao.GetDao().SelectBusiness_GoodsByType(item);
            foreach (Business_Goods good in list_goodsClass)
            {
                list_Goods.Add(good.id);
            }
        }
        //在列表中生成道具ICON
        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        foreach (int item in list_Goods)
        {
            if (row % 5 == 0)
            {
                row = 0;
                col++;
            }
            int itemID = SelectDao.GetDao().SelectBusiness_Goods(item).itemId;
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_All.transform,new Vector3(0 + col * (-65),0 +row * (-65), 0),Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true,false,false,false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, 0);
            go.name = item.ToString();

            UIEventListener.Get(go).onClick = chooseGoods;
            UIEventListener.Get(go).onHover = ShowTips;
            row++;
        }
    }

    /// <summary>
    /// 选中商品
    /// </summary>
    /// <param name="btn"></param>
    void chooseGoods(GameObject btn)
    {
        if (choose_Goods != null)
        {
            choose_Goods.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
        }
        btn.GetComponent<Botton_ItemEXUI>().Show_Part(true,false,true,true);
        choose_Goods = btn;

        CommonFunc.GetInstance.SetButtonState(Button_AskWay, true);
        Button_AskWay.name = btn.name;
        CommonFunc.GetInstance.SetButtonState(Button_AskPrice, true);
        Button_AskPrice.name = btn.name;
        UIEventListener.Get(Button_AskWay).onClick = ShowWay;
        UIEventListener.Get(Button_AskPrice).onClick = ShowPrice;
    }

    /// <summary>
    /// 显示获得途径界面
    /// </summary>
    /// <param name="btn"></param>
    void ShowWay(GameObject btn)
    {
        GameObject_ChooseGood.SetActive(false);
        GameObject_Price.SetActive(false);
        GameObject_Way.SetActive(true);
        
        CommonFunc.GetInstance.SetButtonState(Button_AddNews_Way, false);

        //获得商品类型
        int GoodsType = SelectDao.GetDao().SelectBusiness_Goods(int.Parse(btn.name)).type;
        //获得全部牙行列表

        //获得符合条件的牙行列表

        UIEventListener.Get(Button_AddNews_Way).onClick = AddNews;
        UIEventListener.Get(Button_Back_Way).onClick = BackMain;
    }


    /// <summary>
    /// 显示价格行情界面
    /// </summary>
    /// <param name="btn"></param>
    void ShowPrice(GameObject btn)
    {
        GameObject_ChooseGood.SetActive(false);
        GameObject_Way.SetActive(false);
        GameObject_Price.SetActive(true);
        Button_PriceEx.SetActive(true);
        
        CommonFunc.GetInstance.SetButtonState(Button_AddNews_Price, false);


        NGUITools.DestroyChildren(GameObject_Pos_Price.transform);

        ChooseList_Price.Clear();
        int GoodsID = int.Parse(btn.name);

        //获得商品基础价格（暂时，后改为随机后保存的价格，每月更换）
        string[] limit = SelectDao.GetDao().SelectBusiness_Goods(GoodsID).floatRange.Split(';');
        int GoodsPrice = Random.Range(int.Parse(limit[0]), int.Parse(limit[1]));
        int count = 0;
        foreach (var item in SelectDao.GetDao().SelectBusiness_AreaByType(1))
        {
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_PriceEx.transform, GameObject_Pos_Price.transform,new Vector3(0,-33* count,0),Vector3.one).gameObject;
            go.name = GoodsID.ToString();
            foreach (Transform child in go.GetComponentsInChildren<Transform>())
            {
                if (child.name == "Label_Place")
                {
                    child.GetComponent<UIText>().SetText(false, item.name);
                }
                if (child.name == "Label_Trend")
                {
                    
                }
                if (child.name == "Sprite_Up")
                {
                    
                }
                if (child.name == "Sprite_Down")
                {
                    
                }
                if (child.name == "Sprite_Choose")
                {
                    child.GetComponent<UISprite>().enabled = false;
                    ChooseList_Price.Add(child.gameObject);
                }
                if (child.name == "GameObject_MoneyPos")
                {
                    GameObject money = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX,child.transform,Vector3.zero,Vector3.one).gameObject;
                    money.GetComponent<Price_MoneyEXUI>().SetMoney((int)(GoodsPrice + (float)GoodsPrice * (float)item.ya_price_num / 100f));
                }
            }
            count++;
            UIEventListener.Get(go).onClick = ChoosePrice;
        }
        Button_PriceEx.SetActive(false);

        UIEventListener.Get(Button_Back_Price).onClick = BackMain;
    }
    /// <summary>
    /// 选中某一项
    /// </summary>
    /// <param name="btn"></param>
    void ChoosePrice(GameObject btn)
    {
        foreach (var item in ChooseList_Price)
        {
            item.GetComponent<UISprite>().enabled = false;
        }
        foreach (Transform child in btn.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Sprite_Choose")
            {
                child.GetComponent<UISprite>().enabled = true;
            }
        }
        CommonFunc.GetInstance.SetButtonState(Button_AddNews_Price, true);
        Button_AddNews_Price.name = btn.name;
        UIEventListener.Get(Button_AddNews_Price).onClick = AddNews;
    }

    /// <summary>
    /// 添加新消息
    /// </summary>
    /// <param name="btn"></param>
    void AddNews(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_NewTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        int goodID = int.Parse(btn.name);
        string title = LanguageMgr.GetInstance.GetText("Tips_Title_1");
        string des = LanguageMgr.GetInstance.GetText("Tips_Title_7") + SelectDao.GetDao().SelectBusiness_Goods(goodID).productName;
        go.GetComponent<NewTipsUI>().SetNew(null,title, des);
    }

    void ShowTips(GameObject btn,bool isHover)
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
        Destroy(gameObject);
    }
    void BackMain(GameObject btn)
    {
        GameObject_ChooseGood.SetActive(true);
        GameObject_Way.SetActive(false);
        GameObject_Price.SetActive(false);        
    }

}
