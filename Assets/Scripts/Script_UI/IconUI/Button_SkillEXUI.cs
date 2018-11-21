// ======================================================================================
// 文 件 名 称：Button_SkillEXUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-16 15:45:55
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-16 15:45:55
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SkillEXUI : MonoBehaviour {

    public UIText Label_SkillName;
    public UIText Label_Lv;
    public GameObject Sprite_Yes;
    public GameObject Sprite_No;
    
    void Start () {
		
	}
	

	void Update () {
		
	}


    /// <summary>
    /// 设置技能按钮数据
    /// </summary>
    /// <param name="name">技能名称</param>
    /// <param name="lv">技能等级，int类型</param>
    /// <param name="state">按钮状态，0是不显示是否满足，1是满足条件，2是不满足条件</param>
    public void SetState(string name,int lv,int state)
    {
        Label_SkillName.SetText(false,name);
        if (lv == 0)
        {
            Label_Lv.SetText(true, "Nomal_10");
        }
        else
        {
            Label_Lv.SetText(true, "skill_" + lv.ToString());
        }
        if (state == 1)
        {
            Sprite_Yes.SetActive(true);
            Sprite_No.SetActive(false);
        }
        else if(state == 2)
        {
            Sprite_Yes.SetActive(false);
            Sprite_No.SetActive(true);
        }
        else
        {
            Sprite_Yes.SetActive(false);
            Sprite_No.SetActive(false);
        }
    }


}
