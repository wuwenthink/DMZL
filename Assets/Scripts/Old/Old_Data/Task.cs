// ====================================================================================== 
// 文件名         ：    Task.cs                                                         
// 版本号         ：    v1.1.0                                             
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-22                                                                  
// 最后修改日期   ：    2017-11-19                                                    
// ====================================================================================== 
// 功能描述       ：    任务                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 任务
/// </summary>
public class Task
{
    public int baseId { get; private set; }
    public string name { get; private set; }
    public TaskType type { get; private set; }
    public string des { get; private set; }
    public int lastTaskId { get; private set; }
    public int type_demand { get; private set; }
    public List<string> value_demand { get; private set; }
    public int type_finish { get; private set; }
    public List<string> value_finish { get; private set; }
    public int time { get; private set; }
    public List<string> item_award { get; private set; }
    public List<string> prop_award { get; private set; }
    public List<string> info_prop { get; private set; }
    public List<string> info_identity { get; private set; }
    public List<string> info_building { get; private set; }
    public List<string> info_org { get; private set; }
    public List<string> info_force { get; private set; }

    public bool permit_giveup { get; private set; }

    public Task (int _baseId)
    {
        Task task = SelectDao.GetDao ().SelectTask (_baseId);

        baseId = task.baseId;
        name = task.name;
        type = task.type;
        des = des;
        lastTaskId = task.lastTaskId;
        type_demand = task.type_demand;
        value_demand = task.value_demand;
        type_finish = task.type_finish;
        value_finish = task.value_finish;
        time = task.time;
        item_award = task.item_award;
        prop_award = task.prop_award;
        info_prop = task.info_prop;
        info_identity = task.info_identity;
        info_building = task.info_building;
        info_org = task.info_org;
        info_force = task.info_force;
    }

    public Task (int _id, string _name, int _type, string _des, int _lastTaskId, int _type_demand, string _value_demand, int _type_finish, string _value_finish, int _time, string _item_award, string _prop_award, string _info_prop, string _info_identity, string _info_building, string _info_org, string _info_force, int _permit)
    {
        baseId = _id;
        name = _name;
        type = (TaskType) _type;
        des = _des;
        lastTaskId = _lastTaskId;
        type_demand = _type_demand;
        if (!_value_demand.Equals ("0"))
        {
            value_demand = new List<string> ();
            string [] reg = _value_demand.Split (';');
            foreach (string str in reg)
            {
                value_demand.Add (str);
            }
        }
        else
            value_demand = null;
        type_finish = _type_finish;
        if (!_value_finish.Equals ("0"))
        {
            value_finish = new List<string> ();
            string [] reg = _value_finish.Split (';');
            foreach (string str in reg)
            {
                value_finish.Add (str);
            }
        }
        else
            value_finish = null;
        time = _time;
        if (!_item_award.Equals ("0"))
        {
            item_award = new List<string> ();
            string [] reg = _item_award.Split (';');
            foreach (string str in reg)
            {
                item_award.Add (str);
            }
        }
        else
            item_award = null;
        if (!_prop_award.Equals ("0"))
        {
            prop_award = new List<string> ();
            string [] reg = _prop_award.Split (';');
            foreach (string str in reg)
            {
                prop_award.Add (str);
            }
        }
        else
            prop_award = null;
        if (!_info_prop.Equals ("0"))
        {
            info_prop = new List<string> ();
            string [] reg = _info_prop.Split (';');
            foreach (string str in reg)
            {
                info_prop.Add (str);
            }
        }
        else
            info_prop = null;
        if (!_info_identity.Equals ("0"))
        {
            info_identity = new List<string> ();
            string [] reg = _info_identity.Split (';');
            foreach (string str in reg)
            {
                info_identity.Add (str);
            }
        }
        else
            info_identity = null;
        if (!_info_building.Equals ("0"))
        {
            info_building = new List<string> ();
            string [] reg = _info_building.Split (';');
            foreach (string str in reg)
            {
                info_building.Add (str);
            }
        }
        else
            info_building = null;
        if (!_info_org.Equals ("0"))
        {
            info_org = new List<string> ();
            string [] reg = _info_org.Split (';');
            foreach (string str in reg)
            {
                info_org.Add (str);
            }
        }
        else
            info_org = null;
        if (!_info_force.Equals ("0"))
        {
            info_force = new List<string> ();
            string [] reg = _info_force.Split (';');
            foreach (string str in reg)
            {
                info_force.Add (str);
            }
        }
        else
            info_force = null;
        permit_giveup = _permit == 1 ? true : false;
    }



}
