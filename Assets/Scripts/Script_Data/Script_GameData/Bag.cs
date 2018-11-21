// ======================================================================================
// 文件名         ：    Bag.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-10
// 最后修改日期   ：   2017-10-03 21:49:45
// ======================================================================================
// 功能描述       ：    角色背包
// ======================================================================================

using System.Collections.Generic;

/// <summary>
/// 角色背包
/// </summary>
public class Bag
{
    private static Bag _instance;
    public static Bag GetInstance
    {
        get
        {
            return _instance;
        }
    }


    /// <summary>
    /// 拥有的钱数（银子和铜板）
    /// </summary>
    public int money { get; private set; }
    
    /// <summary>
    /// 拥有的物品集合
    /// </summary>
    public Dictionary<int, ItemInBag> itemDic { get; private set; }

    /// <summary>
    /// 背包的总大小
    /// </summary>
    public int MaxCount { get; private set; }

    /// <summary>
    /// 背包物品叠加上限
    /// </summary>
    public int MaxOverlay_Item = 99999;

    /// <summary>
    /// 背包物品叠加上限
    /// </summary>
    public int MaxOverlay_Money = 99999;

    /// <summary>
    /// 创建背包时调用
    /// </summary>
    public Bag ()
    {
        _instance = this;
        itemDic = new Dictionary<int, ItemInBag> ();
    }

    /// <summary>
    /// 将itemDic按type排序
    /// </summary>
    public void SortBag ()
    {
        if (itemDic.Count > 0)
        {
            List<KeyValuePair<int, ItemInBag>> lst = new List<KeyValuePair<int, ItemInBag>> (itemDic);
            lst.Sort (delegate (KeyValuePair<int, ItemInBag> s1, KeyValuePair<int, ItemInBag> s2)
             {
                 return s1.Value.item.type.CompareTo (s2.Value.item.type);
             });
            itemDic.Clear ();

            foreach (KeyValuePair<int, ItemInBag> kvp in lst)
                itemDic.Add (kvp.Key, kvp.Value);
        }
    }

    /// <summary>
    /// 加载一种道具
    /// 用于读档时重建背包
    /// </summary>
    /// <param name="_id">道具id</param>
    /// <param name="_count">数量</param>
    /// <param name="_durability">耐久度</param>
    public void Load_Bag (int _id, int _count)
    {
        Item item = Constants.Items_All [_id];

        if (!itemDic.ContainsKey (_id))
        {
            itemDic.Add (_id, new ItemInBag (item, _count));
        }
        else
        {
            itemDic [_id].count++;
        }
    }

    /// <summary>
    /// 检查金钱是否超出上限
    /// </summary>
    /// <param name="_count">要获得的钱数</param>
    /// <returns></returns>
    public bool IsAhead_Money (int _count)
    {
        if (money + _count > MaxOverlay_Item)
            return true;
        return false;
    }

    /// <summary>
    /// 增加金钱
    /// </summary>
    /// <param name="_count">增加的数量</param>
    public void AddMoney (int _count)
    {
        if (IsAhead_Money (_count))
            money = MaxOverlay_Money;
        else
        {
            money += _count;
        }
    }

    /// <summary>
    /// 检查是否拥有足够的金钱
    /// </summary>
    /// <param name="_count">需要的金钱数</param>
    /// <returns></returns>
    public bool HaveEnoughMoney (int _count)
    {
        if (money >= _count)
            return true;
        return false;
    }

    /// <summary>
    /// 检查是否拥有足够的某种货币
    /// </summary>
    /// <param name="_id">货币id</param>
    /// <param name="_count"></param>
    /// <returns></returns>
    public bool HaveEnoughCurrency(int _id, int _count)
    {
        if (itemDic.ContainsKey(_id) && itemDic[_id].count >= _count)
            return true;
        return false;
    }

    /// <summary>
    /// 减少金钱
    /// </summary>
    /// <param name="_count">减少的数量</param>
    public void DecreaseMoney (int _count)
    {
        if (money >= _count)
            money -= _count;
        else
            money = 0;
    }
    RunTime_Data runTime;
    /// <summary>
    /// 检查背包是否超过总数量
    /// </summary>
    /// <param name="_count">要获取的数量</param>
    /// <returns></returns>
    public bool IsAhead_OverCount (int _count)
    {
        MaxCount += _count;
        if (MaxCount > runTime.GetLoadLimit ())
            return true;
        return false;
    }

    /// <summary>
    /// 获取道具
    /// </summary>
    /// <param name="_id">道具id</param>
    /// <param name="_count">获取的数量</param>
    public void GetItem (int _id, int _count)
    {
        Item item = Constants.Items_All [_id];
    
        if (!HaveItem (_id))
            itemDic.Add (_id, new ItemInBag (item, _count));
        else
        {
            itemDic [_id].count+= _count;
        }

        // 改变背包个数
        MaxCount += _count;
    }

    /// <summary>
    /// 检查是否拥有足够数量的某个物品
    /// </summary>
    /// <param name="_id">道具id</param>
    /// <param name="_count">需要的数量</param>
    /// <returns></returns>
    public bool HaveItem (int _id, int _count)
    {
        if (HaveItem (_id) && itemDic [_id].count >= _count)
            return true;
        return false;
    }

    /// <summary>
    /// 检查是否拥有某件物品
    /// </summary>
    /// <param name="_id">道具id</param>
    /// <returns></returns>
    public bool HaveItem (int _id)
    {
        if (itemDic.ContainsKey (_id))
            return true;
        return false;
    }

    /// <summary>
    /// 失去道具
    /// </summary>
    /// <param name="_id">道具id</param>
    /// <param name="_count">失去的数量</param>
    public void LoseItem (int _id, int _count)
    {
        if (HaveItem (_id))
        {
            // 如果扣除之后数量为0，则移除此物品
            if (itemDic [_id].count-- <= 0)
            {
                itemDic [_id].Clear ();
                itemDic [_id] = null;
                itemDic.Remove (_id);
            }

            // 修改背包重量和体积
            MaxCount -= _count;
        }
        else
            return;        
    }

    /// <summary>
    /// 失去指定道具
    /// </summary>
    /// <param name="objectName">克隆后得到的物体的name：道具id_编号</param>
    /// <param name="_count"></param>
    public void LoseItem (string objectName, int _count)
    {
        // 解析道具id和编号
        int _id = int.Parse (objectName.Split ('_') [0]);
        int key = int.Parse (objectName.Split ('_') [1]);
        // 扣除道具
        itemDic [_id].count -= _count;
        // 如果扣除后数量为0， 移除此物品
        if (itemDic [_id].count <= 0)
        {
            itemDic [_id].Clear ();
            itemDic [_id] = null;
            itemDic.Remove (_id);
        }
    }

    //// 以下是快排算法

    //void Sort(int[] numbers)
    //{
    //    QuickSort(numbers, 0, numbers.Length - 1);
    //}
    //void QuickSort(int[] numbers, int left, int right)
    //{
    //    if(left < right)
    //    {
    //        int middle = numbers[(left + right) / 2];
    //        int i = left - 1;
    //        int j = right + 1;
    //        while(true)
    //        {
    //            while(numbers[++i] < middle) ;

    //            while(numbers[--j] > middle) ;

    //            if(i >= j)
    //                break;

    //            Swap(numbers, i, j);
    //        }

    //        QuickSort(numbers, left, i - 1);
    //        QuickSort(numbers, j + 1, right);
    //    }
    //}

    //void Swap(int[] numbers, int i, int j)
    //{
    //    int number = numbers[i];
    //    numbers[i] = numbers[j];
    //    numbers[j] = number;
    //}
}