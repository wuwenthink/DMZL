// ======================================================================================
// 文 件 名 称：Price_MoneyEX.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-09 13:16:09
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-09 13:16:09
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using UnityEngine;

public class Price_MoneyEXUI : MonoBehaviour
{
    public GameObject Button_Silver;
    public UILabel Label_Num_Silver;
    public UISprite Sprite_Icon_Silver;
    public GameObject Button_Copper;
    public UILabel Label_Num_Copper;
    public UISprite Sprite_Icon_Copper;

    GameObject_Static GS;

    public int MoneyCount;
    void Start ()
    {
        GS = FindObjectOfType<GameObject_Static> ();
    }


    void Update ()
    {

    }

    /// <summary>
    /// 设置金钱颜色
    /// </summary>
    /// <param name="isEnough">金钱是否足够</param>
    /// <returns></returns>
    public void SetMoneyColor(bool isEnough)
    {
        if (isEnough)
        {
            Label_Num_Silver.color = Color.black;
            Label_Num_Copper.color = Color.black;
        }
        else
        {
            Label_Num_Silver.color = Color.red;
            Label_Num_Copper.color = Color.red;
        }
    }

    public GameObject SetMoney (int moneyCount)
    {
        int copper_value = moneyCount % 1000;
        int silver_value = moneyCount / 1000;
        MoneyCount = moneyCount;
        // 2种货币

        //显示银子
        //Sprite_Icon_Silver.atlas = GS.UIAtlas_Icon_Item1;
        Sprite_Icon_Silver.name = Constants.Items_All[2].icon;
        Label_Num_Silver.text = silver_value.ToString ();

        // 显示铜板
        //Sprite_Icon_Copper.atlas = GS.UIAtlas_Icon_Item1;
        Sprite_Icon_Copper.name = Constants.Items_All [1].icon;
        Label_Num_Copper.text = copper_value.ToString ();




        return this.gameObject;
    }

}
