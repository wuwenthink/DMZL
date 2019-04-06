using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class DbAccess
    {

        /// <summary>
        /// 声明一个连接对象
        /// </summary>
        public static SqliteConnection dbConnection;

        /// <summary>
        /// 声明一个操作数据库命令
        /// </summary>
        private SqliteCommand dbCommand;

        /// <summary>
        /// 声明一个读取结果集的一个或多个结果流
        /// </summary>
        private SqliteDataReader reader;

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="dbPath">数据库地址</param>
        public void OpenDB ( string dbPath )
        {
            try
            {
                dbConnection = new SqliteConnection("Data Source = " + dbPath);
                dbConnection.Open();
                Debug.Log("Connected to db");
            }
            catch ( Exception e )
            {
                string temp1 = e.ToString();
                Debug.Log(temp1);
            }
        }

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery ( string sql )
        {
            try
            {
                if ( dbCommand != null )
                {
                    dbCommand.Dispose();
                }
                dbCommand = new SqliteCommand(sql,dbConnection);
                return dbCommand.ExecuteNonQuery();
            }
            catch ( Exception e )
            {
                Debug.LogError("SQL语句" + sql + "错误原因" + e.ToString());
                return 0;
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <returns></returns> 
        public SqliteDataReader ExecuteReader ( string sql )
        {
            try
            {
                if ( dbCommand != null )
                {
                    dbCommand.Dispose();
                }
                dbCommand = new SqliteCommand(sql,dbConnection);
                return dbCommand.ExecuteReader();
            }
            catch ( Exception e )
            {
                Debug.LogError("SQL语句" + sql + "错误原因" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close ()
        {
            if ( dbCommand != null )
            {
                dbCommand.Dispose();
            }
            dbCommand = null;
            if ( reader != null )
            {
                reader.Dispose();
            }
            reader = null;
            if ( dbConnection != null )
            {
                dbConnection.Close();
            }
            dbConnection = null;
            Debug.Log("Disconnected from db.");
        }
    }
}