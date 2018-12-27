// ======================================================================================
// 文 件 名 称：GiftUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-08 17:03:37
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-08 17:03:37
// ======================================================================================
// 功 能 描 述：UI：赠送礼物
// ======================================================================================

using System;
using UnityEngine;

public class GiftUI : MonoBehaviour
{
    public GameObject GameObject_RoleIconPos;
    public UIText Label_RoleName;
    public GameObject Button_Nature;
    public UIText Label_Nature;
    public UIText Label_NowState;

    public GameObject GameObject_GiftScroll;
    public Transform GameObject_MarketPos;
    public Transform Button_ItemEX;

    public GameObject Button_OK;
    public GameObject Button_AddItem;
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    GameObject ItemTip;
    Transform AddButton;

    Role_Main role;
    

    int ItemID = 0;
    int ItemCount = 0;
    int Money = 0;
    void Awake()
    {

    }

    void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel(gameObject);

        ItemTip = CommonFunc.GetInstance.Ins_ItemTips(ItemTip);
        ItemTip.GetComponent<UIPanel>().depth = GameObject_GiftScroll.GetComponent<UIPanel>().depth + 1;
    }

    /// <summary>
    /// 打开界面，显示角色信息，初始化列表。
    /// </summary>
    /// <param name="roleId"></param>
    public void Open (int roleId)
    {
        Role_Main role = RunTime_Data.RolePool[roleId];
        Transform go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Button_RoleIcon, GameObject_RoleIconPos.transform, Vector3.zero, Vector3.one);
        go.Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = role.headIcon;
        //Button_Nature.transform.Find("Label_Nature").GetComponent<UIText>().SetText(false, SelectDao.GetDao().SelecRole_Nature(role.Nature).name);
        Label_RoleName.SetText (false, role.commonName);

        //给关系赋值
        Label_NowState.SetText(false,"临时关系");

        //初始化物品列表

    }
    //点击添加按钮添加物品
    void AddItem(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_ChooseItem, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<UIPanel>().depth = this.gameObject.GetComponent<UIPanel>().depth + 10;
    }



    /// <summary>
    /// 接收选择界面的道具信息，添加到列表中
    /// </summary>
    /// <param name="itemInfo">道具ID+道具数量</param>
    public void ReceiveChooseItem(string itemInfo)
    {
        string[] item = itemInfo.Split(';');
        ItemID = int.Parse(item[0]);
        ItemCount = int.Parse(item[1]);
        AddItemToList(true);
    }
    /// <summary>
    /// 接收选择界面的金钱数量，添加到列表中
    /// </summary>
    /// <param name="moneyCount">金钱数量</param>
    public void ReceiveChooseMoney(int moneyCount)
    {
        Money = moneyCount;
        AddItemToList(false);
    }

    /// <summary>
    /// 添加到道具列表中，并重新排序
    /// </summary>
    /// <param name="isItem">添加物品是否是道具</param>
    void AddItemToList(bool isItem)
    {
        if (isItem)
        {

        }
    }

    private void ClickControl ()
    {
        UIEventListener.Get(Sprite_Back).onClick = Back;
        UIEventListener.Get(Button_Close).onClick = Back;
    }

    void Back(GameObject btn)
    {
        Destroy(this.gameObject);
    }
}