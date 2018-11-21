using UnityEngine;

namespace PathologicalGames
{

    [AddComponentMenu("Path-o-logical/PoolManager/Pre-Runtime Pool Item")]
    public class PreRuntimePoolItem : MonoBehaviour
    {
        #region Public Properties
        public string poolName = ""; //缓存池名称
        public string prefabName = ""; //缓存池中预制名称
        public bool despawnOnStart = true; //start时是否出池
        public bool doNotReparent = false;//是否不重设池节点为父节点
        #endregion Public Properties

        private void Start()
        {
            SpawnPool pool;
            if (!PoolManager.Pools.TryGetValue(this.poolName, out pool))
            {

                string msg = "PreRuntimePoolItem Error ('{0}'): " +
                        "No pool with the name '{1}' exists! Create one using the " +
                        "PoolManager Inspector interface or PoolManager.CreatePool()." +
                        "See the online docs for more information at " +
                        "http://docs.poolmanager.path-o-logical.com";

                Debug.LogError(string.Format(msg, this.name, this.poolName));
                return;
            }
            pool.Add(this.transform, this.prefabName,
                     this.despawnOnStart, !this.doNotReparent);
        }
    }

}

