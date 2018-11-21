// ======================================================================================
// 文件名         ：    Knowledge.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-24
// 最后修改日期   ：    2017-11-18 14:41
// ======================================================================================
// 功能描述       ：    学识
// ======================================================================================

using System.Collections.Generic;

/// <summary>
/// 学识
/// </summary>
public class Knowledge
{
    /// <summary>
    /// 学识id
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 学识名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 学识类型
    /// </summary>
    public KnowledgeClass knowledgeClass { get; private set; }

    /// <summary>
    /// 上级分类id
    /// </summary>
    public KnowledgeType aheadType { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 圣贤（作者）
    /// </summary>
    public string author { get; private set; }

    /// <summary>
    /// 书籍对应物品的id
    /// </summary>
    public int itemId { get; private set; }

    /// <summary>
    /// 成长索引
    /// </summary>
    public Knowledge_Grow grow { get; private set; }

    /// <summary>
    /// 成长途径
    /// </summary>
    public List<string> growWay { get; private set; }

    /// <summary>
    /// 称号列表
    /// </summary>
    public List<int> titleList { get; private set; }

    public Knowledge (int _id)
    {
        var knowledge = SelectDao.GetDao ().SelectKnowledge (_id);
        id = knowledge.id;
        name = knowledge.name;
        knowledgeClass = knowledge.knowledgeClass;
        aheadType = knowledge.aheadType;
        des = knowledge.des;
        author = knowledge.author;
        itemId = knowledge.itemId;
        grow = knowledge.grow;
        growWay = knowledge.growWay;
        titleList = knowledge.titleList;
    }

    public Knowledge (int _id, string _name, int _type, int _aheadType, string _des, string _author, int _itemId, int _grow, string _way1, string _way2, string _way3, string _way4, string _way5, string _titleList)
    {
        id = _id;
        name = _name;
        knowledgeClass = (KnowledgeClass) _type;
        if (knowledgeClass == KnowledgeClass.学问大类)
            aheadType = (KnowledgeType) _id;
        else
            aheadType = (KnowledgeType) _aheadType;
        des = _des;
        author = _author;
        itemId = _itemId;
        grow = SelectDao.GetDao ().SelectKnowledge_Grow (_grow);

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

        if (!_titleList.Equals ("0"))
        {
            titleList = new List<int> ();
            string [] reg = _titleList.Split (';');
            foreach (string str in reg)
            {
                int item = int.Parse (str);
                titleList.Add (item);
            }
        }
        else
            titleList = null;
    }
}