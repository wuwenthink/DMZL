// ======================================================================================
// 文 件 名 称：BuyMenuUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:17:51
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:17:51
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuUI : MonoBehaviour {
    public GameObject Label_TipsDesc;
    public GameObject GameObject_RoleIconPos;

    public GameObject GameObject_EnableDeal;
    public GameObject GameObject_BuyScroll;
    public GameObject GameObject_ItemPos;
    public GameObject Button_Deal;
    public GameObject Button_Introduce;

    public GameObject GameObject_StateTips;
    public GameObject Button_Option1;
    public GameObject Button_Option2;
    public GameObject Button_Option3;
    
    public GameObject Sprite_Back;
    

    void Start()
    {

        ClickControl();
    }


    void Update()
    {

    }


    private void ClickControl()
    {
        UIEventListener.Get(Sprite_Back).onClick = Back;
    }

    void Back(GameObject btn)
    {

    }

}
