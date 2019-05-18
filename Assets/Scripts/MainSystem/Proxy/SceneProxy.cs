using System;
using Common;
using DMZL_UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainSpace
{
    /// <summary>
    /// 场景代理者
    /// </summary>
    public class SceneProxy :IProxy
    {

        public Type ProxyType => typeof(SceneProxy);

        public string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    MainMessages.LoadScene,
                };
            }
        }
        public void HandleMessage ( string letter,params object[] data )
        {
            switch ( letter )
            {
                case MainMessages.LoadScene:
                    loadScene(data[0] as string);
                    break;
            }
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        private AsyncOperation _asyncOperation;

        /// <summary>
        /// 加载UI
        /// </summary>
        UI_SceneLoading uiSceneLoading;


        public void Initialize ()
        {
            _asyncOperation = null;
            uiSceneLoading = UIManager.I.GetSceneUI<UI_SceneLoading>();
            SceneManager.sceneUnloaded += releaseMemory;
        }
        public void Close ()
        {
            
        }

        /// <summary>
        /// 读取场景
        /// </summary>
        /// <param name="sceneName"></param>
        void loadScene ( string sceneName )
        {
            //暂停Update
            UpdateManager.I.PauseUpdate();
            //出现加载UI
            uiSceneLoading.Show();
            //读取场景
            _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        void releaseMemory (Scene scene)
        {
            //清空系统Update
            UpdateManager.I.ClearSystemUpdate();
            //注册读取UI的Update方法
            UpdateManager.I.OnSystemUpdate(loadUIUpdate);
            //清空Update
            UpdateManager.I.ClearUpdate();
            //清空语言字典
            LanguageManager.I.ClearTextDic();
            //清空UI缓存
            UIManager.I.ClearSceneUICache();
            //清空对象池
            Pool.I.ClearPool();
            //内存回收
            GC.Collect();
        }

        /// <summary>
        /// UI的Update方法
        /// </summary>
        void loadUIUpdate ()
        {
            if ( _asyncOperation != null )
            {               
                uiSceneLoading.SetValue(_asyncOperation.progress);
                if ( _asyncOperation.isDone ) _asyncOperation = null;
                UpdateManager.I.RemoveSystemUpdate(loadUIUpdate);
            }
        }
    }
}
