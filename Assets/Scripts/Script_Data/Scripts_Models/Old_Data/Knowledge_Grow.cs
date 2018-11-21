// ====================================================================================== 
// 文件名         ：    Knowledge_Grow.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-24                                                                  
// 最后修改日期   ：    2017-8-24                                                            
// ====================================================================================== 
// 功能描述       ：    学识的成长数据                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 学识成长
/// </summary>
public class Knowledge_Grow
{
    /// <summary>
    /// 成长索引id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 成长数据(等级，数据)
    /// </summary>
    public Dictionary<int, Grow> growData { get; private set; }

    public Knowledge_Grow (int _id, int _lv, int _exp, int _tile, string _skill, string _prop)
    {
        id = _id;
        if (growData == null)
            growData = new Dictionary<int, Grow> ();
        growData.Add (_lv, new Grow (_exp, _tile, _skill, _prop));
    }
}
public class Grow
{
    /// <summary>
    /// 所需经验
    /// </summary>
    public int exp { get; private set; }
    /// <summary>
    /// 解锁称号身份
    /// </summary>
    public int title { get; private set; }
    /// <summary>
    /// 解锁技法
    /// </summary>
    public List<int> skill { get; private set; }
    /// <summary>
    /// 解锁属性
    /// </summary>
    public List<string> prop { get; private set; }

    public Grow (int _exp, int _title, string _skill, string _prop)
    {
        exp = _exp;
        title = _title;
        if (!_skill.Equals ("0"))
        {
            skill = new List<int> ();
            string [] reg = _skill.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                skill.Add (tempId);
            }
        }
        else
            skill = null;
        if (!_prop.Equals ("0"))
        {
            prop = new List<string> ();
            string [] reg = _prop.Split (';');
            foreach (string str in reg)
            {
                prop.Add (str);
            }
        }
        else
            prop = null;
    }


}

