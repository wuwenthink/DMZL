// ======================================================================================
// 文 件 名 称：Business_Environment.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:33:41
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-06 21:33:41
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business_Environment : MonoBehaviour {

    public Business_Environment(int _id, string _name, int _popular, int _paper_price, int _booth_price)
    {
        id = _id;
        name = _name;
        popular = _popular;
        paper_price = _paper_price;
        booth_price = _booth_price;
    }



    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 人气度
    /// </summary>
    public int popular { get; private set; }
    /// <summary>
    /// 招募布告基础价格
    /// </summary>
    public int paper_price { get; private set; }
    /// <summary>
    /// 摊位费用基础价格
    /// </summary>
    public int booth_price { get; private set; }




}
