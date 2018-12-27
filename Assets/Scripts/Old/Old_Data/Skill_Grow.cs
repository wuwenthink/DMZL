// ====================================================================================== 
// 文件名         ：    Skill_Grow.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-28                                                                  
// 最后修改日期   ：    2017-8-28                                                            
// ====================================================================================== 
// 功能描述       ：    技法成长数据                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 技法成长数据
/// </summary>
public class Skill_Grow
{
    /// <summary>
    /// 成长索引
    /// </summary>
    public int growId { get; private set; }
    /// <summary>
    /// 成长数据（等级，数据）
    /// </summary>
    public Dictionary<int, SkillGrowData> growData { get; private set; }

    public Skill_Grow(int _id, int _lv, int _exp, string _prop, string _produce)
    {
        growId = _id;
        if(growData == null)
            growData = new Dictionary<int, SkillGrowData>();
        growData.Add(_lv, new SkillGrowData(_exp, _prop, _produce));
    }
}
public class SkillGrowData
{
    /// <summary>
    /// 所需经验
    /// </summary>
    public int exp { get; private set; }
    /// <summary>
    /// 解锁属性
    /// </summary>
    public List<string> prop { get; private set; }
    /// <summary>
    /// 解锁配方
    /// </summary>
    public List<int> produce { get; private set; }

    public SkillGrowData(int _exp, string _prop, string _produce)
    {
        exp = _exp;
        if(!_prop.Equals("0"))
        {
            prop = new List<string>();
            string[] reg = _prop.Split(';');
            foreach(string str in reg)
            {
                prop.Add(str);
            }
        }
        else
            prop = null;

        if(!_produce.Equals("0"))
        {
            produce = new List<int>();
            string[] reg = _produce.Split(';');
            foreach(string str in reg)
            {
                produce.Add(int.Parse(str));
            }
        }
        else
            produce = null;
    }

}
