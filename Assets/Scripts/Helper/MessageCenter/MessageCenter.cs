using System.Collections.Generic;

namespace Common
{
    /// <summary>
    ///  消息中心
    /// </summary>
    public class MessageCenter
    {
        private Dictionary<string,List<IObserver>> observerMap;
        public MessageCenter ()
        {
            observerMap = new Dictionary<string,List<IObserver>>();
        }
       
        /// <summary>
        ///  添加观察者
        /// </summary>
        /// <param name="observer"></param>
        public void AddObserver ( IObserver observer )
        {
            string[] list = observer.ListeningMessages;
            for ( int i = 0 ; i < list.Length ; i++ )
            {
                AddObserverMap(list[i],observer);
            }
        }
        /// <summary>
        ///  添加观察者Map
        /// </summary>
        /// <param name="name"></param>
        /// <param name="observer"></param>
        private void AddObserverMap ( string name,IObserver observer )
        {
            if ( !observerMap.ContainsKey(name) )
                observerMap.Add(name,new List<IObserver>());
            if ( observerMap[name].Contains(observer) )
                return;
            observerMap[name].Add(observer);
        }

        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SendMessaege ( string name,params object[] data )
        {
            // 如果没有对应的List,直接返回
            if ( !observerMap.ContainsKey(name) ) return;
            // 找到List
            List<IObserver> list = observerMap[name];
            // 遍历
            for ( int i = 0 ; i < list.Count ; i++ )
            {
                list[i].HandleMessage(name,data);  // 挨个送
            }

        }
       
        /// <summary>
        ///  移除观察者
        /// </summary>
        /// <param name="observer"></param>
        public void RemoveObserver ( IObserver observer )
        {
            string[] list = observer.ListeningMessages;
            for ( int i = 0 ; i < list.Length ; i++ )
            {
                RemoveObserverMap(list[i],observer);
            }
        }

        /// <summary>
        ///  移除观察者Map
        /// </summary>
        /// <param name="name"></param>
        /// <param name="observer"></param>
        private void RemoveObserverMap ( string name,IObserver observer )
        {
            // 没有list,返回
            if ( !observerMap.ContainsKey(name) ) return;
            // list里面没有observer 返回
            if ( !observerMap[name].Contains(observer) ) return;
            // 直接移除
            observerMap[name].Remove(observer);
        }

        /// <summary>
        /// 移除所有观察者
        /// </summary>
        public void RemoveAllObserver ()
        {
            observerMap.Clear();
        }
    }
}
