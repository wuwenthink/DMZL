
// ====================================================================================== 
// 文件名         ：    Produce.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-31                                                                      
// 最后修改日期   ：    2017-8-31                                                            
// ====================================================================================== 
// 功能描述       ：    配方                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 配方
/// </summary>
public class MakeRecipe
{
    /// <summary>
    /// 配方ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 配方名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 配方说明
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 需要学识ID
    /// </summary>
    public int KnowType { get; private set; }
    /// <summary>
    /// 需要学识等级
    /// </summary>
    public int KnowLv { get; private set; }
    /// <summary>
    /// 需要技能ID
    /// </summary>
    public int skillType { get; private set; }
    /// <summary>
    /// 需要技能等级
    /// </summary>
    public int skillLv { get; private set; }
    /// <summary>
    /// 需要消耗精力
    /// </summary>
    public int energy { get; private set; }
    /// <summary>
    /// 需要消耗体力
    /// </summary>
    public int power { get; private set; }
    /// <summary>
    /// 成功率
    /// </summary>
    public int success { get; private set; }
    /// <summary>
    /// 需要器具
    /// </summary>
    public int tool { get; private set; }
    /// <summary>
    /// 合成道具ID
    /// </summary>
    public int composeItemId { get; private set; }
    /// <summary>
    /// 合成道具数量
    /// </summary>
    public int composeItemNum { get; private set; }
    /// <summary>
    /// 合成其他道具
    /// </summary>
    public string productOther { get; private set; }
    /// <summary>
    /// 合成其他道具字典
    /// </summary>
    public Dictionary<int, int> Dic_productOther { get; private set; }
    /// <summary>
    /// 需要时间
    /// </summary>
    public int time { get; private set; }
    /// <summary>
    /// 需要材料
    /// </summary>
    public string item_Need { get; private set; }
    /// <summary>
    /// 需要材料字典
    /// </summary>
    public Dictionary<int, int> Dic_item_Need { get; private set; }
    /// <summary>
    /// 订购机构ID
    /// </summary>
    public int CustomOrg { get; private set; }
    /// <summary>
    /// 订购花费
    /// </summary>
    public int CustomMoney { get; private set; }
    /// <summary>
    /// 订购时间
    /// </summary>
    public int CustomTime { get; private set; }




    public MakeRecipe(int _id, string _name, string _des, int _KnowType, int _KnowLv, int _skillType, int _skillLv, int _energy,int _power,int _success, int _tool, string _composeItemID, string _productOther, int _time, string _item_Need, int _CustomOrg, int _CustomMoney, int _CustomTime)
    {
        id = _id;
        name = _name;
        des = _des;
        KnowType = _KnowType;
        KnowLv = _KnowLv;
        skillType = _skillType;
        skillLv = _skillLv;
        energy = _energy;
        power = _power;
        success = _success;
        tool = _tool;
        composeItemId = int.Parse(_composeItemID.Split(',')[0]);
        composeItemNum = int.Parse(_composeItemID.Split(',')[1]);
        productOther = _productOther;
        if (!_productOther.Equals("0"))
        {
            Dic_productOther = new Dictionary<int, int>();
            string[] reg = _productOther.Split(';');
            foreach (string str in reg)
            {
                string[] s = str.Split(',');
                Dic_productOther.Add(int.Parse(s[0]), int.Parse(s[1]));
            }
        }
        time = _time;
        item_Need = _item_Need;
        if (!_item_Need.Equals("0"))
        {
            Dic_item_Need = new Dictionary<int, int>();
            string[] reg = _item_Need.Split(';');
            foreach (string str in reg)
            {
                string[] s = str.Split(',');
                Dic_item_Need.Add(int.Parse(s[0]), int.Parse(s[1]));
            }
        }
        CustomOrg = _CustomOrg;
        CustomMoney = _CustomMoney;
        CustomTime = _CustomTime;
    }




}
