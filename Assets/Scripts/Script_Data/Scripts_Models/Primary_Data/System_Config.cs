// ======================================================================================
// 文 件 名 称：System_Config.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-01 21:02:45
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-01 21:02:45
// ======================================================================================
// 功 能 描 述：系统配置表
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Config  {

    public System_Config(int _id, string _word, string _value, string _des, string _des2)
    {
        id = _id;
        word = _word;
        values = _value;
        des = _des;
        des2 = _des2;
    }
    /// <summary>
    /// id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 字段
    /// </summary>
    public string word { get; private set; }
    /// <summary>
    /// 对应值
    /// </summary>
    public string values { get; private set; }
    public string des { get; private set; }
    public string des2 { get; private set; }





}
