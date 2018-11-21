// ======================================================================================
// 文 件 名 称：TaskInfoUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-18 22:05:46
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-18 22:05:46
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using UnityEngine;

public class TaskInfoUI : MonoBehaviour
{

    public GameObject GameObject_DescScroll;
    public UIText Lable_DescEX;
    public UIText Label_AskFor;
    public GameObject GameObject_GiftScroll;
    public Transform GameObject_GiftPos;
    public UIText Label_Time;
    public UIText Label_State;
    public GameObject Botton_GiveUp;
    public GameObject Sprite_Back;

    GameObject ItemTip;
    private TaskStore taskStore;
    private Transform lableTips;

    void Awake ()
    {
        CommonFunc.GetInstance.SetUIPanel (gameObject);
        ItemTip = CommonFunc.GetInstance.Ins_ItemTips (ItemTip);
        ItemTip.GetComponent<UIPanel> ().depth = GameObject_GiftScroll.GetComponent<UIPanel> ().depth + 10;

        UIEventListener.Get (Sprite_Back).onClick = Back;
    }

    /// <summary>
    /// 设置任务详情内容
    /// </summary>
    /// <param name="taskID">任务ID</param>
    public void SetInfo (int taskID)
    {

        taskStore = TaskStore.GetInstance;

        var task = SelectDao.GetDao ().SelectTask (taskID);

        Lable_DescEX.SetText (false, task.des);

        //Label_Time.SetText (false, taskStore.tasks_gotton [id].timeGet.ToString ());

        // TPUPDATE 任务要求
        if (task.value_demand != null)
            Label_AskFor.SetText (false, task.value_demand [0]);


        // 奖励
        int num = -1;
        if (task.item_award != null)
        {
            Transform ex = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
            foreach (string str in task.item_award)
            {
                string [] reg = str.Split (',');
                int itemId = int.Parse (reg [0]);
                int itemNum = int.Parse (reg [1]);

                Transform item = Instantiate<Transform> (ex);
                item.name = itemId.ToString ();
                item.parent = GameObject_GiftPos;
                item.localScale = Vector3.one;
                item.localPosition = new Vector3 (++num * 60, 0, 0);

                item.Find ("Sprite_SalaryIcon").GetComponentInChildren<UISprite> ().atlas = FindObjectOfType<GameObject_Static> ().UIAtlas_Icon_Item1;
                item.Find ("Sprite_SalaryIcon").GetComponentInChildren<UISprite> ().spriteName = Constants.Items_All [itemId].icon;

                item.Find ("Label_SalaryNum").GetComponent<UIText> ().SetText (false, reg [1]);

                UIEventListener.Get (item.gameObject).onClick = ClickItem;
            }
            Destroy (ex.gameObject);
        }
        else if (task.prop_award == null)
        {
            Transform none = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Label_NoneEX, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);

            none.parent = GameObject_GiftPos;
            none.localScale = Vector3.one;
            none.localPosition = Vector3.zero;
        }
        if (task.prop_award != null)
        {
            Transform ex = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_PropEX, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
            foreach (string str in task.prop_award)
            {
                string [] reg = str.Split (',');
                int propId = int.Parse (reg [0]);
                int propNum = int.Parse (reg [1]);

                Transform prop = Instantiate (ex);
                prop.name = propId.ToString ();
                prop.parent = GameObject_GiftPos;
                prop.localScale = Vector3.one;
                prop.localPosition = new Vector3 (++num * 60, 0, 0);

                //prop.Find ("Label_PropName").GetComponent<UIText> ().SetText (false, Constants.RoleProp [(RoleInfo) (int.Parse (reg [0]))]);
                prop.Find ("Label_PropAdd").GetComponent<UIText> ().SetText (false, reg [1]);

            }
            Destroy (ex.gameObject);
        }
    }

    /// <summary>
    /// 点击奖励中的道具
    /// </summary>
    /// <param name="btn"></param>
    private void ClickItem (GameObject btn)
    {
        lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ItemTips, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        lableTips.GetComponent<ItemTipsUI> ().itemTipsSet (int.Parse (btn.name), true);
    }


    void Back (GameObject btn)
    {
        Destroy (gameObject);
    }


}
