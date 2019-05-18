namespace StorySpace
{
    /// <summary>
    /// 消息VO
    /// </summary>
    public struct VO_News
    {
        /// <summary>消息ID</summary>
        public int Id;
        /// <summary>消息标题</summary>
        public string Title;
        /// <summary>消息类型</summary>
        public NewsType NewsType;
        /// <summary>消息内容</summary>
        public string Content;

        /// <summary> 是否已读 </summary>
        public bool Read;
        /// <summary>真实性</summary>
        public Facticity Facticity;
        /// <summary>有效性</summary>
        public bool Validity;

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="Title">消息标题</param>
        /// <param name="NewsType">消息类型</param>
        /// <param name="Content">消息内容</param>
        /// <param name="Read">是否已读</param>
        /// <param name="Facticity">真实性</param>
        /// <param name="Validity">有效性</param>
        public VO_News ( int Id,string Title,NewsType NewsType,string Content,bool Read,Facticity Facticity,bool Validity )
        {
            this.Id = Id;
            this.Title = Title;
            this.NewsType = NewsType;
            this.Content = Content;
            this.Read = Read;
            this.Facticity = Facticity;
            this.Validity = Validity;
        }
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum NewsType :int
    {
        //系统消息
        /// <summary>历史进程 </summary>
        History = 0,
        /// <summary>知名Npc </summary>
        FamousPerson = 1,
        /// <summary>穿越引导 </summary>
        ThroughGuide = 2,

        //正常消息
        /// <summary>打更报时 </summary>
        Chiming = 3,
        /// <summary>情报 </summary>
        Infomation = 4,


        //存疑消息
        /// <summary>交谈 </summary>
        Chat = 5,
        /// <summary>传闻 </summary>
        Rumors = 6,
    }
    /// <summary>
    /// 真实性
    /// </summary>
    public enum Facticity :int
    {
        //未知
        Unkown = 0,
        //真
        True = 1,
        //假
        False = 2,
    }
}
