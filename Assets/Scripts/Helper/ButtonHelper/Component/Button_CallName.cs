using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// 在按钮上返回自己名字的脚本
    /// </summary>
    public class Button_CallName : MonoBehaviour
    {
        public UnityAction<string> events;
        public void OnClick()
        {
            events(gameObject.name);
        }
    }
}
