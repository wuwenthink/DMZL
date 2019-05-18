using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class Pool :MonoSingleton<Pool>
    {
        Dictionary<string,Queue<GameObject>> pool;
        protected override void Initialize ()
        {
            pool = new Dictionary<string,Queue<GameObject>>();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="type">对象类型名</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public GameObject GetObject ( GameObject prefab )
        {
            if ( !pool.ContainsKey(prefab.name + "(Clone)") ) pool[prefab.name + "(Clone)"] = new Queue<GameObject>(); 
            if ( pool[prefab.name + "(Clone)"].Count>0 )
            {
                GameObject obj = pool[prefab.name + "(Clone)"].Dequeue();
                UseObject(obj);
                return obj;
            }
            else
            {
                UseObject(prefab);
                return Instantiate(prefab);
            }
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="type">对象类型名</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public T GetObject<T> ( T prefab ) where T: MonoBehaviour,IPooler
        {
            if ( !pool.ContainsKey(prefab.name+ "(Clone)") ) pool[prefab.name + "(Clone)"] = new Queue<GameObject>();
            if ( pool[prefab.name+"(Clone)"].Count > 0 )
            {
                T obj = pool[prefab.name + "(Clone)"].Dequeue().GetComponent<T>();
                UseObject(obj.gameObject);
                return obj as T;
            }
            else
            {
                T obj = Instantiate(prefab) as T;
                UseObject(obj.gameObject);
                return obj;
            }
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public GameObject[] GetObject ( GameObject prefab,int num ) 
        {
            GameObject[] objects = new GameObject[num];
            for ( int i = 0 ; i < num ; i++ )
            {
                objects[i] = GetObject(prefab);
            }
            return objects;
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public T[] GetObject<T> ( T prefab,int num ) where T : MonoBehaviour, IPooler
        {
            T[] objects = new T[num];
            for ( int i = 0 ; i < num ; i++ )
            {
                objects[i] = GetObject<T>(prefab);
            }
            return objects;
        }

        /// <summary>
        /// 在对象池增添新对象(增添对象必须挂载接口IPooler)
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public void CreateNewObject ( GameObject prefab )
        {
            if ( !pool.ContainsKey(prefab.name + "(Clone)") )
            {
                pool.Add(prefab.name + "(Clone)",new Queue<GameObject>());
            }
            GameObject newObj = Instantiate(prefab);
            pool[prefab.name+"(Clone)"].Enqueue(newObj);
            newObj.transform.parent = I.transform;
            newObj.SetActive(false);
        }

        /// <summary>
        /// 在对象池增添新对象(增添对象必须挂载接口IPooler)
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        public void CreateNewObject ( MonoBehaviour prefab )
        {
            if ( !pool.ContainsKey(prefab.name + "(Clone)") )
            {
                pool.Add(prefab.name + "(Clone)",new Queue<GameObject>());
            }
            GameObject newObj = Instantiate(prefab.gameObject);
            pool[prefab.name+"(Clone)"].Enqueue(newObj);
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
        public void CreateNewObject ( GameObject prefab,int num )
        {
            for ( int i = 0 ; i < num ; i++ )
            {
                CreateNewObject(prefab);
            }
        }
        /// <summary>
        /// 在对象池增添新对象(增添对象必须挂载接口IPooler)
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="prefab">预制件</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public void CreateNewObject ( MonoBehaviour prefab,int num )
        {
            for ( int i = 0 ; i < num ; i++ )
            {
                CreateNewObject(prefab);
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
            if ( !pool.ContainsKey(obj.name) )
            {
                pool.Add(obj.name,new Queue<GameObject>());
            }
            pool[obj.name].Enqueue(obj);
            obj.transform.parent = I.transform;
            obj.GetComponent<IPooler>().Recover();
            obj.SetActive(false);
        }
        /// <summary>
        /// 回收预制件
        /// </summary>
        /// <param name="obj">预制件</param>
        public void CollectObject ( MonoBehaviour obj )
        {
            if ( !pool.ContainsKey(obj.name) )
            {
                pool.Add(obj.name,new Queue<GameObject>());
            }
            pool[obj.name].Enqueue(obj.gameObject);
            obj.transform.parent = I.transform;
            obj.GetComponent<IPooler>().Recover();
            obj.gameObject.SetActive(false);
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
        /// 回收预制件数组
        /// </summary>
        /// <param name="obj">预制件</param>
        public void CollectObject( MonoBehaviour[] obj )
        {
            obj.Foreach(e => CollectObject(e));
        }
        /// <summary>
        /// 清除对象池中的某类对象
        /// </summary>
        /// <param name="type"></param>
        public void ClearObject ( GameObject obj )
        {
            //销毁该类型池的对象
            pool[obj.name].ToArray().Foreach(e => Destroy(e));
            pool.Remove(obj.name);
        }
        /// <summary>
        /// 清除对象池中的某类对象
        /// </summary>
        /// <param name="type"></param>
        public void ClearObject ( MonoBehaviour obj )
        {
            //销毁该类型池的对象
            pool[obj.name].ToArray().Foreach(e => Destroy(e));
            pool.Remove(obj.name);
        }
        /// <summary>
        /// 清除对象池中的某类对象
        /// </summary>
        /// <param name="type"></param>
        public void ClearObject ( string objName )
        {
            pool[objName].ToArray().Foreach(e => Destroy(e));
            pool.Remove(objName);
        }
        /// <summary>
        /// 清空对象池
        /// </summary>
        public void ClearPool ()
        {
            //销毁每一种类型的对象
            foreach ( string type in pool.Keys )
            {
                ClearObject(type);
            }
        }
    }
}
