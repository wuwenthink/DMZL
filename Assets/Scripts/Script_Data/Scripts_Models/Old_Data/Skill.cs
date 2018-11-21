// ====================================================================================== 
// 文件名         ：    Skill.cs                                                         
// 版本号         ：    v1.1.0                                             
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-28                                                                  
// 最后修改日期   ：    2017-11-18                                                
// ====================================================================================== 
// 功能描述       ：    技法                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 技法
/// </summary>
public class Skill
{
    /// <summary>
    /// 技法id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 技法名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 类型
    /// </summary>
    public SkillType type { get; private set; }
    /// <summary>
    /// 上级技法id
    /// </summary>
    public int aheadClass { get; private set; }
    /// <summary>
    /// 技法描述
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 成长索引
    /// </summary>
    public Skill_Grow grow { get; private set; }
    /// <summary>
    /// 成长途径
    /// </summary>
    public List<string> growWay { get; private set; }

    public Skill (int _id)
    {
        var skill = SelectDao.GetDao ().SelectSkill (_id);
        id = skill.id;
        name = skill.name;
        type = skill.type;
        aheadClass = skill.aheadClass;
        des = skill.des;
        grow = skill.grow;
        growWay = skill.growWay;

    }

    public Skill (int _id, string _name, int _type, int _aheadClass, string _des, int _grow, string _way1, string _way2, string _way3, string _way4, string _way5)
    {
        id = _id;
        name = _name;
        type = (SkillType) _type;
        if (_aheadClass == 0)
            aheadClass = _id;
        else
            aheadClass = _aheadClass;
        des = _des;
        grow = SelectDao.GetDao ().SelectSkill_Grow (_grow);

        growWay = new List<string> ();
        if (!_way1.Equals ("0"))
            growWay.Add (_way1);
        if (!_way2.Equals ("0"))
            growWay.Add (_way2);
        if (!_way3.Equals ("0"))
            growWay.Add (_way3);
        if (!_way4.Equals ("0"))
            growWay.Add (_way4);
        if (!_way5.Equals ("0"))
            growWay.Add (_way5);
    }




}
