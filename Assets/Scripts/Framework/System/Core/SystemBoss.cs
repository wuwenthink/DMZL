using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 系统Boss
    /// </summary>
    public class SystemBoss :Singleton<SystemBoss>
    {
        #region 窃听功能
        /// <summary>
        /// 声明窃听接收者集合
        /// </summary>
        List<IObserver> monitorObserverList;

        /// <summary>
        /// 声明窃听消息中心
        /// </summary>
        MessageCenter monitorMessageCenter;

        /// <summary>
        /// 注册窃听接受者
        /// </summary>
        /// <param name="observer"></param>
        public void RegisterMonitorObserver ( IObserver observer )
        {
            //添加中介器
            monitorObserverList.Add(observer);
            //向消息中心添加观察者
            monitorMessageCenter.AddObserver(observer);
        }

        /// <summary>
        /// 移除窃听接收者
        /// </summary>
        public  void RemoveMonitorObserver ( IObserver observer )
        {
            //移除消息中心的观察者
            I.monitorMessageCenter.RemoveObserver(observer);
            //Map移除此中介器
            I.monitorObserverList.Remove(observer);
        }

        /// <summary>
        /// 发送窃听消息 
        /// </summary>
        public void SendMonitorMessage ( string name,params object[] data )
        {
            monitorMessageCenter.SendMessaege(name,data);
        }
        #endregion

        #region 群发功能
        /// <summary>
        /// 模型层集合
        /// </summary>
       List<Model> modelList;

        /// <summary>
        /// 向所有系统群发消息
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="data"></param>
        public void SendToAllSystem ( string letter,params object[] data )
        {
            //向所有模型层群发消息
            modelList.ForEach(e=>e.SendMessage(letter, data));
            //向监听器发送消息
            SendMonitorMessage(letter,data);
        }

        /// <summary>
        /// 注册Model
        /// </summary>
        /// <param name="model"></param>
        public void RegisterModel ( Model model )
        {
            modelList.Add(model);
        }

        /// <summary>
        /// 移除Model
        /// </summary>
        /// <param name="model"></param>
        public void RemoveModel ( Model model )
        {
            modelList.Remove(model);
        }
        #endregion

        protected override void Initialize ()
        {
            monitorObserverList = new List<IObserver>();
            monitorMessageCenter = new MessageCenter();

            modelList = new List<Model>();

        }
    }
}
