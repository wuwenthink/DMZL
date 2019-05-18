using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DMZL_UI;

namespace Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class WindowUI :UIBehaviour, IPooler
    {
        /// <summary>
        /// 对象Key
        /// </summary>
        int objectKey;
        public int ObjectKey
        {
            get => objectKey;
            set
            {
                objectKey = value;
                if ( objectKey != 0 )
                {
                    ui_dragBar.ObjectName.text = objectKey.ToString();
                    ui_minimize.ObjectName.text = objectKey.ToString();
                }
            }
        }
        /// <summary>
        /// 窗口Tag
        /// </summary>
        public abstract string WindowTag { get; }

        /// <summary>
        /// 导航栏
        /// </summary>
        protected UI_DragBar ui_dragBar;
        /// <summary>
        /// 快捷菜单按钮
        /// </summary>
        protected UI_Minimize ui_minimize;


        CanvasGroup group;
        float alpha;
        bool io;
        /// <summary>
        /// UI是否最小化
        /// </summary>
        public bool IO { get => io; }
      

        protected override void Awake ()
        {
            //获取Group
            group = GetComponent<CanvasGroup>();        
            //获取透明度
            alpha = group.alpha;
            this.OnMouseClick_Left(() => transform.SetAsLastSibling());
            //初始化窗口
            Initialize();
            //最小化窗口
            Minimize();
            //将刷新操作放入UpdateManager
            UpdateManager.I.OnSystemUpdate(Refresh);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Initialize ();

        /// <summary>
        /// 初始化窗口数据
        /// </summary>
        /// <param name="ObjectKey"></param>
        public abstract void Init ( int ObjectKey );

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh ()
        {
            //如果窗口打开，则开始记时，每隔3秒刷新一次窗口
            if ( IO )
            {
                clock += Time.deltaTime;
                if ( clock >= 4 )
                {
                    clock = 0;
                    Init(ObjectKey);
                }
            }
        }
        float clock=0;

        /// <summary>
        /// 退出窗口
        /// </summary>
        public virtual void Exit ()
        {
            //从UI管理器中移除
            UIManager.I.RemoveWindow(this);
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        public virtual void Show ()
        {
            group.alpha = alpha;
            group.blocksRaycasts = true;
            io = true;
            transform.SetAsLastSibling();
            //初始化窗口
            Init(ObjectKey);
        }
      

        /// <summary>
        /// 最小化窗口
        /// </summary>
        public virtual void Minimize ()
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            io = false;
         
        }

        public virtual void OnReset ()
        {
            //初始化位置
            transform.parent =UIManager.I.WindowsCanvas.transform;
            transform.localPosition = Vector3.zero;
            
            //从对象池中获取导航栏
            ui_dragBar = Pool.I.GetObject(BundleManager.I.Load<UI_DragBar>("UI_DragBar"));
            ui_dragBar.transform.parent = transform;
            ui_dragBar.Window = this;

            //从对象池取出最小化按钮并赋值
            ui_minimize = Pool.I.GetObject(BundleManager.I.Load<UI_Minimize>("UI_Minimize"));
            ui_minimize.Window = this;

            //注册窗口名
            LanguageManager.I.TextRegister(LanguageKey.Word_UI,WindowTag,ui_dragBar.WindowName);
            LanguageManager.I.TextRegister(LanguageKey.Word_UI,WindowTag,ui_minimize.WindowName);
        }

        public virtual void Recover ()
        {
            //移除窗口名
            LanguageManager.I.RemoveText(ui_dragBar.ObjectName);
            LanguageManager.I.RemoveText(ui_dragBar.ObjectName);
            LanguageManager.I.RemoveText(ui_dragBar.WindowName);
            LanguageManager.I.RemoveText(ui_minimize.WindowName);
            //回收导航栏
            Pool.I.CollectObject(ui_dragBar);
            //回收最小化按钮
            Pool.I.CollectObject(ui_minimize);
            //移除刷新事件
            UpdateManager.I.RemoveSystemUpdate(Refresh);
        }

    }
}
