// ======================================================================================
// 文 件 名 称：Relationship.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:59:17
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-06 21:59:17
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour {

    public Relationship(int _id, int _type, string _name, int _lv, string _exp, string _exchange)
    {
        id = _id;
        type = _type;
        name = _name;
        lv = _lv;
        exp = _exp;
        exchange = _exchange;
    }


    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 关系类型
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 关系称呼
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 关系层级
    /// </summary>
    public int lv { get; private set; }
    /// <summary>
    /// 好感度
    /// </summary>
    public string exp { get; private set; }
    /// <summary>
    /// 解锁互动功能
    /// </summary>
    public string exchange { get; private set; }




}
