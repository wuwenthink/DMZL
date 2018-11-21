using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_SceneModel {
    /// <summary>
    /// 场景模板ID
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 场景模板可修改名称
    /// </summary>
    public string writeName { get; set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string des { get; set; }
    /// <summary>
    /// 场景背景图
    /// </summary>
    public string ground { get; set; }
    /// <summary>
    /// 场景图标
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 最大尺寸X（格子数）
    /// </summary>
    public int maxSize_X { get; set; }
    /// <summary>
    /// 最大尺寸Y（格子数）
    /// </summary>
    public int maxSize_Y { get; set; }

    public Map_SceneModel(int _id, string _writeName, string _des, string _ground, string _icon, int _maxSize_X, int _maxSize_Y)
    {
        id = _id;
        writeName = _writeName;
        des = _des;
        ground = _ground;
        icon = _icon;
        maxSize_X = _maxSize_X;
        maxSize_Y = _maxSize_Y;
    }
}
