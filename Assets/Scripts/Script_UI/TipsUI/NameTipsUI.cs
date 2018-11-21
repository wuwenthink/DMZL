// ======================================================================================
// 文 件 名 称：NameTipsUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：Administrator
// 创 建 时 间：2018-03-24 21:45:16
// 最 后 修 改：Administrator
// 更 新 时 间：2018-03-24 21:45:16
// ======================================================================================
// 功 能 描 述：名称提示框
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTipsUI : MonoBehaviour {
    public UISprite Sprite_Sign;
    public UIText Label_TipsDesc;

    void Start () {
		
	}
	

	void Update () {
		
	}

    public void SetName(string value,string Icon)
    {
        Label_TipsDesc.SetText(false, value);
        Sprite_Sign.spriteName = Icon;
    }


}
