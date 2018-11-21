// ====================================================================================== 
// 文件名         ：    SqliteDbHelper.cs                                                             
// 版本号         ：    v3.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-07-15                                                                  
// 最后修改日期   ：    2017-07-18                                                             
// ====================================================================================== 
// 功能描述       ：    对数据库的基本操作                                                                  
// ======================================================================================

using UnityEngine;
using Mono.Data.Sqlite;
using System;

/// <summary>
/// 数据库的工具类
/// </summary>
public class SqliteDbHelper  {

    /// <summary>
    /// 声明一个连接对象
    /// </summary>
    private SqliteConnection dbConnection;
    /// <summary>
    /// 声明一个操作数据库命令
    /// </summary>
    private SqliteCommand dbCommand;
    /// <summary>
    /// 声明一个读取结果集的一个或多个结果流
    /// </summary>
    private SqliteDataReader reader;
    /// <summary>
    /// 数据库的连接字符串，用于建立与特定数据源的连接
    /// </summary>
    /// <param name="connectionString">数据库的连接字符串，用于建立与特定数据源的连接</param>
    public SqliteDbHelper(string connectionString)
    {
        OpenDB(connectionString);
        Debug.Log(connectionString);
    }
    public void OpenDB(string connectionString)
    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            Debug.Log("Connected to db");
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }
    }
    /// <summary>
    /// 关闭连接
    /// </summary>
    public void CloseSqlConnection()
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = null;
        if (reader != null)
        {
            reader.Dispose();
        }
        reader = null;
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
        Debug.Log("Disconnected from db.");
    }

    /// <summary> 
    /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
    /// </summary> 
    /// <param name="sql">要执行的增删改的SQL语句</param> 
    /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
    /// <returns></returns> 
    public int ExecuteNonQuery(string sql)
    {
        if(dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = new SqliteCommand(sql, dbConnection);
        return dbCommand.ExecuteNonQuery();
    }

    /// <summary> 
    /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
    /// </summary> 
    /// <param name="sql">要执行的查询语句</param> 
    /// <returns></returns> 
    public SqliteDataReader ExecuteReader(string sql)
    {
        if(dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = new SqliteCommand(sql, dbConnection);
        return dbCommand.ExecuteReader();
    }

}


