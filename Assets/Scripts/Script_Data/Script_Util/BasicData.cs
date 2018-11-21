// ======================================================================================
// 文 件 名 称：BasicData.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-26 21:34:18
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-26 21:34:18
// ======================================================================================
// 功 能 描 述：基础数据处理
// ======================================================================================

public class BasicData
{
    private static SqliteDbService _dbService_data;
    private static SqliteDbService _dbService_save;

    public static SqliteDbService GetDataDbService
    {
        get
        {
            if(_dbService_data == null)
            {
                _dbService_data = new SqliteDbService ();
                _dbService_data.ConnectionGameData ();
            }
            return _dbService_data;
        }
    }

    public static SqliteDbService GetSaveDbService
    {
        get
        {
            if (_dbService_save == null)
            {
                _dbService_save = new SqliteDbService ();
                _dbService_save.ConnectionSaveData ();
            }
            return _dbService_save;
        }
    }
}