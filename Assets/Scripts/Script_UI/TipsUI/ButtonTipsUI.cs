// ======================================================================================
// 文 件 名 称：ButtonTipsUI.cs
// 版 本 编 号：v1.1.2
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-28 13:06:23
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-15 16:52
// ======================================================================================
// 功 能 描 述：UI：ButtonTips
// ======================================================================================

using UnityEngine;
using System.Collections.Generic;

public class ButtonTipsUI : MonoBehaviour
{
    public UISprite Sprite_BGTable;
    public UISprite Sprite_BGTable2;
    public Transform GameObject_ButtonPos;
    public Transform ButtonEX;
    public GameObject Sprite_Black;

    private Transform UI_Root;

    public List<GameObject> buttonGroup;

    private void Awake()
    {
        buttonGroup = new List<GameObject>();
    }

    private void Start ()
    {
        UI_Root = FindObjectOfType<UIRoot> ().transform;
        CommonFunc.GetInstance.SetUIPanel (gameObject);
    }

    /// <summary>
    /// 设置按钮内容
    /// </summary>
    /// <param name="isClick">是否允许点击空白关闭</param>
    /// <param name="buttons">每个按钮要显示的文字</param>
    public List<GameObject> SetAll (bool isClick ,params string [] buttons)
    {
        NGUITools.DestroyChildren(GameObject_ButtonPos);
        buttonGroup.Clear();
        ButtonEX.gameObject.SetActive(true);
        if (isClick)
        {
            UIEventListener.Get(Sprite_Black).onClick = Back;
        }
        Sprite_BGTable.SetRect (0, 0, Sprite_BGTable.localSize.x, 45 + (buttons.Length - 1) * 20);
        Sprite_BGTable.transform.localPosition = new Vector3 (0, 0, 0);
        Sprite_BGTable2.SetRect (0, 0, Sprite_BGTable2.localSize.x, 45 + (buttons.Length - 1) * 20);
        Sprite_BGTable2.transform.localPosition = new Vector3 (0, 0, 0);

        int index = 1;
        foreach (string str in buttons)
        {
            Transform buttonObj = CommonFunc.GetInstance.UI_Instantiate (ButtonEX, GameObject_ButtonPos, Vector3.zero, Vector3.one);

            buttonObj.name = index.ToString ();
            buttonObj.GetComponentInChildren<UIText> ().SetText (false, str);
            UIEventListener.Get (buttonObj.gameObject).onClick = ClickButton;
            index++;
            buttonGroup.Add(buttonObj.gameObject);
        }
        GameObject_ButtonPos.GetComponent<UIGrid>().enabled = true;
        ButtonEX.gameObject.SetActive (false);

        return buttonGroup;
    }

    /// <summary>
    /// 设置按钮内容
    /// </summary>
    /// <param name="isClick">是否允许点击空白关闭</param>
    /// <param name="buttons">每个按钮要显示的文字</param>
    public List<GameObject> SetAll (bool isClick, params ButtonText [] buttons)
    {
        NGUITools.DestroyChildren(GameObject_ButtonPos);
        buttonGroup.Clear();
        ButtonEX.gameObject.SetActive(true);
        if (isClick)
        {
            UIEventListener.Get(Sprite_Black).onClick = Back;
        }
        ClearButtons ();
        ButtonEX.gameObject.SetActive (true);

        Sprite_BGTable.SetRect (0, 0, Sprite_BGTable.localSize.x, 45 + (buttons.Length - 1) * 20);
        Sprite_BGTable.transform.localPosition = new Vector3 (0, 0, 0);
        Sprite_BGTable2.SetRect (0, 0, Sprite_BGTable2.localSize.x, 45 + (buttons.Length - 1) * 20);
        Sprite_BGTable2.transform.localPosition = new Vector3 (0, 0, 0);

        foreach (var btn in buttons)
        {
            Transform buttonObj = CommonFunc.GetInstance.UI_Instantiate (ButtonEX, GameObject_ButtonPos, Vector3.zero, Vector3.one);

            buttonObj.name = (btn.Id).ToString ();
            buttonObj.GetComponentInChildren<UIText> ().SetText (false, btn.Text);
            UIEventListener.Get (buttonObj.gameObject).onClick = ClickButton;
            buttonGroup.Add(buttonObj.gameObject);
        }
        GameObject_ButtonPos.GetComponent<UIGrid>().enabled = true;
        ButtonEX.gameObject.SetActive (false);

        return buttonGroup;
    }

    // 清空全部按钮
    private void ClearButtons ()
    {
        foreach (Transform child in GameObject_ButtonPos)
        {
            if (child.name != GameObject_ButtonPos.name && child.name != ButtonEX.name)
                Destroy (child.gameObject);
        }
    }

    // 监听按钮并反馈给调用ButtonTips的UI
    private void ClickButton (GameObject btn)
    {
        Back (Sprite_Black);
    }

    private void Back (GameObject go)
    {
        this.gameObject.SetActive(false);
    }
}


public class ButtonText
{
    public int Id;
    public string Text;

    public ButtonText(int _id, string _text)
    {
        Id = _id;
        Text = _text;
    }
    public ButtonText()
    {

    }
}