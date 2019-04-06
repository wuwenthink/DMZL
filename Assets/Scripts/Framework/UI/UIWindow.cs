using System.Collections;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// UI窗口:所有窗口基类
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIWindow :MonoBehaviour
    {
        CanvasGroup group;
        float alpha;
        bool io;

        public bool IO { get => io;}

        private void Awake ()
        {
            //向UIManager注册自己
            UIManager.I.RegisterUI(this);
            //获取Group
            group = GetComponent<CanvasGroup>();
            //获取透明度
            alpha = group.alpha;
            //隐藏窗口
            Hide();
            io = false;
            //初始化ui
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        abstract public void Initialize ();

        /// <summary>
        /// 显示窗口
        /// </summary>
        public void Show ()
        {
            group.alpha = alpha;
            group.blocksRaycasts = true;
            io = true;
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public void Hide ()
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            io = false;
        }
    }
}