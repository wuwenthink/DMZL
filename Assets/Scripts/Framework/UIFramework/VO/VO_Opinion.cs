using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// 选项VO
    /// </summary>
    public struct VO_Opinion
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TextTable">文案文字表名</param>
        /// <param name="OptionTag">选项文案标签</param>
        /// <param name="TipsTag">选项解释标签</param>
        /// <param name="Action">选项回调函数</param>
        public VO_Opinion ( string TextTable,string OptionTag,string TipsTag,UnityAction Action )
        {
            this.TextTable = TextTable;
            this.OptionTag = OptionTag;
            this.TipsTag = TipsTag;
            this.Action = Action;
        }
        /// <summary>
        /// 文案的空间名
        /// </summary>
        public string TextTable;

        /// <summary>
        /// 动作名字标签
        /// </summary>
        public string OptionTag;

        /// <summary>
        /// 选项解释标签
        /// </summary>
        public string TipsTag;

        /// <summary>
        /// 该动作的回调函数
        /// </summary>
        public UnityAction Action;
    }
}
