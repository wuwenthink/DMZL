// ======================================================================================
// 文 件 名 称：IndexMenu_Job.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-20 11:37:38
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-20 11:37:38
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexMenu_Job : MonoBehaviour {

    public IndexMenu_Job(int _id, string _des, int _behind, int _lv, int _job, int _window)
    {
        id = _id;
        des = _des;
        behind = _behind;
        lv = _lv;
        job = _job;
        window = _window;
    }


    /// <summary>
    /// 菜单ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 菜单文字
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 上级菜单
    /// </summary>
    public int behind { get; private set; }
    /// <summary>
    /// 菜单层级
    /// </summary>
    public int lv { get; private set; }
    /// <summary>
    /// 职事ID
    /// </summary>
    public int job { get; private set; }
    /// <summary>
    /// 开启界面编号
    /// </summary>
    public int window { get; private set; }

}
