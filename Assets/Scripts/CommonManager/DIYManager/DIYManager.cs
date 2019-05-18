using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System;

namespace Common
{
    public class DIYManager :Singleton<DIYManager>
    {
        /// <summary>
        /// 数据文件地址
        /// </summary>
        string DataPath = Application.streamingAssetsPath + "/DMZL_DIY.db;";

        /// <summary>
        /// 数据库访问器
        /// </summary>
        DbAccess access;

        protected override void Initialize ()
        {
            access = new DbAccess();
            access.OpenDB(DataPath);
        }
        ~DIYManager ()
        {
            access.Close();
        }

        /// <summary>
        /// 执行一个查询语句，返回一个object[][]
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object[][] Select ( string SELECT,string FROM,string WHERE )
        {
            //从数据库读取数据
            using ( SqliteDataReader reader = access.ExecuteReader("SELECT " + SELECT + " FROM " + FROM + " WHERE " + WHERE) )
            {
                int lineCount = reader.FieldCount;

                List<object[]> lineList = new List<object[]>();

                while ( reader.Read() )
                {
                    object[] line = new object[lineCount];
                    for ( int i = 0 ; i < lineCount ; i++ )
                    {
                        line[i] = reader.GetValue(i);
                    }
                    lineList.Add(line);
                }
                //返回最新添加的数据
                return lineList.ToArray();
            }
        }

        /// <summary>
        /// 插入基础表（不必输入Id值）
        /// </summary>
        /// <param name="INTO"></param>
        /// <param name="VALUES"></param>
        /// <returns>条目Id值</returns>
        public int InsertBaseFrom ( string INTO,string VALUES )
        {
            //插入语句
            access.ExecuteReader("INSERT INTO " + INTO + " VALUES (NULL," + VALUES + ")");
            //返回插入条目的ID
            return Convert.ToInt32(Select("seq","sqlite_sequence","name= '" + INTO + "'")[0][0]);
        }

        /// <summary>
        /// 插入相关表
        /// </summary>
        /// <param name="INTO"></param>
        /// <param name="VALUES"></param>
        public void InsertRelateFrom ( string INTO,string VALUES )
        {
            //插入语句
            access.ExecuteReader("INSERT INTO " + INTO + " VALUES (" + VALUES + ")");
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="UPDATE"></param>
        /// <param name="SET"></param>
        /// <param name="WHERE"></param>
        public void Updata ( string UPDATE,string SET,string WHERE )
        {
            access.ExecuteReader("UPDATE " + UPDATE + " SET " + SET + " WHERE " + WHERE);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="FROM"></param>
        /// <param name="WHERE"></param>
        public void Delect ( string FROM,string WHERE )
        {
            access.ExecuteReader("DELECT " + FROM + " WHERE " + WHERE);
        }
    }
}
