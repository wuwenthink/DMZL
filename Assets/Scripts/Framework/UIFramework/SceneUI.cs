using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    /// <summary>
    /// UIScene:所有场景中的UI基类
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class SceneUI :UIBehaviour
    {
        CanvasGroup group;
        float alpha;
        bool io;

        /// <summary>
        /// UI是否隐藏
        /// </summary>
        public bool IO { get => io;}
      
        protected override void Awake ()
        {
            //在UI系统中注册自己
            UIManager.I.RegisterSceneUI(this);
            //获取Group
            group = GetComponent<CanvasGroup>();
            //获取透明度
            alpha = group.alpha;
            //初始化ui
            Initialize();
            //隐藏窗口
            Hide();
            io = false;          
        }

        /// <summary>
        /// 初始化
        /// </summary>
        abstract public void Initialize ();

        /// <summary>
        /// 显示窗口
        /// </summary>
        public virtual void Show ()
        {
            group.alpha = alpha;
            group.blocksRaycasts = true;
            io = true;
        }
   
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public virtual void Hide ()
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            io = false;
        }
    }
}