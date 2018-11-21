// ======================================================================================
// 文 件 名 称：PoolInstance.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-21 13:18:17
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-21 13:18:17
// ======================================================================================
// 功 能 描 述：游戏中唯一的对象池实例
// ======================================================================================

using PathologicalGames;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstance : MonoBehaviour
{
    private static SpawnPool _poolInstance;

    public static SpawnPool Instance
    {
        get
        {
            return _poolInstance;
        }
    }

    [SerializeField]
    public List<ItemInPool> PrefabPools = new List<ItemInPool> ();

    [Serializable]
    public class ItemInPool
    {
        public string Name;
        public Transform Prefab;
        public int LimitAmount = 1;
    }

    private void Awake ()
    {
        _poolInstance = PoolManager.Pools.Create ("Dmzl");

        _poolInstance.logMessages = true;

        foreach (ItemInPool item in PrefabPools)
        {
            PrefabPool prefabPool = new PrefabPool (item.Prefab)
            {
                preloadAmount = 1,
                cullDespawned = true,
                cullAbove = 3,
                cullDelay = 60,
                cullMaxPerPass = 1,
                limitInstances = true,
                limitAmount = item.LimitAmount,
                limitFIFO = true
            };
            _poolInstance.CreatePrefabPool (prefabPool);
        }
    }



}