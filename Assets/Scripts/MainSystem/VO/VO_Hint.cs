using System;

namespace MainSpace
{
    /// <summary>
    /// 提示VO
    /// </summary>
    public struct VO_Hint
    {
        public int Id;
        public HintType HintType;
        public int Content;
        public int Number;
        public bool Read;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">提示ID</param>
        /// <param name="HintType">提示类型</param>
        /// <param name="Content">内容</param>
        /// <param name="Number">数量</param>
        /// <param name="Read">是否已读</param>
        public VO_Hint ( int Id,HintType HintType ,int Content ,int Number,bool Read )
        {
            this.Id = Id;
            this.HintType = HintType;
            this.Content = Content;
            this.Number = Number;
            this.Read = Read;
        }
        /// <summary>
        /// 数据转化成VO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VO_Hint[] DataToVOs ( object[][] data )
        {
            int dataLength = data.Length;
            VO_Hint[] vos = new VO_Hint[dataLength];

            //生成vo,UI_HintTips，注入VO
            for ( int i = 0 ; i < dataLength ; i++ )
            {
                //把数据库读取的数据转成VO
                vos[i].Id = Convert.ToInt32(data[i][0]);
                vos[i].HintType = (HintType)Convert.ToInt32(data[i][1]);
                vos[i].Content = Convert.ToInt32(data[i][2]);
                vos[i].Number = Convert.ToInt32(data[i][3]);
                vos[i].Read = Convert.ToInt32(data[i][4]) == 0 ? false : true;

            }
            return vos;
        }
    }
    /// <summary>
    /// 提示类型
    /// </summary>
    public enum HintType :int
    {
        //任务
        //接收任务
        MissionAccept = 0,
        //任务完成
        MissionComplete = 1,
        //任务失败
        MissionFailure = 2,
    }
}
