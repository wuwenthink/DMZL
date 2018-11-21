// ======================================================================================
// 文 件 名 称：Temp_Data.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-15 18:29:03
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-15 18:29:03
// ======================================================================================
// 功 能 描 述：临时数据，记录暂时的调整，每天结束后记录到实际位置
// ======================================================================================

using System.Collections.Generic;

public class Temp_Data
{
    private static Temp_Data _instance;
    public static Temp_Data GetInstance
    {
        get
        {
            return _instance;
        }
    }

    public Temp_Data ()
    {
        _instance = this;
        TempEmployee = new Dictionary<int, Dictionary<int, Dictionary<int, Worker>>> ();
    }

    /// <summary>
    /// 临时的（当日调整的）职员表
    /// (商店ID,(职位ID,(人物ID,员工具体信息)))
    /// </summary>
    public Dictionary<int, Dictionary<int, Dictionary<int, Worker>>> TempEmployee;


    /// <summary>
    /// 调整员工的职位
    /// </summary>
    /// <param name="_shopId">商店ID</param>
    /// <param name="_roleId">人物ID（主角填-1）</param>
    /// <param name="_oldPostId">旧的职位ID（没有填-1）</param>
    /// <param name="_newPostId">新的职位ID（开除填-1）</param>
    public void AdjustEmployee (int _shopId, int _roleId, int _oldPostId, int _newPostId)
    {
        if (!TempEmployee.ContainsKey (_shopId))
            TempEmployee.Add (_shopId, RunTime_Data.ShopDic [_shopId].Post);
        if (_oldPostId != -1)
        {
            TempEmployee [_shopId] [_oldPostId].Remove (_roleId);
            if (TempEmployee [_shopId] [_oldPostId].Count <= 0)
                TempEmployee [_shopId].Remove (_oldPostId);
        }
        if (_newPostId == -1)
            return;
        var salary = SelectDao.GetDao ().SelecRole_Job (_newPostId).price;
        if (!TempEmployee [_shopId].ContainsKey (_newPostId))
        {
            TempEmployee [_shopId].Add (_newPostId, new Dictionary<int, Worker>
            {
                { _roleId, new Worker(_roleId, salary )}
            });

            DictionarySort (TempEmployee [_shopId]);
        }
        else
            TempEmployee [_shopId] [_newPostId].Add (_roleId, new Worker (_roleId, salary));
    }

    /// <summary>
    /// 调整薪水
    /// </summary>
    /// <param name="_shopId">商店ID</param>
    /// <param name="_roleId">人物ID（主角填-1）</param>
    /// <param name="_newSalary">调整后的薪水</param>
    public void AdjustSalary (int _shopId, int _roleId, int _newSalary)
    {
        if (!TempEmployee.ContainsKey (_shopId))
            TempEmployee.Add (_shopId, RunTime_Data.ShopDic [_shopId].Post);
        foreach (var item in TempEmployee [_shopId].Values)
        {
            if (item.ContainsKey (_roleId))
            {
                item [_roleId].Salary = _newSalary;
                break;
            }
        }
    }

    // 字典按Key排序
    private Dictionary<int, Dictionary<int, Worker>> DictionarySort (Dictionary<int, Dictionary<int, Worker>> dic)
    {
        if (dic.Count > 0)
        {
            List<KeyValuePair<int, Dictionary<int, Worker>>> lst = new List<KeyValuePair<int, Dictionary<int, Worker>>> (dic);
            lst.Sort (delegate (KeyValuePair<int, Dictionary<int, Worker>> s1, KeyValuePair<int, Dictionary<int, Worker>> s2)
            {
                return s1.Key.CompareTo (s2.Key);
            });
            dic.Clear ();

            foreach (KeyValuePair<int, Dictionary<int, Worker>> kvp in lst)
                dic.Add (kvp.Key, kvp.Value);
            return dic;
        }
        return null;
    }
}
