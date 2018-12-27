// ======================================================================================
// 文 件 名 称：DealChooseUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:01:25
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:01:25
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealChooseUI : MonoBehaviour {
    public GameObject Label_Title_All;

    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_Pos_All;
    public GameObject Button_Already;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    List<GameObject> goodsList;

    void Awake()
    {
        goodsList = new List<GameObject>();
    }

    void Start()
    {

        CommonFunc.GetInstance.SetUIPanel(gameObject);

        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_ScrollList_All.GetComponent<UIPanel>().depth + 1;


        GoodsList(1);//test
        ClickControl();
    }


    void Update()
    {

    }


    /// <summary>
    /// 显示牙行货物，临时数据
    /// </summary>
    /// <param name="BrokeID"></param>
    public void GoodsList(int BrokeID)
    {
        ////设置按钮初始状态为无效
        //CommonFunc.GetInstance.SetButtonState(Button_Already, false);
        ////获得牙行商品种类
        //int Brole_TypeCount = SelectDao.GetDao().SelectBusiness_Broker(BrokeID).type_num;
        ////获得总的商品种类
        //string[] All_TypeCount_s = SelectDao.GetDao().SelectSystem_Config(65).value.Split(';');
        //List<int> All_TypeCount = new List<int>();
        //foreach (string item in All_TypeCount_s)
        //{
        //    All_TypeCount.Add(int.Parse(item));
        //}
        ////创建N个随机数作为索引值，添加到新的list中
        //List<int> list_Random = new List<int>();
        //for (int i = 0; i < Brole_TypeCount; i++)
        //{
        //    list_Random.Add(All_TypeCount[Random.Range(0, All_TypeCount.Count)]);
        //}
        ////根据获得的随机商品类型获得所有商品
        //List<int> list_Goods = new List<int>();
        //foreach (int item in list_Random)
        //{
        //    List<Business_Goods> list_goodsClass = SelectDao.GetDao().SelectBusiness_GoodsByType(item);
        //    foreach (Business_Goods good in list_goodsClass)
        //    {
        //        list_Goods.Add(good.id);
        //    }
        //}
        ////在列表中生成道具ICON
        //int row = 0;//行，Y轴
        //int col = -1;//列，X轴
        //foreach (int item in list_Goods)
        //{
        //    if (row % 5 == 0)
        //    {
        //        row = 0;
        //        col++;
        //    }
        //    int itemID = SelectDao.GetDao().SelectBusiness_Goods(item).itemId;
        //    GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_All.transform, new Vector3(0 + col * (-65), 0 + row * (-65), 0), Vector3.one).gameObject;
        //    go.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
        //    go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, 0);
        //    go.name = item.ToString();

        //    UIEventListener.Get(go).onClick = chooseGoods;
        //    UIEventListener.Get(go).onHover = ShowTips;
        //    row++;
        //}
    }

    /// <summary>
    /// 选中商品
    /// </summary>
    /// <param name="btn"></param>
    void chooseGoods(GameObject btn)
    {
        //选中状态
        if (goodsList.Contains(btn))
        {
            goodsList.Remove(btn);
            btn.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, true);
        }
        else
        {
            goodsList.Add(btn);
            btn.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, true, true);
        }
        //根据选中数量按钮状态
        if (goodsList.Count>0)
        {
            CommonFunc.GetInstance.SetButtonState(Button_Already, true);
            Button_Already.name = btn.name;
            UIEventListener.Get(Button_Already).onClick = DoDeal;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState(Button_Already, true);
        }
    }

    /// <summary>
    /// 打开交易界面
    /// </summary>
    /// <param name="btn"></param>
    void DoDeal(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Deal,FindObjectOfType<UIRoot>().transform,Vector3.zero,Vector3.one).gameObject;
        List<int> id_list = new List<int>();
        foreach (var item in goodsList)
        {
            id_list.Add(int.Parse(item.name));
        }
        go.GetComponent<DealUI>().GoodsList(id_list);
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
        CommonFunc.GetInstance.SetUIPanel(go);
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
        Destroy(ItemTip);
        Destroy(this.gameObject);
    }


}
