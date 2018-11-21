// ======================================================================================
// 文 件 名 称：NearTipsUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-1
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-1
// ======================================================================================
// 功 能 描 述：靠近对象后的触发UI
// ======================================================================================

using UnityEngine;

public class NearTipsUI : MonoBehaviour
{
    public GameObject Button_NearTips;
    public GameObject GameObject_Info;
    public UIText Label_NearName;
    public Transform ButtonGroup_NearTips;
    public Transform ButtonEX;

    /// <summary>
    /// 对象挂载脚本中的数据
    /// </summary>
    //private MountData _mountData;

    private int _state;

    private void Start ()
    {
        CommonFunc.GetInstance.SetUIPanel(gameObject);

        _state = 0;

        //UIEventListener.Get (Button_NearTips).onClick = ShowButtonGroup;
    }

    private void Update ()
    {
        //if (_state == 0 && Input.GetKeyDown (KeyCode.E))
        //{
        //    ShowButtonGroup (Button_NearTips);
        //}
    }

    ///// <summary>
    ///// 初始化
    ///// </summary>
    ///// <param name="md"></param>
    //public void SetMountData (MountData md)
    //{
    //    _mountData = md;
    //    _state = 0;
    //    Button_NearTips.SetActive (true);
    //    GameObject_Info.SetActive (false);
    //}

    //// 展示触发按钮组
    //private void ShowButtonGroup (GameObject go)
    //{
    //    _state = 1;

    //    Button_NearTips.SetActive (false);
    //    GameObject_Info.SetActive (true);

    //    Label_NearName.SetText (false, _mountData.Name);

    //    var optionList = _mountData.OptionList;
    //    int num = -1;
    //    foreach (Option op in optionList)
    //    {
    //        Transform opButton = CommonFunc.GetInstance.UI_Instantiate (ButtonEX, ButtonGroup_NearTips, new Vector3 (0, ++num * -45), Vector3.one);
    //        opButton.name = op.id.ToString ();

    //        opButton.Find ("Label_NearTipsDes").GetComponent<UIText> ().SetText (false, op.option);
    //        opButton.Find ("GameObject_Need/Label_Need").GetComponent<UIText> ().SetText (false, op.need);

    //        UIEventListener.Get (opButton.gameObject).onClick = ClickOption;
    //    }
    //    ButtonEX.gameObject.SetActive (false);
    //}

    // 点击选项后的操作
    private void ClickOption (GameObject go)
    {
        // TODO
    }
}

