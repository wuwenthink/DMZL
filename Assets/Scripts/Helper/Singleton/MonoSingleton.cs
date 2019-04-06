using UnityEngine;

namespace Common
{
    /// <summary>
    /// 脚本单例类: 为所有管理器提供单例代码.
    /// </summary> 
    public abstract class MonoSingleton<T> :MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = GameBoss.GetManager<T>();
                    //如果在场景中没有找到该对象
                    if ( instance == null )
                    {
                        //创建脚本对象，立即执行Awake，为instance赋值。
                        instance = GameBoss.AddManager<T>();
                    }
                    else
                    {
                        instance.Initialize();
                    }
                }
                return instance;
            }
        }
        public static T I
        {
            get { return Instance; }
        }
        protected virtual void Initialize () { }

        private void Awake ()
        {
            if ( instance == null )
            {
                instance = this as T;
                Initialize();
            }
        }
    }
}
