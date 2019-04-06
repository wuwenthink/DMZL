using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 数组助手类
    /// </summary> 
    public static class ArrayHelper 
    {
        /// <summary>
        /// 查找数组中所有符合条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allEnemy"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] allEnemy,  Func<T,bool> handler)
        {
            List<T> result = new List<T>();
            for ( int i = 0; i < allEnemy.Length; i++ )
            {
                if( handler( allEnemy[i] ))
                {
                    result.Add( allEnemy[i] );
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 查找数组中某个符合条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allEnemy"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] allEnemy,Func<T,bool> handler)
        { 
            for ( int i = 0; i < allEnemy.Length; i++ )
            {
                //if ( allEnemy[i].HP > 0 )
                if ( handler( allEnemy[i] ) ) 
                    return  allEnemy[i]; 
            }
            return default(T);
        }

        /// <summary>
        /// 获取最大的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="allEnemy"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T GetMax<T,R>(this T[] allEnemy,Func<T, R> handler) where R :IComparable
        { 
            if ( allEnemy == null || allEnemy.Length == 0 ) return default(T);

            T max = allEnemy[0];
            for ( int i = 1; i < allEnemy.Length; i++ )
            {
                if ( handler( max ).CompareTo( handler( allEnemy[i] )) < 0 )
                    max = allEnemy[i];
            }
            return max;
        }

        /// <summary>
        /// 获取最小的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="allEnemy"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T GetMin<T, R>(this T[] allEnemy, Func<T, R> handler) where R : IComparable
        {
            if (allEnemy == null || allEnemy.Length == 0) return default(T);

            T max = allEnemy[0];
            for (int i = 1; i < allEnemy.Length; i++)
            {
                if (handler(max).CompareTo(handler(allEnemy[i])) > 0)
                    max = allEnemy[i];
            }
            return max;
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="array">数组</param>
        /// <param name="handler"></param>
        public  static void OrderBy<T,R>(T[] array,Func<T,R> handler) where R:IComparable
        {
            for ( int r = 0; r < array.Length - 1; r++ )
            {
                for ( int c = r + 1; c < array.Length; c++ )
                {
                    if( handler( array[r] ).CompareTo( handler( array[c] ) ) >0    )
                    {
                        //交换
                        T temp = array[r];
                        array[r] = array[c];
                        array[c] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="array">数组</param>
        /// <param name="handler"></param>
        public static void DesOrderBy<T, R>(T[] array, Func<T, R> handler) where R : IComparable
        {
            for (int r = 0; r < array.Length - 1; r++)
            {
                for (int c = r + 1; c < array.Length; c++)
                {
                    if (handler(array[r]).CompareTo(handler(array[c])) < 0)
                    {
                        //交换
                        T temp = array[r];
                        array[r] = array[c];
                        array[c] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="allEnemy"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static R[] Select<T,R>( T[] allEnemy,Func<T,R> handler)
        {
            R[] result = new R[allEnemy.Length];
            for ( int i = 0; i < allEnemy.Length; i++ )
            {
                result[i] = handler( allEnemy[i] );
            }
            return result; 
        }

        /// <summary>
        /// 遍历所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="handler"></param>
        public static void Foreach<T>(this T[] input, Action<T> handler)
        {
            int lenght = input.Length;
            for ( int i = 0 ; i < lenght ; i++ )
            {
                handler(input[i]);
            }
        }
    }
}