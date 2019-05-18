using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager :MonoSingleton<UIManager>
    {
        public Canvas WindowsCanvas;
        public Canvas SceneCanvas;
        public Canvas SystemCanvas;

        //获取Canvas方法
        public void GetAllCanvas ()
        {
            FindObjectsOfType<Canvas>().Foreach(e =>
            {
                switch ( e.name )
                {
                    case "WindowsCanvas":
                        WindowsCanvas = e;
                        break;
                    case "SceneCanvas":
                        SceneCanvas = e;
                        break;
                    case "SystemCanvas":
                        SystemCanvas = e;
                        break;
                }
            });
        }

        /// <summary>
        /// 场景UI缓存
        /// </summary>
        Dictionary<Type,SceneUI> sceneUICache;
        /// <summary>
        /// 窗口UI缓存
        /// </summary>
        Dictionary<Type,Dictionary<int,WindowUI>> windowCache;

        protected override void Initialize ()
        {
            sceneUICache = new Dictionary<Type,SceneUI>();
            windowCache = new Dictionary<Type,Dictionary<int,WindowUI>>();
            //获取所有UI
            GetAllCanvas();
        }

        /// <summary>
        /// 获取场景UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSceneUI<T> () where T : SceneUI
        {
            Type type = typeof(T);
            //如果没有type,则返回空
            if ( !sceneUICache.ContainsKey(type) ) return null;
            return sceneUICache[type] as T;
        }

        /// <summary>
        /// 注册场景UI
        /// </summary>
        /// <param name="ui"></param>
        public void RegisterSceneUI ( SceneUI ui )
        {
            sceneUICache[ui.GetType()] = ui;
        }

        /// <summary>
        /// 清空场景UI缓存
        /// </summary>
        public void ClearSceneUICache ()
        {
            sceneUICache.Clear();
            SystemCanvas.GetComponentsInChildren<SceneUI>().Foreach(e => RegisterSceneUI(e));
        }

        /// <summary>
        /// 注册窗口
        /// </summary>
        /// <param name="ui"></param>
        void RegisterWindow ( WindowUI ui )
        {
            if ( !windowCache.ContainsKey(ui.GetType()) ) windowCache[ui.GetType()] = new Dictionary<int,WindowUI>();
            windowCache[ui.GetType()][ui.ObjectKey] = ui;
        }

        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        /// <param name="ObjectName"></param>
        public T GetWindow<T> ( int ObjectKey=0 ) where T : WindowUI
        {
            Type type = typeof(T);
            if ( !windowCache.ContainsKey(type) ) windowCache[type] = new Dictionary<int,WindowUI>();

            if ( windowCache[type].ContainsKey(ObjectKey) )
            {
                return windowCache[type][ObjectKey] as T;
            }
            else
            {
                //创建Window
                T window = SetUpWindow<T>(ObjectKey);
                //注册窗口
                RegisterWindow(window);
                return window;
            }

        }
        /// <summary>
        /// 创建窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ObjectKey"></param>
        /// <returns></returns>
        public T SetUpWindow<T> ( int ObjectKey=0 ) where T : WindowUI
        {
            Type type = typeof(T);
            //从对象池获取Window
            WindowUI window = Pool.I.GetObject(BundleManager.I.Load<WindowUI>(type.Name));
            //初始化Window数据
            window.ObjectKey = ObjectKey;
            return window as T;
        }
        /// <summary>
        /// 移除Window
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="ObjectName"></param>
        public void RemoveWindow ( WindowUI ui )
        {
            //从缓存中移除
            windowCache[ui.GetType()].Remove(ui.ObjectKey);
            //回收该窗口
            Pool.I.CollectObject(ui);
        }
        /// <summary>
        /// 清除所有窗口
        /// </summary>
        public void RemoveAllWindow ()
        {
            foreach ( Dictionary<int,WindowUI> pairs in windowCache.Values )
            {
                foreach ( WindowUI window in pairs.Values )
                {
                    Pool.I.CollectObject(window);
                }
            }
            windowCache.Clear();
        }
    }
}
