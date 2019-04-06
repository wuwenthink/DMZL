using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Common
{
    public class Alert :UIWindow
    {
        /// <summary>
        /// 单例
        /// </summary>
        static Alert I;
        /// <summary>
        /// Debug文字
        /// </summary>
        Text DebugText;
        /// <summary>
        /// 确定按钮
        /// </summary>
        Button ConfirmButton;
        /// <summary>
        /// 确定事件
        /// </summary>
        UnityAction confirmAction;
        public override void Initialize ()
        {
            //单例等于自己
            I = this;
            DebugText = GetComponentInChildren<Text>();
            ConfirmButton = GetComponentInChildren<Button>();
            ConfirmButton.onClick.AddListener(Confirm);
        }

        /// <summary>
        /// 确定
        /// </summary>
        void Confirm ()
        {
            //如果确定事件不为空，则执行
            confirmAction?.Invoke();
            //隐藏窗口
            I.Hide();
            //清空确定事件
            confirmAction = null;
        }
        
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        public static void Display ( string text,UnityAction action = null )
        {
            //更改文字
            I.DebugText.text = text;
            //有事件传入，则注入确定事件中
            if ( action != null ) I.confirmAction = action;
            //显示窗口
            I.Show();
        }

      
    }
}
