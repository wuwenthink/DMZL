// ====================================================================================== 
// 文件名         ：    GameObjectPool.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-3                                                                  
// 最后修改日期   ：    2017-8-4                                                            
// ====================================================================================== 
// 功能描述       ：    Unity中实体对象用的对象池                                                                  
// ====================================================================================== 

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity中实体对象的对象池
/// </summary>
public class GameObjectPool<T>
{
    Dictionary<T, int> objectDictionary = new Dictionary<T, int>();

    int num = 1;

    public GameObjectPool(int _num)
    {
        num = _num;
    }

    /// <summary>
    /// 实例化一个对象
    /// 返回的对象处理完应该调用Store方法，重新压入池子
    /// </summary>
    /// <param name="go">被克隆的对象</param>
    /// <param name="vector">生成对象的位置</param>
    /// <param name="rotation">旋转</param>
    /// <param name="flag">复用时是否需要改变prefab</param>
    /// <returns></returns>
    public GameObject New(GameObject go, Vector3 vector, Quaternion rotation, bool flag)
    {
        // 池子不满，实例化一个新对象
        if(objectDictionary.Count < num)
            return GameObject.Instantiate(go, vector, rotation);
        if(objectDictionary.ContainsValue(-1))
        {
            foreach(KeyValuePair<T, int> dic in objectDictionary)
            {
                // 找一个标识位为FALSE的，将它的KEY置空
                if(dic.Value < 0)
                {
                    GameObject t = dic.Key as GameObject;
                    objectDictionary.Remove(dic.Key);
                    if(flag)
                        return GameObject.Instantiate(go, vector, rotation);
                    t.transform.position = vector;
                    return t;
                }
            }
        }
        // 池子已满且没有应该删除的对象时，扩大池子容量
        num ++;
        return GameObject.Instantiate(go, vector, rotation);
    }

    /// <summary>
    /// 将处理完的对象压入池子
    /// </summary>
    public void Store(T go ,int id)
    {
        objectDictionary.Add(go, id);
    }

    /// <summary>
    /// 清空池子
    /// </summary>
    public void ResetAll()
    {
        objectDictionary.Clear();
    }

    /// <summary>
    /// 修改标识位，将不需再保留的对象的值置为-1
    /// </summary>
    /// <param name="list">需要保留的id</param>
    public void SetFlag(List<int> list)
    {
        List<T> buffer = new List<T>();

        foreach(T t in objectDictionary.Keys)
        {
            if(!list.Contains(objectDictionary[t]))
            {
                buffer.Add(t);
            }
        }

        foreach(T t in buffer)
        {
            objectDictionary[t] = -1;
        }
    }

}
