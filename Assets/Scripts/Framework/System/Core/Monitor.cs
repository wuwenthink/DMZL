using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 窃听器
    /// </summary>
    public class Monitor :Singleton<Monitor>
    {
        /// <summary>
        /// 声明接收者Map
        /// </summary>
        Dictionary<string,IObserver> mediatorMap;

        /// <summary>
        /// 声明消息中心
        /// </summary>
        MessageCenter messageCenter;

        public Monitor ()
        {
            mediatorMap = new Dictionary<string,IObserver>();
            messageCenter = new MessageCenter();
        }

        /// <summary>
        /// 注册功能接受者
        /// </summary>
        /// <param name="mediator"></param>
        public void RegisterObserver ( string ObserverName,  IObserver mediator )
        {
            //如果Map里是有此中介器，则移除消息中心的观察者
            if ( mediatorMap.ContainsKey(ObserverName) ) messageCenter.RemoveObserver(mediator);
            //添加中介器
            mediatorMap[ObserverName] = mediator;
            //向消息中心添加观察者
            messageCenter.AddObserver(mediator);
        }

        /// <summary>
        /// 移除接收者
        /// </summary>
        public void RemoveMediator ( string ObserverName )
        {
            if ( mediatorMap.ContainsKey(ObserverName) )
            {
                //移除消息中心的观察者
                messageCenter.RemoveObserver(mediatorMap[ObserverName]);
                //Map移除此中介器
                mediatorMap.Remove(ObserverName);
            }
        }

        /// <summary>
        /// 发送消息 
        /// </summary>
        public void SendMessage ( string name,params object[] data )
        {
            messageCenter.SendMessaege(name,data);
        }
    }
}
