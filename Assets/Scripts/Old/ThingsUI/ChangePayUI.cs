// ======================================================================================
// 文 件 名 称：ChangePayUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-31 16:41:10
// 最 后 修 改：xic
// 更 新 时 间：2017-11-16 16:41
// ======================================================================================
// 功 能 描 述：UI：调整薪水
// ======================================================================================
using UnityEngine;

public class ChangePayUI : MonoBehaviour
{
    public Transform GameObject_RoleIconPos;
    public UIText Label_RoleName;
    public GameObject Button_Nature;
    public UIText Label_Nature;
    public UIText Label_NowState;

    public Transform GameObject_NowPos;
    public UIInput Input_GetNum;
    public GameObject Button_Reduce;
    public GameObject Button_Add;
    public Transform GameObject_NextPos;
    public GameObject Button_OK;

    public GameObject Button_Close;
    public GameObject Sprite_Back;

    private int shopId;
    private int roleId;
    private int currSalary;
    private bool raiseOrNot;
    private Transform nextSalary;
    private GameObject lableTips;

    private void Start ()
    {
        ClickControl ();

    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="_shopId"></param>
    /// <param name="_roleId"></param>
    /// <param name="_raise">提薪？降薪？</param>
    public void SetAll (int _shopId, int _roleId, bool _raise)
    {
        var comm = CommonFunc.GetInstance;
        var role = RunTime_Data.RolePool [_roleId];
        var dao = SelectDao.GetDao ();
        var charactor = Charactor.GetInstance;

        raiseOrNot = _raise;
        shopId = _shopId;
        roleId = _roleId;

        Transform roleIcon = comm.UI_Instantiate (Data_Static.UIpath_Button_RoleIcon, GameObject_RoleIconPos, Vector3.zero, Vector3.one);
        roleIcon.Find ("Sprite_RoleInfoIcon").GetComponent<UISprite> ().spriteName = role.headIcon;
        Label_RoleName.SetText (false, role.commonName);

        //var nature = dao.SelecRole_Nature (role.Nature);
        //Label_Nature.SetText (false, nature.name);
        lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, transform, Vector3.zero, Vector3.one).gameObject;
        lableTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        //lableTips.GetComponent<LableTipsUI> ().SetAll (false, nature.des);
        lableTips.SetActive (false);

        //Label_NowState.SetText (false, charactor.GetRelationship (_roleId).Relationship.name);

        // 查询现在的工资
        var post = Temp_Data.GetInstance.TempEmployee.ContainsKey (_shopId) ? Temp_Data.GetInstance.TempEmployee [_shopId] : RunTime_Data.ShopDic [_shopId].Post;
        foreach (var item in post.Values)
        {
            if (item.ContainsKey (_roleId))
            {
                currSalary = item [_roleId].Salary;
                break;
            }
        }

        Transform old = comm.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_NowPos, Vector3.zero, Vector3.one);
        old.GetComponent<Price_MoneyEXUI> ().SetMoney (currSalary);
        Input_GetNum.value = currSalary.ToString ();
        nextSalary = comm.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_NextPos, Vector3.zero, Vector3.one);
        nextSalary.GetComponent<Price_MoneyEXUI> ().SetMoney (currSalary);

        EventDelegate.Add (Input_GetNum.onSubmit, input_num);
    }

    // 根据输入框的数据改变修改后的金钱显示
    private void SetNextSalary ()
    {
        Destroy (nextSalary.gameObject);
        nextSalary = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Price_MoneyEX, GameObject_NextPos, Vector3.zero, Vector3.one);
        nextSalary.GetComponent<Price_MoneyEXUI> ().SetMoney (int.Parse (Input_GetNum.value));
    }

    // 监听输入框：休假
    private void input_num ()
    {
        if (string.IsNullOrEmpty (Input_GetNum.value))
            return;
        int value = int.Parse (Input_GetNum.value);
        if (raiseOrNot)
        {
            if (value > currSalary * 10)
                Input_GetNum.value = (currSalary * 10).ToString ();
            if (value <= currSalary)
                Input_GetNum.value = currSalary.ToString ();
        }
        else
        {
            if (value >= currSalary)
                Input_GetNum.value = currSalary.ToString ();
            if (value < currSalary / 10)
                Input_GetNum.value = (currSalary / 10).ToString ();
        }
        SetNextSalary ();
    }

    // 加号
    private void Add (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        value = CommonFunc.GetInstance.ChangeMoney (true, value);
        if (raiseOrNot)
        {
            if (value < currSalary * 10)
                Input_GetNum.value = value.ToString ();
            else
                Input_GetNum.value = (currSalary * 10).ToString ();
        }
        else
        {
            if (value < currSalary)
                Input_GetNum.value = value.ToString ();
            else
                Input_GetNum.value = currSalary.ToString ();
        }
        SetNextSalary ();
    }

    // 减号
    private void Reduce (GameObject go)
    {
        int value = int.Parse (Input_GetNum.value);
        value = CommonFunc.GetInstance.ChangeMoney (false, value);
        if (raiseOrNot)
        {
            if (value > currSalary)
                Input_GetNum.value = value.ToString ();
            else
                Input_GetNum.value = currSalary.ToString ();
        }
        else
        {
            if (value > currSalary / 10)
                Input_GetNum.value = value.ToString ();
            else
                Input_GetNum.value = (currSalary / 10).ToString ();
        }
        SetNextSalary ();
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

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Nature).onHover = GetNatureDes;

        UIEventListener.Get (Button_Reduce).onClick = Reduce;
        UIEventListener.Get (Button_Add).onClick = Add;

        UIEventListener.Get (Button_OK).onClick = Submit;
    }

    // 提交修改
    private void Submit (GameObject go)
    {
        Temp_Data.GetInstance.AdjustSalary (shopId, roleId, int.Parse (Input_GetNum.value));
        Back (Button_Close);
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}