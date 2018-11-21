// ======================================================================================
// 文 件 名 称：Business_Goods.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:44:32
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-06 21:44:32
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business_Goods : MonoBehaviour {

    public Business_Goods(int _id, int _itemId, int _type, string _productName, int _StoreLevel, int _normalPrice, string _floatRange, int _minimumSellingPrice, int _minimumWholesalePrice, string _bidPrice, int _isPrimary)
    {
        id = _id;
        itemId = _itemId;
        type = _type;
        productName = _productName;
        StoreLevel = _StoreLevel;
        normalPrice = _normalPrice;
        floatRange = _floatRange;
        minimumSellingPrice = _minimumSellingPrice;
        minimumWholesalePrice = _minimumWholesalePrice;
        bidPrice = _bidPrice;
        isPrimary = _isPrimary;
    }


    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 道具ID
    /// </summary>
    public int itemId { get; private set; }
    /// <summary>
    /// 商品类型
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 商品名
    /// </summary>
    public string productName { get; private set; }
    /// <summary>
    /// 最低商店等级
    /// </summary>
    public int StoreLevel { get; private set; }
    /// <summary>
    /// 标准价
    /// </summary>
    public int normalPrice { get; private set; }
    /// <summary>
    /// 市场价浮动区间
    /// </summary>
    public string floatRange { get; private set; }
    /// <summary>
    /// 最低售卖价
    /// </summary>
    public int minimumSellingPrice { get; private set; }
    /// <summary>
    /// 最低批发价
    /// </summary>
    public int minimumWholesalePrice { get; private set; }
    /// <summary>
    /// 收购价区间
    /// </summary>
    public string bidPrice { get; private set; }
    /// <summary>
    /// 是否基础商品
    /// </summary>
    public int isPrimary { get; private set; }



}
