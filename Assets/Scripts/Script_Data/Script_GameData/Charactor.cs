// ======================================================================================
// 文 件 名 称：Charactor.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-11 13:21:29
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-13 19:17
// ======================================================================================
// 功 能 描 述：主角信息
// ======================================================================================

using System.Collections.Generic;

/// <summary>
/// 角色数据：所有角色都会拥有的非表格数据
/// </summary>
public class Charactor : Role_Main
{
    private static Charactor _instance;

    public static Charactor GetInstance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// 健康
    /// </summary>
    public int health { set; get; }
    /// <summary>
    /// 精神
    /// </summary>
    public int mind { set; get; }
    /// <summary>
    /// 心情
    /// </summary>
    public int mood { set; get; }
    /// <summary>
    /// 饱腹度
    /// </summary>
    public int hunger { set; get; }
    /// <summary>
    /// 冷暖
    /// </summary>
    public int temp { set; get; }
    /// <summary>
    /// 行动力
    /// </summary>
    public int action { set; get; }
    /// <summary>
    /// 生命
    /// </summary>
    public int hp { set; get; }
    /// <summary>
    /// 防御
    /// </summary>
    public int def { set; get; }
    /// <summary>
    /// 速度
    /// </summary>
    public int speed { set; get; }
    /// <summary>
    /// 伤害
    /// </summary>
    public int demage { set; get; }
    /// <summary>
    /// 暴击
    /// </summary>
    public int crit { set; get; }
    /// <summary>
    /// 技能冷却
    /// </summary>
    public int skillCD { set; get; }
    /// <summary>
    /// 技能威力
    /// </summary>
    public int skillEffect { set; get; }
    /// <summary>
    /// 技能数量上限
    /// </summary>
    public int skillMax { set; get; }
    /// <summary>
    /// 控制抗性
    /// </summary>
    public int resistance { set; get; }
    /// <summary>
    /// 经验
    /// </summary>
    public int exp { set; get; }
    /// <summary>
    /// 阅历
    /// </summary>
    public int advantage { set; get; }

    /// <summary>
    /// 犯罪状态:1-3级，分别为：轻微、严重、恶劣
    /// </summary>
    public int crime { set; get; }

    /// <summary>
    /// 角色拥有的身份字典
    /// </summary>
    public Dictionary<int, Dictionary<int, Identity>> Dic_Idens { private set; get; }
    /// <summary>
    /// 拥有的技能集合
    /// </summary>
    public Dictionary<int, Skill_Learned> SkillDic { private set; get; }
    /// <summary>
    /// 拥有的学识集合
    /// </summary>
    public Dictionary<int, Knowledge_Learned> KnowLedgeDic { private set; get; }
    /// <summary>
    /// 关系集合
    /// </summary>
    public Dictionary<int, Runtime_Relationship> Relationship;

    public Charactor(int _id, string _commonName, string _gender, int _birthday, string _place, string _story, string _headIcon, string _imageID, int _famous, int _tili, int _wuli, int _zhili, int _poli, int _yili, int _meili, int _shengyu, int _mingwang)
        : base(_id, _commonName, _gender,  _birthday,  _place,  _story,  _headIcon,  _imageID,  _famous,  _tili,  _wuli,  _zhili,  _poli,  _yili,  _meili,  _shengyu,  _mingwang)
    {
        _instance = this;
        health = 100;
        mood = 100;
        temp = 50;
        hunger = 100;

        crime = 0;

        //1-3级属性换算
        hp = _tili * 8 + _zhili * 2; ;
        def = _tili * 10;
        speed = _tili * 2 + _wuli * 6 + _zhili * 2;
        demage = _wuli * 10;
        crit = _wuli * 10;
        skillCD = _zhili * 10;
        skillEffect = _wuli * 2 + _zhili * 8;
        skillMax = _tili * 2 + _zhili * 8;

        action = speed / 5;
        resistance = def * 5 + speed * 2;
        //Warehouses = new Dictionary<int, Business_Runtime_Warehouse> ();
        //Shops = new Dictionary<int, Business_Runtime_Shop> ();
        //Relationship_Fixed = new Dictionary<int, Runtime_Relationship> ();
        //Relationship_Snap = new Dictionary<int, Runtime_Relationship> ();
        //PackmanTool = new Dictionary<int, int> ();
        //TransTool = new Dictionary<int, int>();
        RunTime_Data.RolePool.Add (-1, this);
    }

