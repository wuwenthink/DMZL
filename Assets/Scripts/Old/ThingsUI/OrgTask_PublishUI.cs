// ======================================================================================
// 文 件 名 称：OrgTask_PublishUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-04 12:08:51
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-04 12:08:51
// ======================================================================================
// 功 能 描 述：UI：事务发布
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

public class OrgTask_PublishUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    // 列表
    public GameObject GameObject_ChooseTask;

    public Transform GameObject_Pos_Plan;
    public Transform GameObject_Pos_Own;

    public Transform Button_TaskEX;

    public GameObject Button_Info;
    public GameObject Button_Do_list;

    // 详细信息
    public GameObject GameObject_Info;

    public GameObject Button_Task;
    public UIText Label_Task;
    public GameObject Button_Role;
    public UISprite Sprite_RoleIcon;
    public UIText Label_RoleName;
    public UIToggle Toggle_NoLimit;

    public UIInput Input_GetNum;
    public UIText Label_Uint;
    public GameObject Button_Reduce;
    public GameObject Button_Add;

    public Transform GameObject_GoodsPos;
    public GameObject Button_AddItem;

    public GameObject Button_Do_Info;

    private GameObject systemTips;

    private bool _isEntrust; // 是否委托
    private int _shopId; // 商店ID
    private int _post; // 操作人的职位

    private GameObject _selectedTask; // 记录在列表中选中的任务
    private GameObject _selectItem; // 选中的奖励物品

    private int _taskBaseId = -1; // 选中任务的ID
    private string _taskName = ""; //选中任务的名字
    private int _roleId = 0; // 选中人的ID
    private int _time = -1; // 设定的时限
    private Dictionary<int, int> _award = new Dictionary<int, int> (); // 设定的奖励

    void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel (gameObject);

        ClickControl ();

        systemTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_SystemTips, transform, Vector3.zero, Vector3.one).gameObject;
        systemTips.GetComponent<UIPanel> ().depth = GetComponent<UIPanel> ().depth + 10;
        systemTips.SetActive (false);

        ShowTaskInfo (true, 101, 301);
    }

    /// <summary>
    /// 发布任务
    /// </summary>
    public void ShowTaskInfo (bool isEntrust, int shopId, int postId)
    {
        _isEntrust = isEntrust;
        _shopId = shopId;
        _post = postId;

        GameObject_Info.SetActive (true);
        GameObject_ChooseTask.SetActive (false);

        UIEventListener.Get (Button_Role).onClick = ClickRole;
        UIEventListener.Get (Button_Task).onClick = ClickTaskName;
        EventDelegate.Add (Input_GetNum.onChange, input_num);
        UIEventListener.Get (Button_Reduce).onClick = Reduce_Num;
        UIEventListener.Get (Button_Add).onClick = Add_Num;
        UIEventListener.Get (Button_AddItem).onClick = ClickAddItem;
        UIEventListener.Get (Button_Do_Info).onClick = ClickDo_info;
        EventDelegate.Add (Toggle_NoLimit.onChange, ToggleChanged);

        if (isEntrust)
        {
            CommonFunc.GetInstance.SetButtonState (Button_Role, false);
            Toggle_NoLimit.value = true;
            Toggle_NoLimit.enabled = false;
        }

        Label_Task.SetText (true, "Things81");
        Sprite_RoleIcon.spriteName = "";
        Label_RoleName.SetText (true, "Things82");

    }

    // 是否不限人手
    private void ToggleChanged ()
    {
        if (Toggle_NoLimit.value)
        {
            Sprite_RoleIcon.spriteName = "";
            Label_RoleName.SetText (true, "Things82");
            _roleId = -1;
            CommonFunc.GetInstance.SetButtonState (Button_Role, false);
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState (Button_Role, true);
        }
    }

    // 加入奖励物品
    private void ClickAddItem (GameObject go)
    {
        Transform t = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ChooseItem, transform, Vector3.zero, Vector3.one);
        t.GetComponent<UIPanel> ().depth = transform.GetComponent<UIPanel> ().depth + 10;
    }

    // 接收选定的物品和数量
    private void ReceiveChooseItem (string msg)
    {
        string [] reg = msg.Split (';');
        GenerateItem (int.Parse (reg [0]), int.Parse (reg [1]));
    }

    // 接收选定金钱数量
    private void ReceiveChooseMoney (int num)
    {
        GenerateItem (0, num);
    }

    // 生成奖励物品
    private void GenerateItem (int _id, int _num)
    {
        Transform item = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, GameObject_GoodsPos, new Vector3 (70 * _award.Count, 0), Vector3.one);
        item.name = _id.ToString ();
        item.GetComponent<Botton_ItemEXUI> ().SetItem (_id, _num);
        item.GetComponent<Botton_ItemEXUI> ().Show_Part (true, true, false, false);

        UIEventListener.Get (item.gameObject).onClick = DeleteWarn;
        _award.Add (_id, _num);
    }

    // 点击物品的删除警告
    private void DeleteWarn (GameObject go)
    {
        _selectItem = go;

        var lm = LanguageMgr.GetInstance;

        systemTips.SetActive (true);
        systemTips.GetComponent<SystemTipsUI> ().SetTipDesc (lm.GetText ("Tips_System_19"), lm.GetText ("System_6"), lm.GetText ("System_7"));
    }

    // 接收删除提示的选择结果
    private void ReceiveSystemTips (bool _yes)
    {
        if (_yes)
        {
            Destroy (_selectItem);
            _award.Remove (int.Parse (_selectItem.name));
        }
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
    }

    // 点击选择任务
    private void ClickTaskName (GameObject go)
    {
        GameObject_Info.SetActive (false);
        GameObject_ChooseTask.SetActive (true);
        ShowList ();
    }

    /// <summary>
    /// 显示任务列表
    /// </summary>
    public void ShowList ()
    {
        var common = CommonFunc.GetInstance;
        var dao = SelectDao.GetDao ();

        _taskBaseId = -1;

        // 任务列表（个人）
        var list = TaskStore.GetInstance.tasks_gotton.Values;
        int num = -1;
        foreach (var item in list)
        {
            if (item.type != TaskType.个人任务)
                continue;
            Transform t = common.UI_Instantiate (Button_TaskEX, GameObject_Pos_Own, new Vector3 (-12, -30 - 40 * ++num), Vector3.one);
            t.name = item.id + "_" + item.name;

            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, item.name);

            if (!_isEntrust)
                common.SetButtonState (t.gameObject, false);

            UIEventListener.Get (t.gameObject).onClick = ClickTaskInList;
        }

        var taskDic = RunTime_Data.ShopDic [_shopId].TaskDic;

        // 任务列表（固定）
        var orgList = dao.SelectRandomTaskOrg (-1);
        num = -1;
        foreach (var item in orgList)
        {
            if (taskDic.ContainsKey (item.id) && taskDic [item.id].PublicCount >= taskDic [item.id].numPerMonth)
                continue;

            Transform t = common.UI_Instantiate (Button_TaskEX, GameObject_Pos_Plan, new Vector3 (-12, -30 - 40 * ++num), Vector3.one);
            var name = dao.SelectTask (item.taskId).name;
            t.name = item.taskId + "_" + name + "_" + item.id;

            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, name);

            UIEventListener.Get (t.gameObject).onClick = ClickTaskInList;
        }

        Button_TaskEX.gameObject.SetActive (false);

        UIEventListener.Get (Button_Info).onClick = ClickInfo;
        UIEventListener.Get (Button_Do_list).onClick = ClickDo_list;
    }

    // 在列表中选择任务
    private void ClickTaskInList (GameObject go)
    {
        if (_selectedTask != null)
            CommonFunc.GetInstance.SetButtonPress (_selectedTask, false);
        _selectedTask = go;
        CommonFunc.GetInstance.SetButtonPress (_selectedTask, true);
        string [] reg = go.name.Split ('_');
        _taskBaseId = int.Parse (reg [0]);
        _taskName = reg [1];
    }

    // 点击选择人手
    private void ClickRole (GameObject go)
    {
        if (_selectedTask == null)
            return;
        List<int> list = new List<int> ();

        string [] reg = _selectedTask.name.Split ('_');
        // 组织任务：查询对应职位的员工
        if (reg.Length > 2)
        {
            var postList = SelectDao.GetDao ().SelectTaskOrg (int.Parse (reg [2])).getPost;
            var postInShop = RunTime_Data.ShopDic [_shopId].Post;
            foreach (var post in postList)
            {
                foreach (var shoppost in postInShop.Keys)
                {
                    if (shoppost == post)
                    {
                        foreach (var worker in postInShop [shoppost].Keys)
                            list.Add (worker);
                    }
                }
            }
        }
        // 个人任务：查询权限范围内的员工
        else
        {
            //TODO
        }

        Transform chooseRole = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ChangeRole, transform, Vector3.zero, Vector3.one);
        chooseRole.GetComponent<ChooseRoleUI> ().SetAll (list);

    }

    // 接收选定的人的ID
    private void ReceiveButtonState (int _selectedRoleId)
    {
        var role = RunTime_Data.RolePool [_selectedRoleId];
        Sprite_RoleIcon.spriteName = role.headIcon;
        Label_RoleName.SetText(false, role.commonName);
    }

    // 选择任务
    private void ClickDo_list (GameObject go)
    {
        if (_taskBaseId == -1)
            return;
        GameObject_Info.SetActive (true);
        GameObject_ChooseTask.SetActive (false);
        Label_Task.SetText (false, _taskName);
    }

    // 发布任务
    private void ClickDo_info (GameObject go)
    {
        if (_taskBaseId != -1)
        {
            RunTime_Data.ShopDic [_shopId].PublicTask (_taskBaseId, _roleId);

            Transform labelTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, transform, Vector3.zero, Vector3.one);
            labelTips.GetComponent<LableTipsUI> ().SetAll (true, "System_1", LanguageMgr.GetInstance.GetText ("Tips_Lable10"));
            labelTips.GetComponent<UIPanel> ().depth = GetComponent<UIPanel> ().depth + 10;
        }
    }

    // 查看详情
    private void ClickInfo (GameObject go)
    {
        if (_taskBaseId == -1)
            return;
        Transform taskInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_TaskInfo, transform, Vector3.zero, Vector3.one);
        taskInfo.GetComponent<TaskInfoUI> ().SetInfo (_taskBaseId);
        taskInfo.GetComponent<UIPanel> ().depth = GetComponent<UIPanel> ().depth + 10;
    }


    /// <summary>
    /// 监听输入框
    /// </summary>
    /// <param name="input"></param>
    /// <param name="selected"></param>
    private void input_num ()
    {
        if (!IsPositiveInteger (Input_GetNum.value))
            Input_GetNum.value = "1";
    }

    /// <summary>
    /// 检测输入是否为正整数
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 输入-增加按钮
    /// </summary>
    /// <param name="btn"></param>
    private void Add_Num (GameObject btn)
    {
        int input = int.Parse (Input_GetNum.value);
        Input_GetNum.value = input + 1 + "";
    }

    /// <summary>
    /// 输入-减少按钮
    /// </summary>
    /// <param name="btn"></param>
    private void Reduce_Num (GameObject btn)
    {
        int input = int.Parse (Input_GetNum.value);
        if (input > 1)
            Input_GetNum.value = input - 1 + "";
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}