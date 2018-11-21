// ======================================================================================
// 文 件 名 称：Role_Nature.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 19:33:04
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 19:33:04
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_Nature : MonoBehaviour {
    public Role_Nature(int _id, string _name, string _des, int _good, int _bad)
    {
        id = _id;
        name = _name;
        des = _des;
        good = _good;
        bad = _bad;
    }



    /// <summary>
    /// id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 影响善事的参数
    /// </summary>
    public int good { get; private set; }
    /// <summary>
    /// 影响坏事的参数
    /// </summary>
    public int bad { get; private set; }
}
