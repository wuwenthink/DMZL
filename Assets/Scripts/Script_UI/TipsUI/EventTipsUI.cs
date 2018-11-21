// ======================================================================================
// 文 件 名 称：EventTipsUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-13 21:23:24
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-13 21:23:24
// ======================================================================================
// 功 能 描 述：TipsUI：突发事件处理
// ======================================================================================

using UnityEngine;

public class EventTipsUI : MonoBehaviour
{
    public UIText Label_TipsDesc;
    public Transform GameObject_ButtonPos;
    public Transform Button_AnswerEX;

    private Transform UI_Root;

    // Use this for initialization
    void Start ()
    {
        UI_Root = FindObjectOfType<UIRoot> ().transform;
    }

    public void SetAll (params string [] buttons)
    {
        var comm = CommonFunc.GetInstance;
        int num = -1;
        foreach (string btn in buttons)
        {
            Transform t = comm.UI_Instantiate (Button_AnswerEX, GameObject_ButtonPos, new Vector3 (-100 + ++num, 0), Vector3.one);
            t.name = num.ToString ();
            t.Find ("Label_Answer").GetComponent<UIText> ().SetText (false, btn);
            UIEventListener.Get (t.gameObject).onClick = ClickButton;
        }
    }

    // 监听按钮并反馈给调用EventTipsUI的UI
    private void ClickButton (GameObject btn)
    {
        UI_Root.BroadcastMessage ("ReceiveButtonState", int.Parse (btn.name));

        Destroy (gameObject);
    }
}