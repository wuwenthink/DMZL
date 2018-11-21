// ====================================================================================== 
// 文件名         ：    ProduceStore.cs                                                         
// 版本号         ：    v2.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-9-1                                                                  
// 最后修改日期   ：    2017-9-29 21:58:12                                                            
// ====================================================================================== 
// 功能描述       ：    配方存储                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 配方存储
/// </summary>
public class ProduceStore
{
    private static ProduceStore _instance;
    public static ProduceStore GetInstance
    {
        get
        {
            return _instance;
        }
    }

    public Dictionary<int, Dictionary<int, Produce_Runtime>> Store;
    public List<int> TypeList;

    public ProduceStore ()
    {
        _instance = this;

        GetTypes ();

        Store = new Dictionary<int, Dictionary<int, Produce_Runtime>> ();

        foreach (int type in TypeList)
        {
            Store.Add (type, new Dictionary<int, Produce_Runtime> ());
        }

    }

    private void GetTypes ()
    {
        TypeList = new List<int> ();

        var produce_All = SelectDao.GetDao ().SelectAllProduce ();

        foreach (int key in produce_All.Keys)
        {
            if (!TypeList.Contains (produce_All [key].skillType))
            {
                TypeList.Add (produce_All [key].skillType);
            }
        }
    }

    /// <summary>
    /// 加载一个配方（用于读档）
    /// </summary>
    /// <param name="_id"></param>
    public void Load_Produce (int _id)
    {
        MakeRecipe produce = SelectDao.GetDao ().SelectProduce (_id);
        int type = produce.skillType;
        Store [type].Add (_id, new Produce_Runtime (produce, true));
    }

    /// <summary>
    /// 加载所有未解锁的配方（用于读档）
    /// </summary>
    public void Load_otherProduce ()
    {
        var produce_All = SelectDao.GetDao ().SelectAllProduce ();
        foreach (int key in produce_All.Keys)
        {
            int type = produce_All [key].skillType;
            if (!Store [type].ContainsKey (key))
            {
                Store [type].Add (key, new Produce_Runtime (produce_All [key]));
            }
        }
    }

    /// <summary>
    /// 解锁一个配方
    /// </summary>
    /// <param name="_id"></param>
    public void UnlockProduce (int _id)
    {
        int type = SelectDao.GetDao ().SelectProduce (_id).skillType;
        if (Store [type].ContainsKey (_id))
        {
            Store [type] [_id].IsUnlocked = true;
        }

        Sort (type);
    }

    /// <summary>
    /// 排序一种类目，并将解锁过的放在前面
    /// </summary>
    /// <param name="_type"></param>
    private void Sort (int _type)
    {

        if (Store [_type].Count > 0)
        {
            List<KeyValuePair<int, Produce_Runtime>> lst = new List<KeyValuePair<int, Produce_Runtime>> ();
            foreach (KeyValuePair<int, Produce_Runtime> item in Store [_type])
            {
                if (item.Value.IsUnlocked)
                    lst.Add (item);
            }
            foreach (KeyValuePair<int, Produce_Runtime> item in Store [_type])
            {
                if (!item.Value.IsUnlocked)
                    lst.Add (item);
            }

            Store [_type].Clear ();

            foreach (KeyValuePair<int, Produce_Runtime> item in lst)
                Store [_type].Add (item.Key, item.Value);
        }

    }
}
