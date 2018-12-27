// ======================================================================================
// 文 件 名 称：OrgTask_FixedUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 21:38:03
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：UI：任务安排
// ======================================================================================

using UnityEngine;

public class OrgTask_FixedUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public Transform GameObject_Pos_Plan;
    public Transform GameObject_Pos_Today;

    public Transform Button_TaskEX;

    public GameObject Button_Already;
    public GameObject Button_Info;

    private GameObject _select;
    private Business_Runtime_Shop shop;

    private void Start ()
    {
        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        ClickControl ();

        SetAll (102);
    }

    private void SetAll (int _shopId)
    {
        var common = CommonFunc.GetInstance;
        shop = RunTime_Data.ShopDic [_shopId];
        var dao = SelectDao.GetDao ();

        common.SetButtonState (Button_Already, false);
        common.SetButtonState (Button_Info, false);

        // 阶段任务
        var list = shop.TaskDic.Values;
        int num = -1;
        foreach (var item in list)
        {
            if (item.isDaily || !item.WithinLimit)
                continue;

            var task = dao.SelectTask (item.taskId);

            Transform t = common.UI_Instantiate (Button_TaskEX, GameObject_Pos_Plan, new Vector3 (0, -50 * ++num), Vector3.one);
            t.name = item.taskId + "_" + (int) item.State + "_" + item.id;

            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, task.name);
            var time = TimeManager.GetTime (TimeManager.TimeUnit.Twohour, task.time - (TimeManager.GetTime () - item.publicTime));
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, time);
            switch (item.State)
            {
                case Runtime_OrgTask.OrgTaskState.Nobody:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_16");
                    break;
                case Runtime_OrgTask.OrgTaskState.Running:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_17");
                    break;
                case Runtime_OrgTask.OrgTaskState.Finished:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_18");
                    break;
            }

            UIEventListener.Get (t.gameObject).onClick = SelectTask;
        }

        // 假装还有today任务列表
        num = -1;
        foreach (var item in list)
        {
            if (!item.isDaily || !item.WithinLimit)
                continue;

            var task = dao.SelectTask (item.taskId);

            Transform t = common.UI_Instantiate (Button_TaskEX, GameObject_Pos_Today, new Vector3 (0, -50 * ++num), Vector3.one);
            t.name = item.taskId + "_" + (int) item.State + "_" + item.id;

            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, task.name);
            var time = TimeManager.GetTime (TimeManager.TimeUnit.Twohour, task.time - (TimeManager.GetTime () - item.publicTime));
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, time);
            switch (item.State)
            {
                case Runtime_OrgTask.OrgTaskState.Nobody:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_16");
                    break;
                case Runtime_OrgTask.OrgTaskState.Running:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_17");
                    break;
                case Runtime_OrgTask.OrgTaskState.Finished:
                    t.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_18");
                    break;
            }

            UIEventListener.Get (t.gameObject).onClick = SelectTask;
        }

        Button_TaskEX.gameObject.SetActive (false);

    }

    // 点选一条任务
    private void SelectTask (GameObject go)
    {
        if (go.name.Split ('_') [1].Equals ("0"))
            CommonFunc.GetInstance.SetButtonState (Button_Already, true);
        else
            CommonFunc.GetInstance.SetButtonState (Button_Already, false);

        CommonFunc.GetInstance.SetButtonState (Button_Info, true);

        if (_select)
            _select.transform.Find ("Sprite_Choose").gameObject.SetActive (false);
        _select = go;
        _select.transform.Find ("Sprite_Choose").gameObject.SetActive (true);
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Already).onClick = ApplyTask;
        UIEventListener.Get (Button_Info).onClick = TaskInfo;
    }

    // 查看任务详情
    private void TaskInfo (GameObject go)
    {
        if (!_select)
            return;

        Transform taskInfo = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_TaskInfo, transform, Vector3.zero, Vector3.one);
        taskInfo.GetComponent<TaskInfoUI> ().SetInfo (int.Parse (_select.name.Split ('_') [0]));
        taskInfo.GetComponent<UIPanel> ().depth = GetComponent<UIPanel> ().depth + 10;
    }

    // 申请任务
    private void ApplyTask (GameObject go)
    {
        if (!_select)
            return;

        string [] reg = _select.name.Split ('_');
        shop.TaskDic [int.Parse (reg [2])].SetRole (-1);
        TaskStore.GetInstance.GetTask (int.Parse (reg [0]));

        _select.transform.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_17");
        _select.name = reg [0] + "_" + "1" + "_" + reg [2];
        CommonFunc.GetInstance.SetButtonState (Button_Already, false);

        var task = TaskStore.GetInstance.tasks_gotton [int.Parse (reg [0])];
        var lm = LanguageMgr.GetInstance;
        Transform newTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_NewTips, transform, Vector3.zero, Vector3.one);
        newTips.GetComponent<NewTipsUI> ().SetNew (null, lm.GetText ("Tips_Title_2"), task.name);
        newTips.GetComponent<UIPanel> ().depth = GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}