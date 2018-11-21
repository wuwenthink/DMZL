// ======================================================================================
// 文 件 名 称：HaggleUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:41:57
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:41:57
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using UnityEngine;

public class HaggleUI : MonoBehaviour
{
    public GameObject Label_TitleMain;
    public GameObject Label_Title_Old;
    public GameObject Label_Title_Now;

    public GameObject GameObject_RoleIconPos;
    public GameObject Label_RoleName;
    public GameObject Button_Nature;
    public GameObject Label_NowState;
    public GameObject Label_Success;

    public GameObject GameObject_MarketPos;
    public UIInput Input_GetNum;
    public GameObject Button_Reduce;
    public GameObject Button_Add;
    public GameObject GameObject_NowPos;
    public GameObject Button_Do;
    public GameObject Button_Negotiate;
    public GameObject Button_Rebates;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    int moneyCount = 0;
    int addPrice = 0;
    //获取牙行的最高折扣，临时数据
    int discount = 80;
    //获取牙行的最高回扣比例，临时数据
    int kickback_max = 60;
    GameObject NowMoney;
    GameObject chooseMoney;
    bool isRebates = false;//是否为回扣状态

    int RoleID = 0;//角色ID
    int tradeID = 0;//行业ID
    int higgle_percent = 0;//还价最终成功率
    int negotiate_percent = 0;//谈判最终成功率
    int kickback_percent = 0;//回扣最终成功率

