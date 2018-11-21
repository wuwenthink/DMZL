// ======================================================================================
// 文 件 名 称：HolidayUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-14 16:01:14
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-15 10:13
// ======================================================================================
// 功 能 描 述：UI：请假
// ======================================================================================

using UnityEngine;

public class HolidayUI : MonoBehaviour
{
    public GameObject Button_Close;
    public GameObject Sprite_Back;

    public UIText Label;
    
    public GameObject GameObject_RoleInfoIconPos;
    public UIText Label_RoleName;
    public GameObject Button_Nature;
    public UIText Label_Nature;
    public UIText Label_NowState;

    public UIText Label_Des;
    public Transform GameObject_MoneyPos;
    public UIText Label_Time;
    public UIInput Input_GetNum;
    public GameObject Button_Reduce;
    public GameObject Button_Add;

    public GameObject Button_Do;

    private Transform money;
    // 可休假天数
    private int holidayCount;
    // 一天的工资
    private int dailySalary;
    private Transform UI_Root;
    private GameObject lableTips;



    void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel (gameObject);
        ClickControl ();

        UI_Root = FindObjectOfType<UIRoot> ().transform;
        lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, UI_Root, Vector3.zero, Vector3.one).gameObject;
        lableTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;

        SetAll (102, true);
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="_shopId">所在商店的ID</param>
    /// <param name="_askForLeave">请假？休假</param>
    public void SetAll (int _shopId, bool _askForLeave)
    {
        var dao = SelectDao.GetDao ();

        int bossId = -1;
        var shop = RunTime_Data.ShopDic [_shopId];
        // 有掌柜的话，上司是掌柜，否则是店主
        if (shop.Post.ContainsKey (303))
            foreach (var id in shop.Post [303].Keys)
                bossId = id;
        else
            bossId = shop.HostId;

        var boss = RunTime_Data.RolePool [bossId];
        Label_RoleName.SetText(false, boss.commonName);

        //var nature = dao.SelecRole_Nature (boss.Nature);
        //Label_Nature.SetText (false, nature.name);
        //lableTips.GetComponent<LableTipsUI> ().SetAll (false, nature.des);
        lableTips.SetActive (false);
        if (bossId == -1)
            Label_NowState.SetText (true, "Things54");
        //else
        //    Label_NowState.SetText (false, Charactor.GetInstance.GetRelationship (bossId).Relationship.name);

        //var postId = Charactor.GetInstance.GetPostId (_shopId);
        //var post = dao.SelecRole_Job (postId);

        if (_askForLeave)
        // 请假的情况
        {
            Label.SetText (true, "Things4");
            Label_Des.SetText (true, "Things70");
            UIEventListener.Get (Button_Reduce).onClick = Reduce_Ask;
            UIEventListener.Get (Button_Add).onClick = Add_Ask;
            EventDelegate.Add (Input_GetNum.onChange, input_num_Ask);

            //dailySalary = shop.Post [postId] [-1].Salary / 30;
            money = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos, Vector3.zero, Vector3.one);
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (dailySalary);
        }
        else
        // 休假的情况
        {
            Label.SetText (true, "Things69");
            Label_Des.SetText (true, "Things71");
            UIEventListener.Get (Button_Reduce).onClick = Reduce_Normal;
            UIEventListener.Get (Button_Add).onClick = Add_Normal;
            EventDelegate.Add (Input_GetNum.onChange, input_num_Normal);

            //holidayCount = post.holiday;
            Label_Time.SetText (false, (holidayCount - 1).ToString ());
        }
        Input_GetNum.value = "1";

    }

    // 监听输入框：休假
    private void input_num_Normal ()
    {
        if (string.IsNullOrEmpty (Input_GetNum.value))
            return;
        if (IsPositiveInteger (Input_GetNum.value))
        {
            int value = int.Parse (Input_GetNum.value);
            if (value > 30)
                Input_GetNum.value = "30";
            if (value <= 0)
                Input_GetNum.value = "1";
        }
        else
        {
            Input_GetNum.value = "1";
        }
        Label_Time.SetText (false, (holidayCount - int.Parse (Input_GetNum.value)).ToString ());
    }

    // 监听输入框：请假
    private void input_num_Ask ()
    {
        if (string.IsNullOrEmpty (Input_GetNum.value))
            return;
        if (IsPositiveInteger (Input_GetNum.value))
        {
            int value = int.Parse (Input_GetNum.value);
            if (value > 30)
                Input_GetNum.value = "30";
            if (value <= 0)
                Input_GetNum.value = "1";
        }
        else
        {
            Input_GetNum.value = "1";
        }
        money.GetComponent<Price_MoneyEXUI> ().SetMoney (dailySalary * int.Parse (Input_GetNum.value));
    }

    // 检测输入是否为正整数
    private bool IsPositiveInteger (string text)
    {
        char [] charArray = text.ToCharArray ();
        foreach (char ch in charArray)
        {
            if (ch < '0' || ch > '9')
                return false;
        }
        return true;
    }

    // 加号：休假天数
    private void Add_Normal (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        if (value <= holidayCount - 1)
        {
            Input_GetNum.value = (++value).ToString ();
            Label_Time.SetText (false, (holidayCount - value).ToString ());
        }
    }

    // 减号：休假天数
    private void Reduce_Normal (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        if (value > 1)
        {
            Input_GetNum.value = (--value).ToString ();
            Label_Time.SetText (false, (holidayCount - value).ToString ());
        }
    }

    // 加号：请假天数
    private void Add_Ask (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        if (value < 30)
        {
            Input_GetNum.value = (++value).ToString ();
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (dailySalary * value);
        }
    }

    // 减号：请假天数
    private void Reduce_Ask (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        if (value > 1)
        {
            Input_GetNum.value = (--value).ToString ();
            money.GetComponent<Price_MoneyEXUI> ().SetMoney (dailySalary * value);
        }
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;
        UIEventListener.Get (Sprite_Back).onClick = Back;

        UIEventListener.Get (Button_Nature).onHover = GetNatureDes;

        UIEventListener.Get (Button_Do).onClick = ClickDo;
    }

    // 点击告知上级
    private void ClickDo (GameObject go)
    {
        //Charactor.GetInstance.AddHoliday (int.Parse (Input_GetNum.value));
        Back (Button_Close);
    }

    // 鼠标滑过显示性格描述
    private void GetNatureDes (GameObject go, bool state)
    {
        if (state)
        {
            lableTips.SetActive (true);
        }
        else
        {
            lableTips.SetActive (false);
        }
    }


    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}
