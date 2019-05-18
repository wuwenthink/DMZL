using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    /// <summary>
    /// 选项脚本
    /// </summary>
    public class UI_Opinion :UIBehaviour, IPooler
    {
        UI_Tips tips;

        Text optionText;
        Button button;

        VO_Opinion vo;

        protected override void Awake ()
        {
            optionText =transform.GetComponentInChildren<Text>();
            button = transform.GetComponent<Button>();

            //鼠标悬停3秒后从对象池中获取Tips
            button.OnMouseStay(() =>
            {
                tips = Pool.I.GetObject(BundleManager.I.Load<UI_Tips>("UI_Tips"));
                tips.ShowTips(vo.TextTable,vo.TipsTag);
            },3);
            //鼠标离开回收Tips
            button.OnMouseExit(() => 
            {
                if ( tips != null )
                {
                    tips.HideTips();
                    Pool.I.CollectObject(tips);
                    tips = null;
                }
               
            });
        }

        /// <summary>
        /// 初始化选项
        /// </summary>
        /// <param name="vo">选项VO</param>
        public void Init ( VO_Opinion vo )
        {
            this.vo = vo;
            //语言系统注册选项Text
            LanguageManager.I.TextRegister(vo.TextTable,vo.OptionTag,optionText);
            //按钮注册事件
            button.onClick.AddListener(vo.Action);
        }
        public void OnReset ()
        {

        }

        public void Recover ()
        {
            //语言系统移除optionText
            LanguageManager.I.RemoveText(optionText);
            button.onClick.RemoveAllListeners();
        }
    }
}
