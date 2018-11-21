// ====================================================================================== 
// 文件名         ：    News.cs                                                         
// 版本号         ：    v1.1.0                                                
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-21                                                                  
// 最后修改日期   ：    2017-11-18                                                       
// ====================================================================================== 
// 功能描述       ：    消息                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 消息
/// </summary>
public class News
{
    public int baseId { get; private set; }
    public string name { get; private set; }
    public int type { get; private set; }
    public string des { get; private set; }
    public int type_NPC { get; private set; }
    public List<int> value_NPC { get; private set; }
    public int type_Place { get; private set; }
    public List<int> value_Place { get; private set; }
    public int time { get; private set; }
    public int taskId { get; private set; }
    public List<int> info_Prop { get; private set; }
    public List<int> info_Identity { get; private set; }
    public List<int> info_Building { get; private set; }
    public List<int> info_Org { get; private set; }
    public List<int> info_Force { get; private set; }

    public News (int _id)
    {
        var news = SelectDao.GetDao ().SelectNews (_id);

        baseId = _id;
        name = news.name;
        type = news.type;
        des = news.des;
        type_NPC = news.type_NPC;
        value_NPC = news.value_NPC;
        type_Place = news.type_Place;
        value_Place = news.value_Place;
        time = news.time;
        taskId = news.taskId;
        info_Prop = news.info_Prop;
        info_Identity = news.info_Identity;
        info_Building = news.info_Building;
        info_Org = news.info_Org;
        info_Force = news.info_Force;
    }

    public News (int _id, string _name, int _type, string _des, int _type_NPC, string _value_NPC, int _type_Place, string _value_Place, int _time, int _taskId, string _info_Prop, string _info_Identity, string _info_Building, string _info_Org, string _info_Force)
    {
        baseId = _id;
        name = _name;
        type = _type;
        des = _des;
        type_NPC = _type_NPC;
        if (!_value_NPC.Equals ("0"))
        {
            value_NPC = new List<int> ();
            string [] reg = _value_NPC.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                value_NPC.Add (tempId);
            }
        }
        else
            value_NPC = null;
        type_Place = _type_Place;
        if (!_value_Place.Equals ("0"))
        {
            value_Place = new List<int> ();
            string [] reg = _value_Place.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                value_Place.Add (tempId);
            }
        }
        else
            value_Place = null;
        time = _time;
        taskId = _taskId;
        if (!_info_Prop.Equals ("0"))
        {
            info_Prop = new List<int> ();
            string [] reg = _info_Prop.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                info_Prop.Add (tempId);
            }
        }
        else
            info_Prop = null;
        if (!_info_Identity.Equals ("0"))
        {
            info_Identity = new List<int> ();
            string [] reg = _info_Identity.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                info_Identity.Add (tempId);
            }
        }
        else
            info_Identity = null;
        if (!_info_Building.Equals ("0"))
        {
            info_Building = new List<int> ();
            string [] reg = _info_Building.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                info_Building.Add (tempId);
            }
        }
        else
            info_Building = null;
        if (!_info_Org.Equals ("0"))
        {
            info_Org = new List<int> ();
            string [] reg = _info_Org.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                info_Org.Add (tempId);
            }
        }
        else
            info_Org = null;
        if (!_info_Force.Equals ("0"))
        {
            info_Force = new List<int> ();
            string [] reg = _info_Force.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                info_Force.Add (tempId);
            }
        }
        else
            info_Force = null;
    }


}
