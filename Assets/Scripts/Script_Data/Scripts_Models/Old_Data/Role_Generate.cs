// ======================================================================================
// 文 件 名 称：Role_Generate.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-18 13:03:07
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-29
// ======================================================================================
// 功 能 描 述：NPC角色生成模板数据
// ======================================================================================


using System.Collections.Generic;

public class Role_Generate
{
    public int id { private set; get; }
    public string name { private set; get; }
    public string des { private set; get; }
    public int type { private set; get; }
    public string typeName { private set; get; }
    public int idenId { private set; get; }
    public List<string> knowledge { private set; get; }
    public List<string> eliteKnowledge { private set; get; }
    public List<string> skill { private set; get; }
    public List<string> eliteSkill { private set; get; }
    public int male { private set; get; }
    public List<int> ageRange { private set; get; }
    public List<string> Zhiye { private set; get; }
    public List<string> ZhiShi { private set; get; }
    public List<string> GuanZhi { private set; get; }
    public List<string> chengHao { private set; get; }
    public List<string> RongXian { private set; get; }
    public List<int> Interaction { private set; get; }
    public List<int> AI { private set; get; }

    public Role_Generate (int _id, string _name, string _des, int _type, string _typeName, int _idenId, string _knowledge, string _eliteKnowledge, string _skill, string _eliteSkill, int _male, string _ageRange, string _Zhiye, string _ZhiShi, string _GuanZhi, string _chengHao, string _RongXian, string _interaction, string _ai)
    {
        id = _id;
        name = _name;
        des = _des;
        type = _type;
        typeName = _typeName;
        idenId = _idenId;
        if (!_knowledge.Equals ("0"))
        {
            knowledge = new List<string> ();
            string [] reg = _knowledge.Split (';');
            foreach (string str in reg)
            {
                knowledge.Add (str);
            }
        }
        else
            knowledge = null;
        if (!_eliteKnowledge.Equals ("0"))
        {
            eliteKnowledge = new List<string> ();
            string [] reg = _eliteKnowledge.Split (';');
            foreach (string str in reg)
            {
                eliteKnowledge.Add (str);
            }
        }
        else
            eliteKnowledge = null;
        if (!_skill.Equals ("0"))
        {
            skill = new List<string> ();
            string [] reg = _skill.Split (';');
            foreach (string str in reg)
            {
                skill.Add (str);
            }
        }
        else
            skill = null;
        if (!_eliteSkill.Equals ("0"))
        {
            eliteSkill = new List<string> ();
            string [] reg = _eliteSkill.Split (';');
            foreach (string str in reg)
            {
                eliteSkill.Add (str);
            }
        }
        else
            eliteSkill = null;
        male = _male;
        if (!_ageRange.Equals ("0"))
        {
            ageRange = new List<int> ();
            string [] reg = _ageRange.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                ageRange.Add (tempId);
            }
        }
        else
            ageRange = null;
        if (!_Zhiye.Equals ("0"))
        {
            Zhiye = new List<string> ();
            string [] reg = _Zhiye.Split (';');
            foreach (string str in reg)
            {
                Zhiye.Add (str);
            }
        }
        else
            Zhiye = null;
        if (!_ZhiShi.Equals ("0"))
        {
            ZhiShi = new List<string> ();
            string [] reg = _ZhiShi.Split (';');
            foreach (string str in reg)
            {
                ZhiShi.Add (str);
            }
        }
        else
            ZhiShi = null;
        if (!_GuanZhi.Equals ("0"))
        {
            GuanZhi = new List<string> ();
            string [] reg = _GuanZhi.Split (';');
            foreach (string str in reg)
            {
                GuanZhi.Add (str);
            }
        }
        else
            GuanZhi = null;
        if (!_chengHao.Equals ("0"))
        {
            chengHao = new List<string> ();
            string [] reg = _chengHao.Split (';');
            foreach (string str in reg)
            {
                chengHao.Add (str);
            }
        }
        else
            chengHao = null;
        if (!_RongXian.Equals ("0"))
        {
            RongXian = new List<string> ();
            string [] reg = _RongXian.Split (';');
            foreach (string str in reg)
            {
                RongXian.Add (str);
            }
        }
        else
            RongXian = null;
        if (!_interaction.Equals ("0"))
        {
            Interaction = new List<int> ();
            string [] reg = _interaction.Split (';');
            foreach (string str in reg)
            {
                int index = int.Parse (str);
                Interaction.Add (index);
            }
        }
        else
            AI = null;
        if (!_ai.Equals ("0"))
        {
            AI = new List<int> ();
            string [] reg = _ai.Split (';');
            foreach (string str in reg)
            {
                int index = int.Parse (str);
                AI.Add (index);
            }
        }
        else
            AI = null;
    }


}