    float AnimationTime = 2;//读条动画时间
    void Awake ()
    {
        isRebates = false;
        NowMoney = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_NowPos.transform, Vector3.zero, Vector3.one).gameObject;
        chooseMoney = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_MarketPos.transform, Vector3.zero, Vector3.one).gameObject;

        CommonFunc.GetInstance.SetUIPanel (this.gameObject);
    }

    void Start ()
    {
        ClickControl ();
    }


    void Update ()
    {

    }

    /// <summary>
    /// 打开界面显示人物信息（暂缺）和金钱信息
    /// </summary>
    /// <param name="MoneyCount">金钱总量</param>
    public void Open (int MoneyCount)
    {
        /*______________人物处理__________________*/

        RoleID = 1001;//NPC测试ID
        string role_name = RunTime_Data.RolePool [RoleID].commonName;
        string icon_name = RunTime_Data.RolePool [RoleID].headIcon;
        Transform icon = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_RoleIcon, GameObject_RoleIconPos.transform, Vector3.zero, Vector3.one);
        icon.Find ("Sprite_RoleInfoIcon").GetComponent<UISprite> ().spriteName = icon_name;
        Label_RoleName.GetComponent<UIText> ().SetText (false, role_name);
        //Button_Nature.transform.Find ("Label_Nature").GetComponent<UIText> ().SetText (false, SelectDao.GetDao ().SelecRole_Nature (RunTime_Data.RolePool [RoleID].Nature).name);

        //关系赋值

        tradeID = 102;//测试数据
        higgle_percent = SelectDao.GetDao ().SelectBusiness_Trade (tradeID).higgle;
        negotiate_percent = SelectDao.GetDao ().SelectBusiness_Trade (tradeID).negotiate;
        kickback_percent = SelectDao.GetDao ().SelectBusiness_Trade (tradeID).kickback;
        Label_Success.GetComponent<UIText> ().SetText (false, higgle_percent.ToString () + "%");
        /*______________金钱处理__________________*/

        chooseMoney.GetComponent<Price_MoneyEXUI> ().SetMoney (MoneyCount);

        moneyCount = MoneyCount;

        Input_GetNum.value = discount.ToString ();
        Input_GetNum.characterLimit = 10;
        Input_GetNum.inputType = UIInput.InputType.Standard;
        Input_GetNum.validation = UIInput.Validation.Integer;
        EventDelegate.Add (Input_GetNum.onChange, SetNow_Haggle);

        SetNow_Haggle ();

        UIEventListener.Get (Button_Reduce).onClick = ReduceCount;
        UIEventListener.Get (Button_Add).onClick = AddCount;

        UIEventListener.Get (Button_Do).onClick = OpenAnimation_Deal;
        UIEventListener.Get (Button_Negotiate).onClick = OpenAnimation_Negotiate;

        //判断自己的身份是否符合打开回扣界面的条件（组织身份非店主）
        bool OrgLeader = false;
        if (!OrgLeader)
        {
            CommonFunc.GetInstance.SetButtonState (Button_Rebates, true);

            UIEventListener.Get (Button_Rebates).onClick = OpenTips;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Rebates, false);
        }

    }
    /// <summary>
    /// 弹出确认提示
    /// </summary>
    /// <param name="btn"></param>
    void OpenTips (GameObject btn)
    {
        Transform go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 10;
        if (!isRebates)
        {
            go.GetComponent<SystemTipsUI> ().SetTipDesc ( LanguageMgr.GetInstance.GetText ("Tips_System_7"), LanguageMgr.GetInstance.GetText ("Tips_5"), LanguageMgr.GetInstance.GetText ("Tips_7"));
        }
        else
        {
            go.GetComponent<SystemTipsUI> ().SetTipDesc (LanguageMgr.GetInstance.GetText ("Tips_System_8"), LanguageMgr.GetInstance.GetText ("Tips_5"), LanguageMgr.GetInstance.GetText ("Tips_6"));
        }
    }

    /// <summary>
    /// 接收系统提示界面的按钮选择
    /// </summary>
    /// <param name="isYes"></param>
    void ReceiveSystemTips (bool isYes)
    {
        if (isYes)
        {
            ChangeModel ();
        }
    }

    //减少数量
    void ReduceCount (GameObject btn)
    {
        int Input_Num = int.Parse (Input_GetNum.value);

        int min = 0;
        if (!isRebates)
        {
            min = discount;
        }
        else
        {
            min = 0;
        }

        if (Input_Num <= min)
        {
            Input_GetNum.value = min.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Reduce, false);
        }
        else
        {
            Input_Num = Input_Num - 1;
            Input_GetNum.value = Input_Num.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
        }

        if (!isRebates)
        {
            SetNow_Haggle ();
        }
        else
        {
            SetNow_Rebates ();
        }
    }
    //增加数量
    void AddCount (GameObject btn)
    {
        int Input_Num = int.Parse (Input_GetNum.value);

        int max = 0;
        if (!isRebates)
        {
            max = 100;
        }
        else
        {
            max = kickback_max;
        }

        if (Input_Num >= max)
        {
            Input_GetNum.value = max.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add, false);
        }
        else if (Input_Num >= 0)
        {
            Input_Num = Input_Num + 1;
            Input_GetNum.value = Input_Num.ToString ();
            CommonFunc.GetInstance.SetButtonState (Button_Add, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
        }

        if (!isRebates)
        {
            SetNow_Haggle ();
        }
        else
        {
            SetNow_Rebates ();
        }
    }

    /// <summary>
    /// 根据公式计算当前的数值
    /// </summary>
    void SetNow_Haggle ()
    {
        if (Input_GetNum.value != "" && !Input_GetNum.value.Contains ("-"))
        {
            int Input_Num = int.Parse (Input_GetNum.value);
            if (Input_Num > 100)
            {
                Input_GetNum.value = 100.ToString ();
            }
            else if (Input_Num <= discount)
            {
                Input_GetNum.value = discount.ToString ();
            }
            int Count = (int) (moneyCount * (float.Parse (Input_GetNum.value) / 100f));

            if (Count > 0)
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
                CommonFunc.GetInstance.SetButtonState (Button_Do, true);
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce, false);
                CommonFunc.GetInstance.SetButtonState (Button_Do, false);
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }
            NowMoney.GetComponent<Price_MoneyEXUI> ().SetMoney (Count);
        }
    }

    //切换模式
    void ChangeModel ()
    {
        if (!isRebates)
        {
            isRebates = true;
            CommonFunc.GetInstance.SetButtonState (Button_Add, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
            CommonFunc.GetInstance.SetButtonState (Button_Do, true);

            Label_TitleMain.GetComponent<UIText> ().SetText (true, "Deal2");
            Label_Title_Old.GetComponent<UIText> ().SetText (true, "Deal5");
            Label_Title_Now.GetComponent<UIText> ().SetText (true, "Deal6");
            Button_Rebates.GetComponentInChildren<UIText> ().SetText (true, "Deal8");

            //根据交易总额计算溢价总额，临时数据
            int count = (int) (moneyCount * 0.2f);
            addPrice = count;
            Input_GetNum.value = kickback_max.ToString ();
            chooseMoney.GetComponent<Price_MoneyEXUI> ().SetMoney (count);
            EventDelegate.Remove (Input_GetNum.onChange, SetNow_Haggle);
            EventDelegate.Add (Input_GetNum.onChange, SetNow_Rebates);
            SetNow_Rebates ();


        }
        else
        {
            isRebates = false;
            CommonFunc.GetInstance.SetButtonState (Button_Add, true);
            CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
            CommonFunc.GetInstance.SetButtonState (Button_Do, true);

            Input_GetNum.value = discount.ToString ();
            Label_TitleMain.GetComponent<UIText> ().SetText (true, "Deal1");
            Label_Title_Old.GetComponent<UIText> ().SetText (true, "Deal3");
            Label_Title_Now.GetComponent<UIText> ().SetText (true, "Deal4");
            Button_Rebates.GetComponentInChildren<UIText> ().SetText (true, "Deal7");
            chooseMoney.GetComponent<Price_MoneyEXUI> ().SetMoney (moneyCount);
            EventDelegate.Remove (Input_GetNum.onChange, SetNow_Rebates);
            EventDelegate.Add (Input_GetNum.onChange, SetNow_Haggle);
            SetNow_Haggle ();
        }
    }

    /// <summary>
    /// 根据公式计算当前的数值：回扣
    /// </summary>
    void SetNow_Rebates ()
    {
        if (Input_GetNum.value != "" && !Input_GetNum.value.Contains ("-"))
        {
            int Input_Num = int.Parse (Input_GetNum.value);
            if (Input_Num > kickback_max)
            {
                Input_GetNum.value = kickback_max.ToString ();
            }
            else if (Input_Num <= 0)
            {
                Input_GetNum.value = 0.ToString ();
            }
            int Count = (int) (addPrice * (float.Parse (Input_GetNum.value) / 100f));
            if (Count > 0)
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce, true);
                CommonFunc.GetInstance.SetButtonState (Button_Do, true);
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, true);
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState (Button_Reduce, false);
                CommonFunc.GetInstance.SetButtonState (Button_Do, false);
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }
            NowMoney.GetComponent<Price_MoneyEXUI> ().SetMoney (Count);
        }
    }

    //执行还价/回扣
    void OpenAnimation_Deal (GameObject btn)
    {
        //播放还价读条动画
        Transform go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ProgressAnimation, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        AnimationTime = 1 / go.GetComponent<ProgressAnimationUI> ().animationTime;
        go.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 10;
        StartCoroutine (Deal ());
    }

    IEnumerator Deal ()
    {
        //N秒动画后
        yield return new WaitForSeconds (AnimationTime);
        //弹出结果提示
        Transform go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 10;
        //判断是还价还是回扣
        if (!isRebates)
        {
            //是否成功,传递还价后金钱
            if (Random.value * 100 < higgle_percent)
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable2"));
            }
            else
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable5"));
            }
        }
        else
        {
            //是否成功，传递原本金钱
            if (Random.value * 100 < kickback_percent)
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable3"));
            }
            else
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable6"));
            }
            //传递给自己增加回扣部分的金钱


        }

        //关闭界面
        Debug.Log ("关闭还价界面");
    }

    void OpenAnimation_Negotiate (GameObject btn)
    {
        //播放还价读条动画
        Transform go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ProgressAnimation, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        AnimationTime = 1 / go.GetComponent<ProgressAnimationUI> ().animationTime;
        go.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 10;
        StartCoroutine (Negotiate ());
    }

    //谈判
    IEnumerator Negotiate ()
    {
        //N秒动画后
        yield return new WaitForSeconds (AnimationTime);
        //弹出结果提示
        Transform go = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        go.GetComponent<UIPanel> ().depth = this.gameObject.GetComponent<UIPanel> ().depth + 10;

        //是否成功,传递读条后成功率
        negotiate_percent = SelectDao.GetDao ().SelectBusiness_Trade (tradeID).negotiate;
        if (!isRebates)
        {
            if (Random.value * 100 < higgle_percent)
            {
                higgle_percent += Random.Range (5, higgle_percent);
                higgle_percent = Mathf.Clamp (higgle_percent, 0, 100);
                Label_Success.GetComponent<UIText> ().SetText (false, higgle_percent.ToString () + "%");

                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable4"));

                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }
            else
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable7"));
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }
        }
        else
        {
            if (Random.value * 100 < kickback_percent)
            {
                kickback_percent += Random.Range (5, kickback_percent);
                kickback_percent = Mathf.Clamp (kickback_percent, 0, 100);
                Label_Success.GetComponent<UIText> ().SetText (false, kickback_percent.ToString () + "%");

                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable4"));

                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }
            else
            {
                go.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable7"));
                CommonFunc.GetInstance.SetButtonState (Button_Negotiate, false);
            }

        }
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
    }

    void Back (GameObject btn)
    {
        Destroy (this.gameObject);
    }


}
