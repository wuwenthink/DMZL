// ====================================================================================== 
// 文件名         ：     SqliteDbService.cs                                                        
// 版本号         ：     v1.3.0                                                
// 作者           ：     xic                                                         
// 创建日期       ：     2017-07-18                                                                 
// 最后修改日期   ：     2017-10-11 12:03:34                                                           
// ====================================================================================== 
// 功能描述       ：     连接数据库，对数据库具体表的增删改查处理                                                                 
// ====================================================================================== 

using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

/// <summary>
/// 数据库具体操作的类
/// </summary>
public class SqliteDbService {

    private SqliteDbHelper db;

    /// <summary>
    /// 创建一个游戏数据的数据库连接，游戏加载时调用一次即可
    /// </summary>
    public void ConnectionGameData()
    {
        db = new SqliteDbHelper("Data Source="+ Application.streamingAssetsPath + "/GameData.db;");
    }

    /// <summary>
    /// 创建存档文件并连接存档数据库
    /// </summary>
    public void ConnectionSaveData ()
    {
        string path = Application.dataPath + "/SaveData/SaveData.sqlite";
        if (!File.Exists (path))
            SqliteConnection.CreateFile (path);
        db = new SqliteDbHelper ("Data Source=" + path);
    }

    /// <summary>
    /// 关闭数据库，结束游戏时调用
    /// </summary>
    public void CloseConn()
    {
        db.CloseSqlConnection();
    }

    /// <summary>
    /// 判断表是否存在
    /// </summary>
    /// <param name="_tableName"></param>
    /// <returns></returns>
    public bool TableExist(string _tableName)
    {
        int result = -1;
        SqliteDataReader reader = ExecuteReader ("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='"+_tableName+"' ");
        while(reader.Read())
        {
            result = reader.GetInt32 (0);
        }
        return result > 0 ? true : false;
    }

    /// <summary> 
    /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
    /// </summary> 
    /// <param name="sql">要执行的增删改的SQL语句</param> 
    /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
    /// <returns></returns> 
    public int ExecuteNonQuery(string sql)
    {
        return db.ExecuteNonQuery(sql);
    }

    /// <summary> 
    /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
    /// </summary> 
    /// <param name="sql">要执行的查询语句</param> 
    /// <returns></returns> 
    public SqliteDataReader ExecuteReader(string sql)
    {
        return db.ExecuteReader(sql);
    }

}
