// ======================================================================================
// 文 件 名 称：NewTipsUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-02 17:52:31
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-02 17:52:31
// ======================================================================================
// 功 能 描 述：新消息提示
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTipsUI : MonoBehaviour {
    public GameObject Label_Title;
    public GameObject Label_TipsDesc;
    public GameObject GameObject_IconPos;

    void Start () {

	}
	

	void Update () {
		
	}

    /// <summary>
    /// 设置新消息内容
    /// </summary>
    /// <param name="title">新-标题</param>
    /// <param name="desc">新-内容</param>
    /// <param name="add">已添加到……中</param>
    public void SetNew(GameObject Icon,string title,string desc)
    {
        if (Icon != null)
        {
            Icon.transform.parent = GameObject_IconPos.transform;
            Icon.transform.localPosition = Vector3.zero;
            Icon.transform.localScale = Vector3.one;
        }
        Label_Title.GetComponent<UIText>().SetText(false, title);
        Label_TipsDesc.GetComponent<UIText>().SetText(false, desc);
    }

}
