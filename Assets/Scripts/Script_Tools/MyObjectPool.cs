// ====================================================================================== 
// 文件名         ：    MyObjectPool.cs                                                         
// 版本号         ：    v2.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-7-20                                                                  
// 最后修改日期   ：    2017-8-3                                                            
// ====================================================================================== 
// 功能描述       ：    对象池                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 对象池的类
/// </summary>
public class MyObjectPool<T> where T : class, new()
{
    /// <summary>
    /// 存储对象用的字典
    /// </summary>
    private Dictionary<int, T> objectDictionary = new Dictionary<int, T>();
    /// <summary>
    /// 这个对象池允许存在的对象数目
    /// </summary>
    private int num = 1;

    /// <summary>
    /// 对象池的构造方法
    /// </summary>
    /// <param name="num">最大允许的数目</param>
    public MyObjectPool(int num) 
    {
        this.num = num;
    }

    /// <summary>
    /// 创建一个新对象的方法
    /// </summary>
    /// <returns></returns>
    public T New(List<int> reservedIds)
    {
        if(objectDictionary.Count < num)
            return new T();
        foreach(int id in objectDictionary.Keys)
        {
            if(!reservedIds.Contains(id))
            {
                T t = objectDictionary[id];
                objectDictionary.Remove(id);
                return t;
            }
        } 
        return null;
    }

    /// <summary>
    /// 将处理完的对象重新压入队列
    /// </summary>
    /// <param name="t">要压入的对象</param>
    public void Store(int id, T t)
    {
        objectDictionary.Add(id, t);
    } 

    /// <summary>
    /// 清空对象池
    /// </summary>
    public void ResetAll()
    {
        objectDictionary.Clear();
    }

    /// <summary>
    /// 判断对象是否已经在对象池内
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool isContain(int id)
    {
        return objectDictionary.ContainsKey(id);
    }

}
