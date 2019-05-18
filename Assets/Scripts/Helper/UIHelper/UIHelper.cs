using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    /// <summary>
    /// UI助手
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseEnter ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action());

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标悬停
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        /// <param name="delay">延时</param>
        public static void OnMouseStay ( this UIBehaviour ui,Action action,float delay )
        {
            float wait = 0;
            Action actionDelay = null;

            //延时执行回调事件
            actionDelay = () =>
              {
                  wait += Time.deltaTime;
                  if ( wait >= delay )
                  {
                      action();
                      wait = 0;
                      UpdateManager.I.RemoveSystemUpdate(actionDelay);
                  }
              };

            //进入时则添加进UpdateManager
            OnMouseEnter(ui,() => UpdateManager.I.OnSystemUpdate(actionDelay));

            //鼠标离开后
            OnMouseExit(ui,() =>
            {
                if ( wait != 0 )
                {
                    wait = 0;
                    UpdateManager.I.RemoveSystemUpdate(actionDelay);
                }
            });
        }

        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseExit ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action());

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标点击_左键
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClick_Left ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Left ) action();
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标点击_右键
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClick_Right ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Right ) action();
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标双击_左键
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClickSecond_Left ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Left && ( (PointerEventData)e ).clickCount == 2 ) action();
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标双击_右键
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClickSecond_Right ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Right && ( (PointerEventData)e ).clickCount == 2 ) action();
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseDown ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action());

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseUp ( this UIBehaviour ui,Action action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action());

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        #region 回调函数返回UI名字
        /// <summary>
        /// 鼠标进入,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseEnter ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action(ui.name));

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标悬停,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        /// <param name="delay">延时</param>
        public static void OnMouseStay ( this UIBehaviour ui,Action<string> action,float delay )
        {
            float wait = 0;
            Action actionDelay = null;

            //延时执行回调事件
            actionDelay = () =>
            {
                wait += Time.deltaTime;
                if ( wait >= delay )
                {
                    action(ui.name);
                    wait = 0;
                    UpdateManager.I.RemoveSystemUpdate(actionDelay);
                }
            };

            //进入时则添加进UpdateManager
            OnMouseEnter(ui,() => UpdateManager.I.OnSystemUpdate(actionDelay));

            //鼠标离开后
            OnMouseExit(ui,() =>
            {
                if ( wait != 0 )
                {
                    wait = 0;
                    UpdateManager.I.RemoveSystemUpdate(actionDelay);
                }
            });
        }

        /// <summary>
        /// 鼠标离开,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseExit ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action(ui.name));

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标点击_左键,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClick_Left ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Left ) action(ui.name);
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标点击_右键,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClick_Right ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Right ) action(ui.name);
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标双击_左键,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClickSecond_Left ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Left && ( (PointerEventData)e ).clickCount == 2 ) action(ui.name);
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        /// <summary>
        /// 鼠标双击_右键,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseClickSecond_Right ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e =>
            {
                if ( ( (PointerEventData)e ).button == PointerEventData.InputButton.Right && ( (PointerEventData)e ).clickCount == 2 ) action(ui.name);
            });

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标按下,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseDown ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action(ui.name));

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 鼠标抬起,回调函数返回UI名字
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="action">回调事件</param>
        public static void OnMouseUp ( this UIBehaviour ui,Action<string> action )
        {
            //获取EventTrigger如果没有就添加一个
            EventTrigger trigger;

            trigger = ui.GetComponent<EventTrigger>();
            if ( trigger == null ) trigger = ui.gameObject.AddComponent<EventTrigger>();

            //添加进入事件
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback = new EventTrigger.TriggerEvent();

            //将回调的Action添加
            entry.callback.AddListener(e => action(ui.name));

            //添加进EventTrigger
            trigger.triggers.Add(entry);
        }
        #endregion 
    }
}

