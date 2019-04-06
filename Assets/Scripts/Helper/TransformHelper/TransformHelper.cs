using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    ///  Transform组件助手类
    /// </summary>
	public static class TransformHelper
	{
        /// <summary>
        /// 根据名称查找未知层级子物体
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            Transform childTF = currentTF.Find(childName);
            if (childTF != null) return childTF;
            for (int i = 0; i < currentTF.childCount; i++)
            { 
                childTF = currentTF.GetChild(i).FindChildByName(childName);
                if (childTF != null) return childTF;
            }
            return null;
        }

        /// <summary>
        /// 根据部分名称查找未知层级子物体
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform[] FindChildListBySubName(this Transform currentTF, string childName)
        {
            List<Transform> transformList = new List<Transform>();
            foreach (Transform childitem in currentTF)
            {
                if (childitem.name.Contains(childName))
                {
                    transformList.Add(childitem);
                }
            }
            return transformList.ToArray();
        }
    }
}