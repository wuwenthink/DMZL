// ======================================================================================
// 文 件 名 称：Botton_ItemEXUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-07 16:05:37
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-07 16:05:37
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using UnityEngine;

public class Botton_ItemEXUI : MonoBehaviour
{
    public GameObject Sprite_BG;
    public GameObject Sprite_Icon;
    public GameObject Label_Num;
    public GameObject Sprite_Choose;
    public GameObject Sprite_Yes;
    public GameObject Sprite_Add;


    void Start ()
    {

    }


    void Update ()
    {

    }

    /// <summary>
    /// 控制各个组件显示隐藏
    /// </summary>
    /// <param name="num">数量</param>
    /// <param name="choose">选中</param>
    /// <param name="yes">对号</param>
    /// <param name="add">添加</param>
    public void Show_Part (bool num, bool choose, bool yes, bool add)
    {
        Label_Num.SetActive (num);
        Sprite_Choose.SetActive (choose);
        Sprite_Yes.SetActive (yes);
        Sprite_Add.SetActive (add);
    }

    /// <summary>
    /// 给道具赋值
    /// </summary>
    /// <param name="iconName">图标名</param>
    /// <param name="num">数量</param>
    public void SetItem (int id, int num)
    {
        Sprite_Icon.GetComponent<UISprite> ().spriteName = Constants.Items_All [id].icon;
        Label_Num.GetComponent<UIText> ().SetText (false, num.ToString ());
    }

}
