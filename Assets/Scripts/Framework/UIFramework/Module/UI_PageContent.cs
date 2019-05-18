using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    public class UI_PageContent :UIBehaviour
    {
        /// <summary>
        /// 页码
        /// </summary>
        int page;

        /// <summary>
        /// 内容数量
        /// </summary>
        public int PageCount;
        /// <summary>
        /// 内容集合
        /// </summary>
        List<MonoBehaviour> content;

        /// <summary>
        /// 页码文字
        /// </summary>
        public Text pageText;
        /// <summary>
        /// 左按钮
        /// </summary>
        public Button left;
        /// <summary>
        /// 右按钮
        /// </summary>
        public Button right;
        

        protected override void Awake ()
        {
            //初始页码=1
            page = 1;
            content = new List<MonoBehaviour>();

            left.onClick.AddListener(() => Page -= 1);
            right.onClick.AddListener(() => Page += 1);
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        public void AddContent ( MonoBehaviour @object )
        {
            //将内容添加为自己的子物体
            @object.transform.parent = transform;
            //隐藏物体
            @object.gameObject.SetActive(false);
            Content.Add(@object);
            Page = page;
        }
        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="Object"></param>
        public void AddContent ( MonoBehaviour[] objects )
        {
            int Length = objects.Length;
            for ( int i = 0 ; i < Length ; i++ )
            {
                objects[i].transform.parent = transform;
                objects[i].gameObject.SetActive(false);
                Content.Add(objects[i]);
            }
            Page = page;
        }
        /// <summary>
        /// 获取全部内容
        /// </summary>
        public MonoBehaviour[] GetAllContent ()
        {
            return Content.ToArray();
        }
        /// <summary>
        /// 清空内容
        /// </summary>
        public void ClearContent ()
        {
            Content.Clear();
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page
        {
            get => page;
            set
            {
                page = value;
                //文字改变
                pageText.text = page.ToString();

                //将除了该页中的对象显示，其余隐藏
                int count = content.Count;
                for ( int i = 0 ; i < count ; i++ )
                {
                    if ( i >= ( page - 1 ) * PageCount && i < page * PageCount )
                    {
                        Content[i].gameObject.SetActive(true);
                    }
                    else Content[i].gameObject.SetActive(false);
                }

                //如果页码等于1，则隐藏左侧按钮
                if ( page == 1 ) left.gameObject.SetActive(false);
                else left.gameObject.SetActive(true);
                //如果页码乘每页数量>内容数量，则隐藏右侧按钮
                if (page*PageCount>=count ) right.gameObject.SetActive(false);
                else right.gameObject.SetActive(true);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public List<MonoBehaviour> Content
        {
            get => content;
            set
            {
                content = value;
                content.ForEach(e => { e.gameObject.SetActive(false); e.transform.parent = transform; });
                Page = page;
            }
        }
    }
}
