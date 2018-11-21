using UnityEngine;
using UnityEditor;

/// <summary>
/// 技能BUFF表
/// </summary>
public class skill_Buff 
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
    /// 触发几率
    /// </summary>
    public int percent { get; private set; }

    /// <summary>
    /// 触发时机
    /// </summary>
    public int trigger { get; private set; }

    /// <summary>
    /// 持续回合
    /// </summary>
    public int round { get; private set; }

    /// <summary>
    /// buff类型
    /// </summary>
    public int type { get; private set; }

    /// <summary>
    /// 影响属性
    /// </summary>
    public string prop { get; private set; }

    /// <summary>
    /// BUFF数值
    /// </summary>
    public string value { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 图标名
    /// </summary>
    public string icon { get; private set; }

    /// <summary>
    /// 特效名
    /// </summary>
    public string effect { get; private set; }


    public skill_Buff(int _id, string _name, int _percent, int _trigger, int _round, int _type, string _prop, string _value, string _des, string _icon, string _effect)
    {
        id = _id;
        name = _name;
        percent = _percent;
        trigger = _trigger;
        round = _round;
        type = _type;
        prop = _prop;
        value = _value;
        des = _des;
        icon = _icon;
        effect = _effect;
    }

}