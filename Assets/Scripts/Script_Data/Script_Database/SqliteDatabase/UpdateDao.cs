// ======================================================================================
// 文 件 名 称：UpdateDao.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-10 18:09:06
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-12-15
// ======================================================================================
// 功 能 描 述：数据库：插入(修改)
// ======================================================================================


public class UpdateDao
{
    private SqliteDbService dbService_save;
    private static UpdateDao updateDao;

    private UpdateDao ()
    {
        dbService_save = BasicData.GetSaveDbService;
    }

    /// <summary>
    /// 单例
    /// </summary>
    /// <returns></returns>
    public static UpdateDao GetDao ()
    {
        if (updateDao == null)
            updateDao = new UpdateDao ();
        return updateDao;
    }

    public void UpdateRoom (BuildingPart _buildingPart)
    {
        // 如果表不存在，创建表
        if (!dbService_save.TableExist (Data_Static.TableName_BuildingPart))
        {
            dbService_save.ExecuteNonQuery ("CREATE TABLE '" + Data_Static.TableName_BuildingPart + "' ('position'  TEXT, 'sceneId'  INTEGER, 'bpModelId'  INTEGER, 'roomId'  INTEGER PRIMARY KEY AUTOINCREMENT, 'rModelPrefab'  TEXT, 'rModelId'  INTEGER); ");
        }

        // 插入新数据
        string sql = "INSERT INTO " + Data_Static.TableName_BuildingPart + " (position, sceneId, bpModelId, rModelPrefab, rModelId) VALUES ('" + _buildingPart.position.x + "," + _buildingPart.position.y + "', " + _buildingPart.sceneId + ", " + _buildingPart.bpModelId + ", '" + _buildingPart.rModelPrefab + "', " + _buildingPart.rModelId + ")";
        dbService_save.ExecuteNonQuery (sql);
    }

}
