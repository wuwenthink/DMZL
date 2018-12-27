// ======================================================================================
// 文 件 名 称：SupplyAgreementUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-30 22:54:55
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-30 22:54:55
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyAgreementUI : MonoBehaviour {
    public GameObject GameObject_Choose;
    public GameObject Button_FixedTime;
    public GameObject Button_FixedPrice;
    public GameObject Button_Partnership;

    public GameObject GameObject_Agreement;

    public GameObject GameObject_FixedTime;
    public GameObject Button_Sell_FT;
    public GameObject Button_Buy_FT;
    public GameObject Input_GetNum_AllTime_FT;
    public GameObject Button_Reduce_AllTime_FT;
    public GameObject Button_Add_AllTime_FT;
    public GameObject Label_AllTime_Per_FT;
    public GameObject Input_GetNum_SendTime_FT;
    public GameObject Button_Reduce_SendTime_FT;
    public GameObject Button_Add_SendTime_FT;
    public GameObject Label_SendTime_Per_FT;
    public GameObject Input_GetNum_FrontMoney_FT;
    public GameObject Button_Reduce_FrontMoney_FT;
    public GameObject Button_Add_FrontMoney_FT;
    public GameObject Label_FrontMoney_Per_FT;
    public GameObject GameObject_GoodsScroll_FT;
    public GameObject GameObject_GoodsPos_FT;
    public GameObject Label_PriceReduce_FT;
    public GameObject Button_OK_FT;

    public GameObject GameObject_FixedPrice;
    public GameObject Button_Sell_FP;
    public GameObject Button_Buy_FP;
    public GameObject Input_GetNum_AllTime_FP;
    public GameObject Button_Reduce_AllTime_FP;
    public GameObject Button_Add_AllTime_FP;
    public GameObject Label_AllTime_Per_FP;
    public GameObject Input_GetNum_SendTime_FP;
    public GameObject Button_Reduce_SendTime_FP;
    public GameObject Button_Add_SendTime_FP;
    public GameObject Label_SendTime_Per_FP;
    public GameObject Input_GetNum_FrontMoney_FP;
    public GameObject Button_Reduce_FrontMoney_FP;
    public GameObject Button_Add_FrontMoney_FP;
    public GameObject Label_FrontMoney_Per_FP;
    public GameObject GameObject_GoodsScroll_FP;
    public GameObject GameObject_GoodsPos_FP;
    public GameObject Label_PriceReduce_FP;
    public GameObject GameObject_MoneyPos_FP;
    public GameObject Button_OK_FP;

    public GameObject GameObject_Partnership;
    public GameObject Button_Sell_P;
    public GameObject GameObject_ShopOwnerPos;
    public GameObject GameObject_PartnerPos;
    public GameObject GameObject_MoneyPos;
    public GameObject Button_Reduce_Money;
    public GameObject Button_Add_Money;
    public GameObject GameObject_GoodsScroll_P;
    public GameObject GameObject_GoodsPos_P;
    public GameObject Input_GetNum_Share;
    public GameObject Button_Reduce_Share;
    public GameObject Button_Add_Share;
    public GameObject Label_SuccessRate;
    public GameObject Button_OK_P;

    public GameObject Button_Close_FT;
    public GameObject Button_Close_FP;
    public GameObject Button_Close_P;
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
        UIEventListener.Get(Button_Close_FT).onClick = Back;
        UIEventListener.Get(Button_Close_FP).onClick = Back;
        UIEventListener.Get(Button_Close_P).onClick = Back;
    }

    void Back(GameObject btn)
    {

    }


}
