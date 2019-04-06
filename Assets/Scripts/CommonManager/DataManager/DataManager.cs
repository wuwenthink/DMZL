using Mono.Data.Sqlite;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 数据管理器
    /// </summary>
    public class DataManager:Singleton<DataManager>
    {
        /// <summary>
        /// 数据文件地址
        /// </summary>
        string DataPath =  Application.streamingAssetsPath + "/Config/DMZL_Data.db;";

        /// <summary>
        /// 数据库访问器
        /// </summary>
        DbAccess access;

        protected override void Initialize ()
        {
            access = new DbAccess();
            access.OpenDB(DataPath);
        }

        /// <summary>
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqliteDataReader ReadData(string sql)
        {
           return access.ExecuteReader(sql);
        }

    }
}
