// ====================================================================================== 
// 文件名         ：    RunTime_Data.cs                                                         
// 版本号         ：    v1.0.1                                                 
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-17                                                                  
// 最后修改日期   ：    2017-11-8                                                      
// ====================================================================================== 
// 功能描述       ：    用于存储所有运行时数据                                                                  
// ====================================================================================== 


using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 运行时数据，包括所有只有玩家角色拥有的数据
/// </summary>
public class RunTime_Data
{
    /// <summary>
    /// UI对象池，记录已经打开的UI界面
    /// </summary>
    public static Dictionary<string, GameObject> UI_Pool;

    //public static int Npc_Sequence;
    public static int News_Sequence;
    public static int Task_Sequence;

    /// <summary>
    /// 角色当前所在的场景id
    /// </summary>
    public static int currSceneId;
    /// <summary>
    /// 当前区域的id
    /// </summary>
    public static int currAreaId;
    /// <summary>
    /// 地图中所有的场景列表（用于存储）
    /// </summary>
    public static Dictionary<int,MapScene> Dic_MapScene;
    /// <summary>
    /// 当前的我方队伍
    /// </summary>
    public static List<int> team_Our;
    /// <summary>
    /// 当前的敌方队伍
    /// </summary>
    public static List<int> team_Enemy;

    /// <summary>
    /// 记录固定NPC
    /// （ID对应存档表中固定NPC的ID）
    /// </summary>
    public static Dictionary<int, Charactor> RolePool;

    /// <summary>
    /// 记录所有的商店
    /// </summary>
    public static Dictionary<int, Business_Runtime_Shop> ShopDic;

    ///// <summary>
    ///// 记录所有的牙行
    ///// </summary>
    //public static Dictionary<int, Business_Runtime_Broker> BrokerDic;

    /// <summary>
    /// 工作的店的ID
    /// </summary>
    public int WorkInShopId = -1;

    ///// <summary>
    ///// 剩余的假期天数（正在休的，每天结束后要减1）
    ///// </summary>
    //public int HolidayCount;

    /// <summary>
    /// 拥有的仓库集合
    /// </summary>
    public Dictionary<int, Business_Runtime_Warehouse> Warehouses;

    /// <summary>
    /// 拥有的商店集合
    /// </summary>
    public Dictionary<int, Business_Runtime_Shop> Shops;

    ///// <summary>
    ///// 货郎的售卖工具：挑担，推车等存储空间内的物品集合
    ///// </summary>
    //public Dictionary<int, int> PackmanTool;

    ///// <summary>
    ///// 当前运输器具的空间内的物品集合
    ///// </summary>
    //public Dictionary<int, int> TransTool;

    /// <summary>
    /// 与玩家关联的存储器具（箱子，衣柜等）内部空间物品的集合
    /// </summary>
    public Dictionary<int, Dictionary<int, int>> ItemBoxes;


    /// <summary>
    /// 进入新游戏时调用
    /// </summary>
    public void Init ()
    {
        UI_Pool = new Dictionary<string, GameObject> ();
        UI_Pool.Add ("org", null);
        UI_Pool.Add ("iden", null);
        UI_Pool.Add ("force", null);
        UI_Pool.Add ("area", null);
        UI_Pool.Add ("role", null); // RoleInfoUI
        UI_Pool.Add ("skill", null);

        RolePool = new Dictionary<int, Charactor> ();
        ShopDic = new Dictionary<int, Business_Runtime_Shop>();
        //BrokerDic = new Dictionary<int, Business_Runtime_Broker> ();

        team_Our = new List<int>();
        team_Enemy = new List<int>();


        new Bag ();
        //new Bussiness_Account ();
        //new NewsStore ();
        //new TaskStore ();
        //new ProduceStore ();

        //new Temp_Data ();
    }


    /// <summary>
    /// 计算角色负重
    /// </summary>
    /// <returns></returns>
    public int GetLoadLimit()
    {
        return 200;
    }

    /// <summary>
    /// 得到一个仓库
    /// </summary>
    public void AddWarehouse(int _id, int _type)
    {
        Warehouses.Add(_id, new Business_Runtime_Warehouse(_type, _id));
    }

    /// <summary>
    /// 得到一个商店
    /// </summary>
    public void AddShop(int _id, string _name, int _tradeId, int _hostId, int _businessEnveromentId, int _businessAreaId)
    {
        var shop = new Business_Runtime_Shop(_id, _name, _tradeId, _hostId, _businessEnveromentId, _businessAreaId);
        Shops.Add(_id, shop);
        RunTime_Data.ShopDic.Add(_id, shop);
    }

    /// <summary>
    /// 得到一个工作
    /// </summary>
    /// <param name="_shopId"></param>
    public void AddJob(int _shopId, int _postId, int _salary)
    {
        WorkInShopId = _shopId;
        var post = RunTime_Data.ShopDic[_shopId].Post;
        if (!post.ContainsKey(_postId))
            post.Add(_postId, new Dictionary<int, Worker>() { { -1, new Worker(-1, _salary) } });
        else
            post[_postId].Add(-1, new Worker(-1, _salary));
    }

    ///// <summary>
    ///// 获得假期
    ///// </summary>
    //public void AddHoliday(int _dayCount)
    //{
    //    HolidayCount = _dayCount;
    //}

    /// <summary>
    /// 查询主角在某商店的职位ID
    /// </summary>
    /// <returns></returns>
    public int GetPostId(int _shopId)
    {
        var shop = RunTime_Data.ShopDic[_shopId];
        if (Shops.ContainsKey(_shopId))
            return 301;
        foreach (var postId in shop.Post.Keys)
        {
            foreach (var roleId in shop.Post[postId].Keys)
            {
                if (roleId == -1)
                    return postId;
            }
        }
        return -1;
    }

}
