// ======================================================================================
// 文 件 名 称：Drama_Main.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-26 17:29:41
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-26 17:29:41
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drama_Main : MonoBehaviour {

    public Drama_Main(int _id, string _name, string _icon, int _TCYear, int _TCMonth, int _TCDay,string _Story, string _History)
    {
        id = _id;
        name = _name;
        icon = _icon;
        TCYear = _TCYear;
        TCMonth = _TCMonth;
        TCDay = _TCDay;
        Story = _Story;
        History = _History;
    }

    /// <summary>
    /// 剧本ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 剧本名
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 剧本图标
    /// </summary>
    public string icon { get; private set; }
    /// <summary>
    /// 农历年
    /// </summary>
    public int TCYear { get; private set; }
    /// <summary>
    /// 农历月
    /// </summary>
    public int TCMonth { get; private set; }
    /// <summary>
    /// 农历月
    /// </summary>
    public int TCDay { get; private set; }
    /// <summary>
    /// 剧情介绍
    /// </summary>
    public string Story { get; private set; }
    /// <summary>
    /// 历史介绍
    /// </summary>
    public string History { get; private set; }
}
