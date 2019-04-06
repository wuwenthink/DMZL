namespace Common
{
    public interface IObserver
    {
        /// <summary>
        /// 监听的消息
        /// </summary>
        /// <returns></returns>
        string[] ListeningMessages
        {
            get;
        }

        /// <summary>
        /// 处理消息语句
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        void HandleMessage ( string letter,params object[] data );
    }
}
