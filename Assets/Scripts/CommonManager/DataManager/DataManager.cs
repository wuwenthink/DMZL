using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 数据存储结构体
    /// </summary>
    public struct DBdata
    {
        /// <summary>
        /// 读取语句
        /// </summary>
        public string sql;

        public object[][] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">读取语句</param>
        /// <param name="data">数据</param>
        public DBdata ( string sql,object[][] data )
        {
            this.sql = sql;
            this.data = data;
        }

    }

    /// <summary>
    /// 数据管理器
    /// </summary>
    public class DataManager :Singleton<DataManager>
    {
        /// <summary>
        /// 数据文件地址
        /// </summary>
        string DataPath = Application.streamingAssetsPath + "/Config/DMZL_Data.db;";

        /// <summary>
        /// 数据缓存
        /// </summary>
        List<DBdata> dataCache;

        /// <summary>
        /// 数据库访问器
        /// </summary>
        DbAccess access;

        protected override void Initialize ()
        {
            access = new DbAccess();
            access.OpenDB(DataPath);
            //实例数据缓存
            dataCache = new List<DBdata>(10);
        }
        ~DataManager ()
        {
            access.Close();
        }

        /// <summary>
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object[][] ReadData ( string sql )
        {
            //从后遍历缓存，如果存有该条数据，则返回
            int count = dataCache.Count;
            for ( int i = count - 1 ; i >= 0 ; i-- )
            {
                if ( dataCache[i].sql == sql )
                {
                    //将获取到的数据移动到末位
                    dataCache.Add(dataCache[i]);
                    dataCache.RemoveAt(i);
                    return dataCache[count - 1].data;
                }
            }

            //从数据库读取数据进入缓存
            using ( SqliteDataReader reader = access.ExecuteReader(sql) )
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
                //从数据库添加数据至缓存
                dataCache.Add(new DBdata(sql,lineList.ToArray()));
                //如果保存条数大于10条，则剔除首位
                if ( count >= 10 )
                {
                    dataCache.RemoveAt(0);
                    count--;
                }
                //返回最新添加的数据
                return dataCache[count].data;
            }
            //return access.ExecuteReader(sql);
        }

    }
}
