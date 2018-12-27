// ======================================================================================
// 文 件 名 称：Runtime_Relationship.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 21:21:56
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 21:21:56
// ======================================================================================
// 功 能 描 述：运行时：与主角的关系
// ======================================================================================
using System.Collections.Generic;

public class Runtime_Relationship
{
    /// <summary>
    /// NPC的ID
    /// </summary>
    public int NpcId { private set; get; }
    /// <summary>
    /// 当前好感度
    /// </summary>
    public int Exp { private set; get; }
    /// <summary>
    /// 过去30天的关系变化次数
    /// </summary>
    public List<int> list_ChangeTime;

    public enum State {
        keep,//维持状态
        add,//加深状态
        reduce//减弱状态
    }
    /// <summary>
    /// 当前关系状态：变化方式为30天内根据关系变化次数进行判断
    /// </summary>
    public State RelationshipState;
    public Relationship Relationship { private set; get; }
    /// <summary>
    /// 获得关系的文字
    /// </summary>
    /// <returns></returns>
    public string GetStateWord(State re)
    {
        if (re== State.keep)
        {
            return LanguageMgr.GetInstance.GetText("Relation6");
        }
        else if (re == State.add)
        {
            return LanguageMgr.GetInstance.GetText("Relation7");
        }
        else
        {
            return LanguageMgr.GetInstance.GetText("Relation8");
        }
    }

    public Runtime_Relationship (int _npcId, int _exp)
    {
        list_ChangeTime = new List<int>();
        NpcId = _npcId;
        ChangeExp (_exp);
    }

    void SaveTime()
    {
        foreach (var item in list_ChangeTime)
        {
            if (TimeManager.GetDays() - item > 30)
            {
                list_ChangeTime.Remove(item);
            }
        }
        list_ChangeTime.Add(TimeManager.GetDays());
        //给关系状态赋值
        if (list_ChangeTime.Count <= 1)
        {
            RelationshipState = State.reduce;
        }
        else if (list_ChangeTime.Count <= 5)
        {
            RelationshipState = State.keep;
        }
        else
        {
            RelationshipState = State.add;
        }
    }

    /// <summary>
    /// 改变好感度
    /// </summary>
    /// <param name="_exp">改变的好感度，正为加，负为减</param>
    public void ChangeExp (int _exp)
    {
        Exp += _exp;
        DealWithRelationship ();
        SaveTime();
    }

    // 计算关系变化
    private void DealWithRelationship ()
    {
        Relationship = SelectDao.GetDao ().SelectRelationshipByExp (Exp);
    }

}
