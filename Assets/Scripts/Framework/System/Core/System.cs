namespace Common
{
    /// <summary>
    /// 系统抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class System<T> where T : System<T>, new()
    {

        /// <summary>
        /// 声明对应模型层
        /// </summary>
        Model model;

        #region 单例
        static T instance;
        static readonly object sync = new object();
        static T Instance
        {
            get
            {
                if ( instance == null )
                {
                    lock ( sync )
                    {
                        if ( instance == null )
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
        public static T I
        {
            get { return Instance; }
        }
        #endregion

        protected System ()
        {
            model = new Model();
        }

        #region 导航层与模型层操作方法

        /// <summary>
        /// 注册功能代理者
        /// </summary>
        /// <param name="proxy"></param>
        public void RegisterProxy ( IProxy proxy )
        {
            model.RegisterProxy(proxy);
        }

        /// <summary>
        /// 移除功能代理者
        /// </summary>
        public void RemoveProxy<F> () where F : IProxy
        {
            model.RemoveProxy<F>();
        }

        /// <summary>
        /// 获取功能代理者
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public F GetProxy<F> () where F : IProxy
        {
            return model.GetProxy<F>();
        }

        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="data"></param>
        public void SendMessage ( string letter,params object[] data )
        {
            model.SendMessage(letter,data);
            //向监听器发送消息
            Monitor.I.SendMessage(letter,data);
        }

     

        /// <summary>
        /// 关闭系统
        /// </summary>
        public virtual void Close ()
        {
            model = null;
            instance = null;
        }

        /// <summary>
        /// 启动系统
        /// </summary>
        abstract public void Start ();
    }
}
