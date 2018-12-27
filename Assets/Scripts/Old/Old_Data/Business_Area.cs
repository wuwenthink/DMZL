// ======================================================================================
// 文 件 名 称：Business_Area.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:20:38
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-06 21:20:38
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business_Area : MonoBehaviour {

    public Business_Area(int _id, string _name, int _type, int _area, int _ya_price_num, int _yun_price_num)
    {
        id = _id;
        name = _name;
        type = _type;
        area = _area;
        ya_price_num = _ya_price_num;
        yun_price_num = _yun_price_num;
    }



    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 地区名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 地区类型
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 所属场景
    /// </summary>
    public int area { get; private set; }
    /// <summary>
    /// 牙商价格参数
    /// </summary>
    public int ya_price_num { get; private set; }
    /// <summary>
    /// 运商价格参数
    /// </summary>
    public int yun_price_num { get; private set; }

}
