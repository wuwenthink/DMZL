// ======================================================================================
// 文 件 名 称：ProgressAnimationUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 21:41:16
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 21:41:16
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAnimationUI : MonoBehaviour {
    public UISlider ProgressBar;
    public UILabel Label_Percent;
    public TypewriterEffect Label_DesTip;
    public TweenScale UI_Pos;

    public float animationTime = 0.3f;
    void Start () {
        ProgressBar.value = 0;
    }
	

	void Update () {

        if (Label_DesTip.isActive == false)
        {
            reset();
        }

        ProgressBar.value += animationTime * Time.deltaTime;
        Label_Percent.text = ((int)(ProgressBar.value * 100)).ToString()+"%";
        if (ProgressBar.value == 1)
        {
            UI_Pos.enabled = true;
            UI_Pos.PlayReverse();

            Invoke("delete", UI_Pos.duration);
        }
    }

    void reset()
    {
        Label_DesTip.enabled = true;
        Label_DesTip.ResetToBeginning();
    }

    void delete()
    {
        Destroy(this.gameObject);
    }
}
