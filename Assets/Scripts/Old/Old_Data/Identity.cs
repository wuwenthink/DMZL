// ====================================================================================== 
// 文件名         ：    Identity.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-17                                                                  
// 最后修改日期   ：    2017-8-17                                                            
// ====================================================================================== 
// 功能描述       ：    身份                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 身份
/// </summary>
public class Identity
{
    /// <summary>
    /// 身份id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 身份名称
    /// </summary>
    public string identityName { get; private set; }
    /// <summary>
    /// 上级身份id
    /// </summary>
    public int frontId { get; private set; }
    /// <summary>
    /// 身份类型
    /// </summary>
    public IdentityType type { get; private set; }
    /// <summary>
    /// 身份等级
    /// </summary>
    public int level { get; private set; }
    /// <summary>
    /// 等级别称
    /// </summary>
    public string lvDes { get; private set; }
    /// <summary>
    /// 身份介绍
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 额定人数
    /// </summary>
    public int limit { get; private set; }
    /// <summary>
    /// 所属机构id
    /// </summary>
    public int orgId { get; private set; }
    /// <summary>
    /// 机构名称
    /// </summary>
    public string orgName { get; private set; }
    /// <summary>
    /// 身份图标
    /// </summary>
    public string icon { get; private set; }
    /// <summary>
    /// 是否需要入籍
    /// </summary>
    public bool isSign { get; private set; }
    /// <summary>
    /// 上任道具
    /// </summary>
    public int takeOffice { get; private set; }
    /// <summary>
    ///  官凭
    /// </summary>
    public int paper { get; private set; }
    /// <summary>
    /// 印信
    /// </summary>
    public int seal { get; private set; }
    /// <summary>
    /// 公服id
    /// </summary>
    public int cloth_Gong { get; private set; }
    /// <summary>
    /// 常服id
    /// </summary>
    public int cloth_Chang { get; private set; }
    /// <summary>
    /// 素服id
    /// </summary>
    public int cloth_Su { get; private set; }
    /// <summary>
    /// 身份职能id
    /// </summary>
    public int function { get; private set; }
    /// <summary>
    /// 就任要求id
    /// </summary>
    public List<int> need { get; private set; }
    /// <summary>
    /// 学识id
    /// </summary>
    public List<int> learn { get; private set; }
    /// <summary>
    /// 技法id
    /// </summary>
    public List<int> skill { get; private set; }
    /// <summary>
    /// 福利俸禄(道具id，数量)；id=0表示金钱
    /// </summary>
    public Dictionary<int, int> salary { get; private set; }
    /// <summary>
    /// 附加属性(属性id，数量)
    /// </summary>
    public Dictionary<int, int> prop { get; private set; }

    public int needMoney { get; private set; }
    public List<int []> needKnow { get; private set; }
    public List<int []> needSkill { get; private set; }
    public List<int> needBuilding { get; private set; }
    public int appointId { get; private set; }

    public IdentityState idenState { get; set; }

    public Identity (Identity _identity)
    {
        id = _identity.id;
        identityName = _identity.identityName;
        frontId = _identity.frontId;
        type = _identity.type;
        level = _identity.level;
        lvDes = _identity.lvDes;
        des = _identity.des;
        limit = _identity.limit;
        orgId = _identity.orgId;
        orgName = _identity.orgName;
        icon = _identity.icon;
        isSign = _identity.isSign;
        needMoney = _identity.needMoney;
        needKnow = _identity.needKnow;
        needSkill = _identity.needSkill;
        needBuilding = _identity.needBuilding;
        appointId = _identity.appointId;
        takeOffice = _identity.takeOffice;
        paper = _identity.paper;
        seal = _identity.seal;
        cloth_Gong = _identity.cloth_Gong;
        cloth_Chang = _identity.cloth_Chang;
        cloth_Su = _identity.cloth_Su;
        function = _identity.function;
        need = _identity.need;
        learn = _identity.learn;
        skill = _identity.skill;
        salary = _identity.salary;
        prop = _identity.prop;
        idenState = IdentityState.show;
    }


    public Identity (int _id, string _identityName, int _frontId, IdentityType _type, int _level, string _lvDes, string _des, int _limit,
        int _orgId, string _orgName, string _icon, int _isSign, int _needMoney, string _needKnow, string _needSkill, string _needBuilding, int _appointId,
        int _takeOffice, int _paper, int _seal,  int _cloth_Chang, 
        int _function, string _need, string _learn, string _skill, string _salary, string _prop )
    {
        id = _id;
        identityName = _identityName;
        frontId = _frontId;
        type = _type;
        level = _level;
        lvDes = _lvDes;
        des = _des;
        limit = _limit;
        orgId = _orgId;
        orgName = _orgName;
        icon = _icon;
        isSign = _isSign == 1 ? true : false;
        takeOffice = _takeOffice;
        paper = _paper;
        seal = _seal;
        cloth_Chang = _cloth_Chang;

        function =  _function;


        if (!_need.Equals ("0"))
        {
            need = new List<int> ();
            string [] reg = _need.Split (';');
            foreach (string s in reg)
            {
                need.Add (int.Parse (s));
            }
        }
        else
            need = null;

        if (!_learn.Equals ("0"))
        {
            learn = new List<int> ();
            string [] reg = _learn.Split (';');
            foreach (string s in reg)
            {
                learn.Add (int.Parse (s));
            }
        }
        else
            learn = null;

        if (!_skill.Equals ("0"))
        {
            skill = new List<int> ();
            string [] reg = _skill.Split (';');
            foreach (string s in reg)
            {
                skill.Add (int.Parse (s));
            }
        }
        else
            skill = null;

        if (!_salary.Equals ("0"))
        {
            salary = new Dictionary<int, int> ();
            string [] reg = _salary.Split (';');
            foreach (string s in reg)
            {
                salary.Add (int.Parse (s.Split (',') [0]), int.Parse (s.Split (',') [1]));
            }
        }
        else
            salary = null;

        if (!_prop.Equals ("0"))
        {
            prop = new Dictionary<int, int> ();
            string [] reg = _prop.Split (';');
            foreach (string s in reg)
            {
                prop.Add (int.Parse (s.Split (',') [0]), int.Parse (s.Split (',') [1]));
            }
        }
        else
            prop = null;

        needMoney = _needMoney;
        if (!_needKnow.Equals ("0"))
        {
            needKnow = new List<int []> ();
            string [] reg = _needKnow.Split (';');
            foreach (string str in reg)
            {
                var s = str.Split (',');
                int [] tempId = { int.Parse (s [0]), int.Parse (s [1]) };
                needKnow.Add (tempId);
            }
        }
        else
            needKnow = null;
        if (!_needSkill.Equals ("0"))
        {
            needSkill = new List<int []> ();
            string [] reg = _needSkill.Split (';');
            foreach (string str in reg)
            {
                var s = str.Split (',');
                int [] tempId = { int.Parse (s [0]), int.Parse (s [1]) };
                needSkill.Add (tempId);
            }
        }
        else
            needSkill = null;
        if (!_needBuilding.Equals ("0"))
        {
            needBuilding = new List<int> ();
            string [] reg = _needBuilding.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                needBuilding.Add (tempId);
            }
        }
        else
            needBuilding = null;
        appointId = _appointId;
    }
}
