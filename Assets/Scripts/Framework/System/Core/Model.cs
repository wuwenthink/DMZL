using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 模型层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Model
    {
        /// <summary>
        /// 功能代理者Map
        /// </summary>
        Dictionary<Type,IProxy> proxyMap;

        /// <summary>
        /// 消息中心
        /// </summary>
        MessageCenter messageCenter;


        public Model ()
        {
            proxyMap = new Dictionary<Type,IProxy>();
            messageCenter = new MessageCenter();
        }

        /// <summary>
        ///  注册IProxy
        /// </summary>
        /// <param name="proxy"></param>
        public void RegisterProxy ( IProxy proxy )
        {
            //如果Map里右Proxy，则关闭该代理者
            if ( proxyMap.ContainsValue(proxy) )
            {
                proxyMap[proxy.ProxyType].Close();
                //如果Map里是有此代理者，则移除消息中心的以前的代理者
                messageCenter.RemoveObserver(proxyMap[proxy.ProxyType]);
            }
            //注册
            proxyMap[proxy.ProxyType] = proxy;
            //向消息中心添加观察者
            messageCenter.AddObserver(proxy);
            //初始化
            proxy.Initialize();
        }

        /// <summary>
        ///  移除IProxy
        /// </summary>
        /// <param name="name"></param>
        public void RemoveProxy<T> () where T : IProxy
        {
            Type proxyType = typeof(T);
            if ( proxyMap.ContainsKey(proxyType) )
            {
                //执行关闭方法
                proxyMap[proxyType].Close();
                //移除消息中心的代理者
                messageCenter.RemoveObserver(proxyMap[proxyType]);
                proxyMap.Remove(proxyType);          
            }         
        }

        /// <summary>
        ///  获取IProxy
        /// </summary>
        /// <returns></returns>
        public T GetProxy<T> () where T : IProxy
        {
            {
                Type proxyType = typeof(T);
                if ( proxyMap.ContainsKey(proxyType) )
                {
                    return (T)proxyMap[proxyType];
                }
                return default;
            }
        }

        /// <summary>
        /// 发送消息 
        /// </summary>
        public void SendMessage ( string letter,params object[] data )
        {
            messageCenter.SendMessaege(letter,data);
        }
    }
}
