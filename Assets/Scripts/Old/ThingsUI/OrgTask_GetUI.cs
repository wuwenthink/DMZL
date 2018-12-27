// ======================================================================================
// 文 件 名 称：OrgTask_GetUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-04 11:38:25
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：UI：委托任务
// ======================================================================================

using UnityEngine;

public class OrgTask_GetUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public Transform GameObject_Pos;

    public Transform Button_TaskEX;

    public GameObject Button_Already;

    private Business_Runtime_Shop shop;
    private GameObject _select;

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

        common.SetButtonState (Button_Already, false);

        // 委托任务列表
        var list = shop.EntrustTaskDic.Values;
        int num = -1;
        foreach (var task in list)
        {
            Transform t = common.UI_Instantiate (Button_TaskEX, GameObject_Pos, new Vector3 (0, -40 * ++num), Vector3.one);
            t.name = task.baseId + "_" + (int) task.State;

            t.Find ("Label_Name").GetComponent<UIText> ().SetText (false, task.name);
            var time = TimeManager.GetTime (TimeManager.TimeUnit.Twohour, task.time - (task.publicTime - TimeManager.GetTime ()));
            t.Find ("Label_Time").GetComponent<UIText> ().SetText (false, time);

            switch (task.State)
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

            Transform rewardPos = t.Find ("GameObject_ItemPos");
            int itemNum = -1;
            foreach (var item in task.item_award)
            {
                string [] reg = item.Split (',');
                Transform clone = common.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, rewardPos, new Vector3 (++itemNum * 70, 0), Vector3.one);
                clone.GetComponent<Botton_ItemEXUI> ().SetItem (int.Parse (reg [0]), int.Parse (reg [1]));
                clone.GetComponent<Botton_ItemEXUI> ().Show_Part (true, true, false, false);
            }

            UIEventListener.Get (t.gameObject).onClick = SelectTask;
        }

        Button_TaskEX.gameObject.SetActive (false);
    }

    // 点选一个任务
    private void SelectTask (GameObject go)
    {
        if (_select)
            _select.transform.Find ("Sprite_Choose").gameObject.SetActive (false);
        _select = go;
        _select.transform.Find ("Sprite_Choose").gameObject.SetActive (true);

        int state = int.Parse (_select.name.Split ('_') [1]);
        if (state == 0)
            CommonFunc.GetInstance.SetButtonState (Button_Already, true);
        else
            CommonFunc.GetInstance.SetButtonState (Button_Already, false);
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Already).onClick = ClickAlready;
    }

    // 申请任务
    private void ClickAlready (GameObject go)
    {
        CommonFunc.GetInstance.SetButtonState (Button_Already, false);

        var taskId = int.Parse (_select.name.Split ('_') [0]);

        TaskStore.GetInstance.GetTask (taskId);
        _select.name = taskId + "_" + "1";
        shop.EntrustTaskDic [taskId].State = Runtime_OrgTask.OrgTaskState.Running;
        _select.transform.Find ("Label_State").GetComponent<UIText> ().SetText (true, "Task_17");

        var task = TaskStore.GetInstance.tasks_gotton [taskId];
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