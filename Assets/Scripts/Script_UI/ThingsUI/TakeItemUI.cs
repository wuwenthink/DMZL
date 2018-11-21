// ======================================================================================
// 文 件 名 称：TakeItemUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-18 22:53:04
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-18 22:53:04
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemUI : MonoBehaviour {

    public UIText Label_SystemName;
    public UIText Label_Title_All;
    public UIText Label_Title_ChooseItem;
    public UIText Label_Take;

    public GameObject GameObject_ScrollList_All;
    public GameObject GameObject_Pos_All;
    public GameObject GameObject_ScrollList_ChooseItem;
    public GameObject GameObject_Pos_ChooseItem;

    public GameObject GameObject_ChooseItem;
    public GameObject Sprite_Back_Item;
    public GameObject GameObject_ItemIconPos;
    public GameObject Label_ItemName;
    public GameObject Label_ItemNum;
    public UIInput Input_GetNum_Item;
    public GameObject Button_Reduce_Item;
    public GameObject Button_Add_Item;
    public GameObject Button_OK_Item;

    public UIText Label_Limit;
    public GameObject Button_Take;
    public GameObject Button_Close;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    GameObject ItemEX;
    Dictionary<int, int> Dic_Right;
    Dictionary<int, int> chooseDic;
    Dictionary<int, GameObject> Dic_Item;
    int itemID = 0;
    int itemCount = 0;
    int chooseItemCount = 0;

    bool isIn_s;
    int type_s;

    int NowCount = 0;
    int AllCount = 0;

    public int warehouseID;//接触后赋值，仓库ID
    public int saveToolID;//接触后赋值，存储器具ID

    RunTime_Data runTime;
    void Awake()
    {
        chooseDic = new Dictionary<int, int>();
        Dic_Item = new Dictionary<int, GameObject>();

        CommonFunc.GetInstance.SetUIPanel(gameObject);
        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = gameObject.GetComponent<UIPanel>().depth + 10;
    }

    void Start () {
        GameObject_ChooseItem.SetActive(false);
        CommonFunc.GetInstance.SetButtonState(Button_Take, false);
        UIEventListener.Get(Button_Take).onClick = TakeItem;

        ItemEX = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_ItemIconPos.transform, Vector3.zero, Vector3.one).gameObject;

        Open(false, 0);//Test
    }
	

	void Update () {
		
	}

    /// <summary>
    /// 打开界面，传输类型和具体参数
    /// </summary>
    /// <param name="isIn">true是放入，false是拿出</param>
    /// <param name="type">传输类型，0是背包和货郎器具交互，1是背包和存储器具交互，2是背包和运输器具交互，3是仓库和运输器具交互</param>
    public void Open(bool isIn,int type)
    {
        isIn_s = isIn;
        type_s = type;
        if (isIn_s)//放入物品
        {
            Label_SystemName.SetText(true, "Transport1");
            Label_Take.SetText(true, "Transport9");
            if (type_s == 0)//从背包中放入货郎器具
            {
                Label_Title_All.SetText(true, "Transport3");
                foreach (var item in Bag.GetInstance.itemDic)
                {
                    Dic_Right.Add(item.Key, item.Value.count);
                }
            }
            else if(type_s == 1)//从背包中放入存储器具
            {
                Label_Title_All.SetText(true, "Transport3");
                foreach (var item in Bag.GetInstance.itemDic)
                {
                    Dic_Right.Add(item.Key, item.Value.count);
                }
            }
            else if (type_s == 2)//从背包中放入运输器具
            {
                Label_Title_All.SetText(true, "Transport3");
                foreach (var item in Bag.GetInstance.itemDic)
                {
                    Dic_Right.Add(item.Key, item.Value.count);
                }
            }
            else if (type_s == 3)//从仓库中放入运输器具
            {
                Label_Title_All.SetText(true, "Transport5");
                foreach (var item in runTime.Warehouses[warehouseID].GoodsDic)
                {
                    Dic_Right.Add(item.Key, item.Value.Count);
                }
            }
        }
        else//拿出物品
        {
            Label_SystemName.SetText(true, "Transport2");
            Label_Take.SetText(true, "Transport8");
            //if (type_s == 0)//从货郎器具中拿出到背包
            //{
            //    Label_Title_All.SetText(true, "Transport4");
            //    Dic_Right = runTime.PackmanTool;
            //}
            //else if (type_s == 1)//从存储器具中拿出到背包
            //{
            //    Label_Title_All.SetText(true, "Transport6");
            //    Dic_Right = Charactor.GetInstance.TransTool;
            //}
            //else if (type_s == 2)//从运输器具中拿出到背包
            //{
            //    Label_Title_All.SetText(true, "Transport6");
            //    Dic_Right = Charactor.GetInstance.TransTool;
            //}
            //else if (type_s == 3)//从运输器具中拿出到仓库
            //{
            //    Label_Title_All.SetText(true, "Transport6");
            //    Dic_Right = Charactor.GetInstance.ItemBoxes[saveToolID];
            //}
        }

        //在列表中生成道具ICON
        int row = 0;//行，Y轴
        int col = -1;//列，X轴
        foreach (var item in Dic_Right)
        {
            if (row % 6 == 0)
            {
                row = 0;
                col++;
            }
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_All.transform, new Vector3(0 + col * (-68), 0 + row * (-68), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(item.Key, item.Value);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject_Pos_All.GetComponent<UIScrollView>();
            go.name = item.Key.ToString();
            CommonFunc.GetInstance.LableColor(go.GetComponentInChildren<UILabel>());
            Dic_Item.Add(item.Key, go);

            UIEventListener.Get(go).onClick = chooseItem;
            UIEventListener.Get(go).onHover = ShowTips;
            row++;
        }

        //当前的目标空间容量初始化
        if (isIn_s)//放入物品
        {
            if (type_s == 0)//从背包中放入货郎器具
            {
                NowCount = 0;
                AllCount = 100;
            }
            else if (type_s == 1)//从背包中放入存储器具
            {
                NowCount = 0;
                AllCount = 100;
            }
            else if (type_s == 2)//从背包中放入运输器具
            {
                NowCount = 0;
                AllCount = 100;
            }
            else if (type_s == 3)//从仓库中放入运输器具
            {
                NowCount = 0;
                AllCount = 100;
            }
        }
        else//拿出物品
        {
            //if (type_s == 0)//从货郎器具中拿出到背包
            //{
            //    NowCount = (int)Bag.GetInstance.MaxCount;
            //    AllCount = Charactor.GetInstance.GetLoadLimit();
            //}
            //else if (type_s == 1)//从存储器具中拿出到背包
            //{
            //    NowCount = (int)Bag.GetInstance.MaxCount;
            //    AllCount = Charactor.GetInstance.GetLoadLimit();
            //}
            //else if (type_s == 2)//从运输器具中拿出到背包
            //{
            //    NowCount = (int)Bag.GetInstance.MaxCount;
            //    AllCount = Charactor.GetInstance.GetLoadLimit();
            //}
            //else if (type_s == 3)//从运输器具中拿出到仓库
            //{
            //    NowCount = Charactor.GetInstance.Warehouses[warehouseID].Occupy;
            //    AllCount = Charactor.GetInstance.Warehouses[warehouseID].CurrVol;
            //}
        }

        Label_Limit.SetText(false, NowCount + " / " + AllCount);
    }


    // 选择商品打开选择数量界面
    void chooseItem(GameObject btn)
    {
        GameObject_ChooseItem.SetActive(true);
        CommonFunc.GetInstance.SetButtonState(Button_Add_Item, true);
        CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, true);
        CommonFunc.GetInstance.SetButtonState(Button_OK_Item, true);
        GameObject_ChooseItem.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 5;

        itemID = int.Parse(btn.name);
        ItemEX.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
        ItemEX.GetComponent<Botton_ItemEXUI>().SetItem(itemID, 0);

        itemCount = Dic_Right[itemID];
        Item item = Constants.Items_All[itemID];
        Label_ItemName.GetComponent<UIText>().SetText(false, item.name);
        Label_ItemNum.GetComponent<UIText>().SetText(false, itemCount.ToString());

        //输入初始化
        Input_GetNum_Item.value = 0.ToString();
        Input_GetNum_Item.characterLimit = 10;
        Input_GetNum_Item.inputType = UIInput.InputType.Standard;
        Input_GetNum_Item.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_GetNum_Item.onChange, SetNow_Item);

        SetNow_Item();

        //判断商品数量是否大于0
        if (itemCount > 0)
        {
            Button_Reduce_Item.name = btn.name;
            Button_Add_Item.name = btn.name;
            UIEventListener.Get(Button_Reduce_Item).onClick = ReduceCount_Item;
            UIEventListener.Get(Button_Add_Item).onClick = AddCount_Item;
            Button_OK_Item.name = btn.name;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState(Button_Add_Item, false);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, false);
        }

    }

    //减少物品数量
    void ReduceCount_Item(GameObject btn)
    {
        int Input_Num = int.Parse(Input_GetNum_Item.value);
        if (Input_Num <= 0)
        {
            Input_GetNum_Item.value = 0.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, false);
            CommonFunc.GetInstance.SetButtonState(Button_OK_Item, false);
        }
        else
        {
            Input_Num = Input_Num - 1;
            Input_GetNum_Item.value = Input_Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add_Item, true);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, true);
            CommonFunc.GetInstance.SetButtonState(Button_OK_Item, true);
            UIEventListener.Get(Button_OK_Item).onClick = ChooseItem;
        }
        chooseItemCount = int.Parse(Input_GetNum_Item.value);
    }

    //增加物品数量
    void AddCount_Item(GameObject btn)
    {
        int Input_Num = int.Parse(Input_GetNum_Item.value);
        if (Input_Num >= itemCount)
        {
            Input_GetNum_Item.value = itemCount.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add_Item, false);
        }
        else if (Input_Num >= 0)
        {
            Input_Num = Input_Num + 1;
            Input_GetNum_Item.value = Input_Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_Add_Item, true);
            CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, true);
            CommonFunc.GetInstance.SetButtonState(Button_OK_Item, true);
            UIEventListener.Get(Button_OK_Item).onClick = ChooseItem;
        }
        chooseItemCount = int.Parse(Input_GetNum_Item.value);
    }
    //更新输入数据
    void SetNow_Item()
    {
        if (Input_GetNum_Item.value != "" && !Input_GetNum_Item.value.Contains("-"))
        {
            int Input_Num = int.Parse(Input_GetNum_Item.value);
            if (Input_Num > itemCount)
            {
                Input_GetNum_Item.value = itemCount.ToString();
            }
            else if (Input_Num <= 0)
            {
                Input_GetNum_Item.value = 0.ToString();
            }
            if (Input_Num > 0)
            {
                CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, true);
                CommonFunc.GetInstance.SetButtonState(Button_OK_Item, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState(Button_Reduce_Item, false);
                CommonFunc.GetInstance.SetButtonState(Button_OK_Item, false);
            }
        }
        chooseItemCount = int.Parse(Input_GetNum_Item.value);
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

    //选中物品
    void ChooseItem(GameObject btn)
    {
        GameObject_ChooseItem.SetActive(false);

        //判断是添加新的商品，还是添加已经选择商品的数量
        int itemID = int.Parse(btn.name);
        int Count = int.Parse(Input_GetNum_Item.value);

        NowCount += Count;

        int nowCount = int.Parse(Dic_Item[itemID].GetComponentInChildren<UILabel>().text);
        if (chooseDic.ContainsKey(itemID))
        {
            nowCount -= Count;
            Dic_Item[itemID].GetComponentInChildren<UILabel>().text = nowCount.ToString();
            CommonFunc.GetInstance.LableColor(Dic_Item[itemID].GetComponentInChildren<UILabel>());

            chooseDic[itemID] += Count;
        }
        else
        {
            nowCount -= Count;
            Dic_Item[itemID].GetComponentInChildren<UILabel>().text = nowCount.ToString();
            CommonFunc.GetInstance.LableColor(Dic_Item[itemID].GetComponentInChildren<UILabel>());

            chooseDic.Add(itemID, Count);
        }
        RefreshChooseList();
    }

    /// <summary>
    /// 刷新选中列表
    /// </summary>
    void RefreshChooseList()
    {
        NGUITools.DestroyChildren(GameObject_Pos_ChooseItem.transform);

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
            int itemID = item.Key;

            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_ItemEX, GameObject_Pos_ChooseItem.transform, new Vector3(0 + col * (-70), 0 + row * (-70), 0), Vector3.one).gameObject;
            go.GetComponent<Botton_ItemEXUI>().Show_Part(true, true, false, false);
            go.GetComponent<Botton_ItemEXUI>().SetItem(itemID, item.Value);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject_Pos_ChooseItem.GetComponent<UIScrollView>();
            go.name = item.Key.ToString();
            row++;

            UIEventListener.Get(go).onClick = deleteChoose;
            UIEventListener.Get(go).onHover = ShowTips;
        }
        if (NowCount > AllCount)
        {
            Label_Limit.gameObject.GetComponent<UILabel>().color = Color.red;
            CommonFunc.GetInstance.SetButtonState(Button_Take, false);
        }
        else
        {
            Label_Limit.gameObject.GetComponent<UILabel>().color = Color.black;
            CommonFunc.GetInstance.SetButtonState(Button_Take, true);
        }
        Label_Limit.SetText(false, NowCount + " / " + AllCount);
    }

    /// <summary>
    /// 从列表和DIC中删除点击对象
    /// </summary>
    /// <param name="btn"></param>
    void deleteChoose(GameObject btn)
    {
        int itemID_s = int.Parse(btn.name);
        chooseDic.Remove(itemID_s);

        int Count = int.Parse(btn.GetComponentInChildren<UILabel>().text);
        int nowCount = int.Parse(Dic_Item[itemID_s].GetComponentInChildren<UILabel>().text);
        nowCount += Count;

        NowCount -= Count;

        Dic_Item[itemID_s].GetComponentInChildren<UILabel>().text = nowCount.ToString();


        RefreshChooseList();
    }

    /// <summary>
    /// 将已经选择的物品放入目标位置
    /// </summary>
    /// <param name="btn"></param>
    void TakeItem(GameObject btn)
    {
        //当前的目标空间容量初始化
        if (isIn_s)//放入物品
        {
            if (type_s == 0)//从背包中放入货郎器具
            {
            }
            else if (type_s == 1)//从背包中放入存储器具
            {
            }
            else if (type_s == 2)//从背包中放入运输器具
            {
            }
            else if (type_s == 3)//从仓库中放入运输器具
            {
            }
        }
        else//拿出物品
        {
            if (type_s == 0)//从货郎器具中拿出到背包
            {
            }
            else if (type_s == 1)//从存储器具中拿出到背包
            {
            }
            else if (type_s == 2)//从运输器具中拿出到背包
            {
            }
            else if (type_s == 3)//从运输器具中拿出到仓库
            {
            }
        }

        Label_Limit.SetText(false, NowCount + " / " + AllCount);    
    }

    void ItemInfoBack(GameObject btn)
    {
        Sprite_Back_Item.SetActive(false);
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
