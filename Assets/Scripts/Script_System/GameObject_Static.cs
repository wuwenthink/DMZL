// ======================================================================================
// 文 件 名 称：GameObject_Static.cs
// 版 本 编 号：v1.3.0
// 原 创 作 者：XiaoRui
// 修 改 人    ：wuwenthink
// 创 建 日 期：
// 更 新 日 期：2017-10-03 22:07:13
// ======================================================================================
// 功 能 描 述：统一的数据处理
// ======================================================================================

using UnityEngine;

public class GameObject_Static : MonoBehaviour
{
    //图集
    public UIAtlas UIAtlas_UI_Main;

    public UIAtlas UIAtlas_UI_MainBG;
    public UIAtlas UIAtlas_Icon_Item1;
    public UIAtlas UIAtlas_Icon_RoleHead;
    public UIAtlas UIAtlas_NowIcon;

    public UIAtlas Atlas_Map;

    public void Inst_UIAtlas ()
    {
        UIAtlas_UI_Main = UIAtlas_Instantiate (Data_Static.UIAtlas_UI_Main);
        UIAtlas_UI_MainBG = UIAtlas_Instantiate (Data_Static.UIAtlas_UI_MainBG);
        UIAtlas_Icon_Item1 = UIAtlas_Instantiate (Data_Static.UIAtlas_Icon_Item1);
        UIAtlas_Icon_RoleHead = UIAtlas_Instantiate (Data_Static.UIAtlas_Icon_RoleHead);
        UIAtlas_NowIcon = UIAtlas_Instantiate (Data_Static.UIAtlas_NowIcon);

        //Atlas_Map = UIAtlas_Instantiate (Data_Static.Atlas_Map);
    }

    private void Start ()
    {
    }

    private void Update ()
    {
    }

    //图集的实例化
    public UIAtlas UIAtlas_Instantiate (string path)
    {
        UIAtlas ins_GO = (UIAtlas) Instantiate (Resources.Load (path, typeof (UIAtlas)));
        ins_GO.transform.parent = FindObjectOfType<UIRoot> ().transform;
        return ins_GO;
    }

    /// <summary>
    /// 颜色处理
    /// </summary>
    /// <param name="switchValue">判断对象</param>
    /// <param name="dic">参数：判断值和颜色值列表的键值对</param>
    /// <returns></returns>
    public Color ColorDeal (int r, int g, int b, int a)
    {
        //颜色赋值

        float color_r = (float) r / 255f;
        float color_g = (float) g / 255f;
        float color_b = (float) b / 255f;
        float color_a = (float) a / 255f;

        Color colorValue = new Color (color_r, color_g, color_b, color_a);
        return colorValue;
    }
}