    /// <summary>
    /// 获取学识经验
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_exp"></param>
    public void Learn_Knowledge(int _id, int _exp = 0)
    {
        Knowledge_Learned knowledge = new Knowledge_Learned(_id, _exp);
        if (!KnowLedgeDic.ContainsKey(_id))
        {
            KnowLedgeDic.Add(_id, knowledge);
            int ahaeadType = (int)KnowLedgeDic[_id].aheadType;
            if (!KnowLedgeDic.ContainsKey(ahaeadType))
            {
                KnowLedgeDic.Add(ahaeadType, new Knowledge_Learned(ahaeadType));
                KnowLedgeDic.Add(ahaeadType + 10, new Knowledge_Learned(ahaeadType + 10));
            }
        }
        else
            KnowLedgeDic[_id].Exp += _exp;
    }

    /// <summary>
    /// 获取技法经验
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_exp"></param>
    public void Learn_Skill(int _id, int _exp = 0)
    {
        if (!SkillDic.ContainsKey(_id))
        {
            SkillDic.Add(_id, new Skill_Learned(_id, _exp));
            int aheadClass = (int)SkillDic[_id].aheadClass;
            if (!SkillDic.ContainsKey(aheadClass))
            {
                SkillDic.Add(aheadClass, new Skill_Learned(aheadClass));
            }
        }
        SkillDic[_id].Exp += _exp;
    }

    /// <summary>
    /// 计算一项学识/技法的熟练度等级
    /// </summary>
    /// <param name="_isKnowledge">学识？技法？</param>
    /// <param name="_id"></param>
    /// <returns></returns>
    public int GetLv_Skill(bool _isKnowledge, int _id)
    {
        if (_isKnowledge)
        {
            if (!KnowLedgeDic.ContainsKey(_id))
                return 0;

            for (int lv = 0; lv <= 9; lv++)
            {
                if (KnowLedgeDic[_id].Exp < KnowLedgeDic[_id].grow.growData[lv].exp)
                    return lv - 1;
            }
            return 9;
        }
        else
        {
            if (!SkillDic.ContainsKey(_id))
                return 0;

            for (int lv = 0; lv <= 9; lv++)
            {
                if (SkillDic[_id].Exp < SkillDic[_id].grow.growData[lv].exp)
                    return lv - 1;
            }
            return 9;
        }
    }

