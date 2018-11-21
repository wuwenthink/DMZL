// ====================================================================================== 
// 文件名         ：    Save_Game.cs                                                         
// 版本号         ：    v2.0.0                                              
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink                                                          
// 创建日期       ：                                                                      
// 最后修改日期   ：    2017-12-12                                                 
// ====================================================================================== 
// 功能描述       ：    存档                                                                
// ======================================================================================


using UnityEngine;

/// <summary>
/// 存档的类
/// </summary>
public class Save_Game : MonoBehaviour
{

    SqliteDbService dbService_game;
    SqliteDbService dbService_save;

    Charactor charactor;
    Bag bag;
    Role_Main role;
    NewsStore newsStore;
    TaskStore taskStore;

    void Init ()
    {
        dbService_game = BasicData.GetDataDbService;
        dbService_save = BasicData.GetSaveDbService;
        bag = Bag.GetInstance;
        newsStore = NewsStore.GetInstance;
        taskStore = TaskStore.GetInstance;
        charactor = Charactor.GetInstance;
    }

    /// <summary>
    /// 存档的方法
    /// <param name="controlNo">存档位置编号  0：自动存档 1：手动存档</param>
    /// </summary>
    public void Save (int controlNo)
    {
        Init ();

        SaveTitle (controlNo);
        SavePlayer (controlNo);

        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<LableTipsUI>().SetAll(true, "System_1", "Tips_Lable11");
    }

    /// 存储存档基础信息（剧本相关）
    private void SaveTitle (int controlNo)
    {
        // 如果表不存在，创建表
        if (!dbService_save.TableExist (Data_Static.TableName_Title))
        {
            dbService_save.ExecuteNonQuery ("CREATE TABLE '" + Data_Static.TableName_Title + "' ('controlNo'  INTEGER,'dramaName'  TEXT,'mapSprite'  TEXT	'identity'  TEXT,'money'  INTEGER,'playerName'  TEXT,'playerHead'  TEXT,'area'  TEXT,'time'  TEXT)");
        }
        // 如果表存在，清除序号为controlNo的数据
        else
        {
            dbService_save.ExecuteNonQuery ("DELETE FROM " + Data_Static.TableName_Title + " WHERE controlNo=" + controlNo + ";");
        }
    }

    /// 存储角色信息
    /// 注：①仓库及其他存储空间的信息需另外存储 
    ///       ②商店这里只存了ID 
    ///       ③表中第一项role的数据为Role_Main表(除身份、学识、技法)所有字段，分号连接
    private void SavePlayer (int controlNo)
    {
        // 如果表不存在，创建表
        if (!dbService_save.TableExist (Data_Static.TableName_Player))
        {
            dbService_save.ExecuteNonQuery ("CREATE TABLE '" + Data_Static.TableName_Player + "' ( 'controlNo'  INTEGER, 'role'  TEXT, 'hunger'  INTEGER, 'health'  INTEGER, 'mood'  INTEGER, 'energy'  INTEGER, 'power'  INTEGER, 'action'  INTEGER, 'workInShop'  INTEGER, 'holidayCount'  INTEGER, 'shop'  TEXT );");
        }
        // 如果表存在，清除序号为controlNo的数据
        else
        {
            dbService_save.ExecuteNonQuery ("DELETE FROM " + Data_Static.TableName_Player + " WHERE controlNo=" + controlNo + ";");
        }
        //int _id, string _commonName, string _gender, int _birthday, string _place, string _story, string _headIcon, string _imageID, int _famous, int _tili, int _wuli, int _zhili, int _poli, int _yili, int _meili, int _shengyu, int _mingwang
        // 逐项存储角色信息
        string roleData = "-1;" + charactor.commonName + ";"  + ";" + charactor.gender + ";" + charactor.birthday + ";" + charactor.place + ";" + charactor.story + ";" + charactor.headIcon
            + ";" + charactor.imageID + ";" + charactor.famous + ";" + charactor.tili + ";" + ";" + charactor.wuli + ";" + ";" + charactor.zhili + ";" + charactor.poli + ";" + charactor.yili
            + ";" + charactor.meili + ";" + charactor.shengyu + ";" + charactor.mingwang;
        string shop = "";
        //if (charactor.Shops != null && charactor.Shops.Count > 0)
        //{
        //    foreach (int key in charactor.Shops.Keys)
        //    {
        //        shop += ";" + key;
        //    }
        //    shop = shop.Substring (1);
        //}
        //else
        //    shop = "-1";
        dbService_save.ExecuteNonQuery ("INSERT INTO " + Data_Static.TableName_Player + " VALUES (" + controlNo + ",'" + roleData + "'," + charactor.hunger + "," +
            charactor.health + "," + charactor.mood + "," + charactor.temp + "," + charactor.hp + "," + charactor.action + "');");
    }

    /// <summary>
    /// 存储角色背包信息
    /// </summary>
    void SaveBag (int controlNo)
    {
        // 清空数据库表
        dbService_save.ExecuteNonQuery ("DELETE FROM Save_Bag_" + controlNo);
        // 将自增序列归零
        dbService_save.ExecuteNonQuery ("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'Save_Bag_" + controlNo + "'; ");

        // 存储金钱
        //dbServer.ExecuteNonQuery("INSERT INTO Save_Bag_"+ controlNo + "(money, gold) VALUES ("+bag.money+", "+bag.gold+")");

        // 存储道具
        foreach (int id in bag.itemDic.Keys)
        {
            //if (Constants.ItemType_CannotStack.Contains (Constants.Items_All [id].type))
            {
                //foreach(int key in bag.itemDic[id].durability.Keys)
                {
                    //dbServer.ExecuteNonQuery("INSERT INTO Save_Bag_" + controlNo + "(item_id, num, durability, money) VALUES(" + id + ", 1, " + bag.itemDic[id].durability[key] + ", -1)");
                }
            }
            //else
            //dbServer.ExecuteNonQuery("INSERT INTO Save_Bag_" + controlNo + "(item_id, num, durability, money) VALUES("+id+", "+ bag.itemDic[id].count+ ", "+bag.itemDic[id].durability[0]+", -1)");
        }

    }


}
