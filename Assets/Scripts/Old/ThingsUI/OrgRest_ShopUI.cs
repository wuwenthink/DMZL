// ======================================================================================
// 文 件 名 称：OrgRest_ShopUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 21:13:58
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-14 17:48
// ======================================================================================
// 功 能 描 述：UI：打烊
// ======================================================================================

using UnityEngine;

public class OrgRest_ShopUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public UIText Label_Time;
    public GameObject Button_Reduce;
    public GameObject Button_Add;

    public GameObject Button_Rest;
    public GameObject Button_Do;

    private Business_Runtime_Shop shop;
    private int tolerantOpenTime;

    // 记录调整过程中的值
    private int time;

    private void Start ()
    {
        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        ClickControl ();

        //SetAll (101);
    }

    public void SetAll (int _shopId)
    {
        //    shop = Charactor.GetInstance.Shops [_shopId];
        //    tolerantOpenTime = shop.OpenTime;
        //    Label_Time.SetText (false, TimeManager.GetTwoHour (tolerantOpenTime));
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Reduce).onClick = Reduce_Num;
        UIEventListener.Get (Button_Add).onClick = Add_Num;

        UIEventListener.Get (Button_Rest).onClick = ClickRest;
        UIEventListener.Get (Button_Do).onClick = ClickDo;
    }

    // 记录新的开店时间
    private void ClickDo (GameObject go)
    {
        shop.SetOpenTime (time);
    }

    // 处理明天休息
    private void ClickRest (GameObject go)
    {
        shop.CloseTomorrow ();
    }

    /// <summary>
    /// 输入-增加按钮
    /// </summary>
    /// <param name="btn"></param>
    private void Add_Num (GameObject btn)
    {
        if (time < 9)
        {
            Label_Time.SetText (false, TimeManager.GetTwoHour (++time));
        }
    }

    /// <summary>
    /// 输入-减少按钮
    /// </summary>
    /// <param name="btn"></param>
    private void Reduce_Num (GameObject btn)
    {
        if (time > 3)
        {
            Label_Time.SetText (false, TimeManager.GetTwoHour (--time));
        }
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}