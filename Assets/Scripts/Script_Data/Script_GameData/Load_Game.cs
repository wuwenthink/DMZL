// ====================================================================================== 
// 文件名         ：    Load_Game.cs                                                         
// 版本号         ：    v2.0.1.0                                                  
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink                                                          
// 创建日期       ：                                                                      
// 最后修改日期   ：    2017-8-25                                                            
// ====================================================================================== 
// 功能描述       ：    读档                                                                
// ======================================================================================

using Mono.Data.Sqlite;
using UnityEngine;

/// <summary>
/// 读档
/// </summary>
public class Load_Game : MonoBehaviour
{
    Charactor charactor;
    Bag bag;

    SqliteDataReader reader;
    SqliteDbService dbService_save;

    /// <summary>
    /// 读档
    /// </summary>
    public void Load (int controlNo)
    {
        dbService_save = BasicData.GetSaveDbService;

        charactor = Charactor.GetInstance;
        bag = Bag.GetInstance;

        ReadPlayer (controlNo);
        ReadBagInfo ();
    }

    /// 读取角色信息
    private void ReadPlayer (int controlNo)
    {
        reader = dbService_save.ExecuteReader ("SELECT * FROM " + Data_Static.TableName_Player + " WHERE controlNo=" + controlNo + ";");
        while (reader.Read ())
        {
            string [] reg = reader.GetString (1).Split (';');
            //[0]int _id, [1]string _commonName, [2]string _gender, [3]int _birthday, [4]string _place, [5]string _story, [6]string _headIcon, [7]string _imageID,
            //[8]int _famous, [9]int _tili, [10]int _wuli, [11]int _zhili, [12]int _poli, [13]int _yili, [14]int _meili, [15]int _shengyu, [16]int _mingwang
            charactor = new Charactor (int.Parse (reg [0]), reg [1], reg [2], int.Parse(reg [3]), reg [4],reg [5], reg [6], reg [7], int.Parse(reg [8]),  int.Parse(reg [9]),
                int.Parse (reg [10]), int.Parse (reg [11]), int.Parse (reg [12]), int.Parse (reg [13]), int.Parse (reg [14]), int.Parse (reg [15]),int.Parse (reg [16]))
            {
                hunger = reader.GetInt32 (2),
                health = reader.GetInt32 (3),
                mood = reader.GetInt32 (4),
                temp = reader.GetInt32 (5),
                hp = reader.GetInt32 (6),
                action = reader.GetInt32 (7),
                //WorkInShopId = reader.GetInt32 (8),
                //HolidayCount = reader.GetInt32 (9)
            };

            //string shop = reader.GetString (10);
            //if (!shop.Equals ("-1"))
            //{
            //    reg = shop.Split (';');
            //    foreach (string item in reg)
            //    {
            //        charactor.Shops.Add (int.Parse (item), null);
            //    }
            //}
        }
    }

    /// <summary>
    /// 读取背包信息
    /// </summary>
    void ReadBagInfo ()
    {
        reader = BasicData.GetDataDbService.ExecuteReader ("SELECT item_id, num, durability, money, gold FROM Save_Bag_0;");

        while (reader.Read ())
        {
            if (reader.GetInt32 (3) != -1)
            {
                bag.Load_Bag (reader.GetInt32 (3), reader.GetInt32 (4));
            }
            else
            {
                //bag.Load_Bag(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
            }
        }
    }

}
