using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 技能主表
/// </summary>
public class skill_Main
{
    /// <summary>
    /// id
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 归属武学
    /// </summary>
    public int gongfu { get; private set; }

    /// <summary>
    /// 武器种类
    /// </summary>
    public int weapon { get; private set; }

    /// <summary>
    /// 等级数量
    /// </summary>
    public int lvCount { get; private set; }

    /// <summary>
    /// 经验参数
    /// </summary>
    public int exp { get; private set; }

    /// <summary>
    /// 阅历参数
    /// </summary>
    public int advantage { get; private set; }

    /// <summary>
    /// buff索引
    /// </summary>
    public string buffIndex { get; private set; }

    /// <summary>
    /// 敌我判断
    /// </summary>
    public int roleType { get; private set; }

    /// <summary>
    /// 触发时机
    /// </summary>
    public int trigger { get; private set; }

    /// <summary>
    /// 技能威力
    /// </summary>
    public int value { get; private set; }

    /// <summary>
    /// 释放距离
    /// </summary>
    public int distance { get; private set; }

    /// <summary>
    /// 范围类型
    /// </summary>
    public int rangeType { get; private set; }

    /// <summary>
    /// 范围数值
    /// </summary>
    public int rangeValue { get; private set; }

    /// <summary>
    /// 行动点数
    /// </summary>
    public int action { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 图标名
    /// </summary>
    public string icon { get; private set; }

    /// <summary>
    /// 动画名
    /// </summary>
    public string animation { get; private set; }

    /// <summary>
    /// 特效名
    /// </summary>
    public string effect { get; private set; }


    public skill_Main(int _id, string _name, int _gongfu, int _weapon, int _lvCount, int _exp, int _advantage, string _buffIndex, int _roleType, int _trigger, int _value, int _distance, int _rangeType, int _rangeValue, int _action, string _des, string _icon, string _animation, string _effect)
    {
        id = _id;
        name = _name;
        gongfu = _gongfu;
        weapon = _weapon;
        lvCount = _lvCount;
        exp = _exp;
        advantage = _advantage;
        buffIndex = _buffIndex;
        roleType = _roleType;
        trigger = _trigger;
        value = _value;
        distance = _distance;
        rangeType = _rangeType;
        rangeValue = _rangeValue;
        action = _action;
        des = _des;
        icon = _icon;
        animation = _animation;
        effect = _effect;
    }



}