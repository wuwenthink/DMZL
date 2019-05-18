using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;
using MainSpace;
using System;
using UnityEngine.UI;

namespace DMZL_UI
{
    public class UI_HintHistoryWindow :WindowUI
    {
        public override string WindowTag => "HintHistory";
        /// <summary>
        /// 页码内容
        /// </summary>
        UI_PageContent pageContent;

        public override void Initialize ()
        {
            //获取内容transform
            pageContent = GetComponentInChildren<UI_PageContent>();
        }

        public override void Init ( int ObjectKey )
        {
            //回收所有提示
            CollectAllHints();

            //从数据库读取已读提示数据
            object[][] data = SaveManager.I.Select("*","Main_Hint","Read=1");

            //将数据转化成VO，并增加新提示
            AddHints(VO_Hint.DataToVOs(data));
        }

        /// <summary>
        /// 增加提示
        /// </summary>
        /// <param name="vo"></param>
        void AddHints ( VO_Hint[] vos )
        {
            int length = vos.Length;
            //从对象池获取Tips并初始化
            UI_HintTips[] tips = Pool.I.GetObject(BundleManager.I.Load<UI_HintTips>("UI_HintTips"),length);
            for ( int i = 0 ; i < length ; i++ )
            {
                tips[i].Init(vos[i]);
            }
            pageContent.AddContent(tips);
        }

        /// <summary>
        /// 回收所有提示
        /// </summary>
        void CollectAllHints ()
        {
            //回收所有内容
            Pool.I.CollectObject(pageContent.GetAllContent());
            //清空内容
            pageContent.ClearContent();
        }
    }
}