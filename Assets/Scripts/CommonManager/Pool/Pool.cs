using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class Pool :MonoSingleton<Pool>
    {
        Dictionary<string,List<GameObject>> pool;
        protected override void Initialize ()
        {
            pool = new Dictionary<string,List<GameObject>>();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="type">对象类型名</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public GameObject GetObject ( string type,GameObject prefab )
        {
            if ( !pool.ContainsKey(type) ) CreateNewObject(type,prefab);
            GameObject obj = pool[type].ToArray().Find(e => e.activeInHierarchy == false);
            if ( obj != null )
            {
                UseObject(obj);
                return obj;
            }
            else
            {
                CreateNewObject(type,prefab);
                GameObject newObject = GetObject(type,prefab);
                UseObject(newObject);
                return newObject;
            }
        }

        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public GameObject[] GetObjects ( string type,GameObject prefab,int num )
        {
            GameObject[] objects = new GameObject[num];
            for ( int i = 0 ; i < num ; i++ )
            {
                objects[i] = GetObject(type,prefab);
            }
            return objects;
        }

        /// <summary>
        /// 在对象池增添新对象(增添对象必须挂载接口IPooler)
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public void CreateNewObject ( string type,GameObject prefab )
        {
            if ( !pool.ContainsKey(type) )
            {
                pool.Add(type,new List<GameObject>());
            }
            GameObject newObj = Instantiate(prefab);
            pool[type].Add(newObj);
            newObj.transform.parent = I.transform;
            newObj.SetActive(false);
        }

        /// <summary>
        /// 在对象池增添新对象(增添对象必须挂载接口IPooler)
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public void CreateNewObject ( string type,GameObject prefab,int num )
        {
            for ( int i = 0 ; i < num ; i++ )
            {
                CreateNewObject(type,prefab);
            }
        }

        /// <summary>
        /// 使用预制件
        /// </summary>
        void UseObject ( GameObject obj )
        {
            obj.SetActive(true);
            obj.GetComponent<IPooler>().OnReset();
        }

        /// <summary>
        /// 回收预制件
        /// </summary>
        /// <param name="obj">预制件</param>
        public void CollectObject ( GameObject obj )
        {
            obj.transform.parent = I.transform;
            obj.SetActive(false);
            obj.GetComponent<IPooler>().Recover();
        }
        /// <summary>
        /// 回收预制件数组
        /// </summary>
        /// <param name="obj">预制件</param>
        public void CollectObject ( GameObject[] obj )
        {
            obj.Foreach(e=> CollectObject(e));
        }
        /// <summary>
        /// 清除对象池中的某类对象
        /// </summary>
        /// <param name="type"></param>
        public void ClearObject ( string type )
        {
            //销毁该类型所有对象
            pool[type].ToArray().Foreach(e=>Destroy(e));
            pool.Remove(type);
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void ClearPool ()
        {
            //销毁每一种类型的对象
            foreach ( string type in pool.Keys )
            {
                pool[type].ToArray().Foreach(e => Destroy(e));
            }
            //清空字典
            pool.Clear();
        }
    }
}
