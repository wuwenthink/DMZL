using UnityEngine.Events;
using UnityEngine.UI;

namespace Common
{
    /// <summary>
    /// 按钮扩展方法类
    /// </summary>
    public static class ButtonHelper
    {
        /// <summary>
        /// button触发返回自己名字的回调函数
        /// </summary>
        /// <param name="button"></param>
        /// <param name="call"></param>
        public static void OnClick_AddListener_CallName ( this Button button,UnityAction<string> call )
        {
            Button_CallName item = button.GetComponent<Button_CallName>();
            if ( item == null )
            {
                item = button.gameObject.AddComponent<Button_CallName>();
            }
            item.events += call;
            button.onClick.AddListener(item.OnClick);
        }
    }
}