    /// <summary>
    /// 计算学识/技法截止到当前等级的属性增量
    /// </summary>
    /// <param name="_isKnowledge">学识？技法？</param>
    /// <param name="_id"></param>
    /// <returns>"属性id,增量"</returns>
    public Dictionary<int, int> CalculatePropertyIncrement(bool _isKnowledge, int _id)
    {
        int level = GetLv_Skill(_isKnowledge, _id);

        Dictionary<int, int> temp = new Dictionary<int, int>();

        if (_isKnowledge && KnowLedgeDic.ContainsKey(_id))
        {
            for (int lv = 0; lv <= level; lv++)
            {
                if (KnowLedgeDic[_id].grow.growData[lv].prop != null)
                {
                    foreach (string str in KnowLedgeDic[_id].grow.growData[lv].prop)
                    {
                        int prop = int.Parse(str.Split(',')[0]);
                        int count = int.Parse(str.Split(',')[1]);
                        if (temp.ContainsKey(prop))
                            temp[prop] += count;
                        else
                            temp.Add(prop, count);
                    }
                }
            }
        }
        else if (SkillDic.ContainsKey(_id))
        {
            for (int lv = 0; lv <= level; lv++)
            {
                if (SkillDic[_id].grow.growData[lv].prop != null)
                {
                    foreach (string str in SkillDic[_id].grow.growData[lv].prop)
                    {
                        int prop = int.Parse(str.Split(',')[0]);
                        int count = int.Parse(str.Split(',')[1]);
                        if (temp.ContainsKey(prop))
                            temp[prop] += count;
                        else
                            temp.Add(prop, count);
                    }
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// 根据类型计算已学过的书籍数量
    /// </summary>
    /// <param name="_knowledgeType"></param>
    /// <returns></returns>
    public int GetBookCount_Know(KnowledgeType _knowledgeType)
    {
        int result = 0;
        foreach (var item in KnowLedgeDic.Values)
        {
            if (item.aheadType == _knowledgeType && item.knowledgeClass == KnowledgeClass.每类书籍)
                result++;
        }
        return result;
    }

    ///// <summary>
    ///// 获得，改变一个关系
    ///// </summary>
    ///// <param name="_npcId"></param>
    ///// <param name="_exp"></param>
    //public void ChangeRelationship(int _npcId, int _exp)
    //{
    //    if (!Relationship_Fixed.ContainsKey(_npcId) && !Relationship_Snap.ContainsKey(_npcId))
    //        Relationship_Snap.Add(_npcId, new Runtime_Relationship(_npcId, _exp));
    //    else if (Relationship_Fixed.ContainsKey(_npcId))
    //    {
    //        Relationship_Fixed[_npcId].ChangeExp(_exp);
    //        if ((Relationship_Fixed[_npcId].Exp) < 20 && (Relationship_Fixed[_npcId].Exp) > -20)
    //        {
    //            Relationship_Fixed.Remove(_npcId);
    //            Relationship_Snap.Add(_npcId, new Runtime_Relationship(_npcId, _exp));
    //        }
    //    }
    //    else if (Relationship_Snap.ContainsKey(_npcId))
    //    {
    //        Relationship_Snap[_npcId].ChangeExp(_exp);
    //        if ((Relationship_Snap[_npcId].Exp) > 20 || (Relationship_Snap[_npcId].Exp) < -20)
    //        {
    //            Relationship_Snap.Remove(_npcId);
    //            Relationship_Fixed.Add(_npcId, new Runtime_Relationship(_npcId, _exp));
    //        }
    //    }
    //}

    ///// <summary>
    ///// 获取与某个NPC的关系情况
    ///// </summary>
    ///// <param name="_npcId"></param>
    ///// <returns></returns>
    //public Runtime_Relationship GetRelationship(int _npcId)
    //{
    //    if (Relationship_Fixed.ContainsKey(_npcId))
    //        return Relationship_Fixed[_npcId];
    //    else if (Relationship_Snap.ContainsKey(_npcId))
    //        return Relationship_Snap[_npcId];
    //    return null;
    //}

    ///// <summary>
    ///// 处理身份的增减
    ///// </summary>
    ///// <param name="isAdd">是添加还是删除</param>
    ///// <param name="type">身份类型</param>
    ///// <param name="ID">身份ID</param>
    //public void DealIdentity (bool isAdd, int type, int ID)
    //{
    //    Identity identity = SelectDao.GetDao ().SelectIdentity (ID);
    //    if (isAdd)
    //    {
    //        if (Dic_Idens.ContainsKey (type))
    //        {
    //            Dic_Idens [type].Add (ID, identity);
    //            Dic_Idens.Add (type, Dic_Idens [type]);
    //        }
    //        else
    //        {
    //            Dic_Idens.Add (type, new Dictionary<int, Identity> ());
    //            Dic_Idens [type].Add (ID, identity);
    //        }
    //    }
    //    else
    //    {
    //        Dic_Idens [type].Remove (ID);
    //    }
    //}


}