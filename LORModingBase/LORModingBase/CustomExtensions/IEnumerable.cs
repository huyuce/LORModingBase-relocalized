using System;
using System.Collections.Generic;
using System.Linq;

namespace LORModingBase.CustomExtensions
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class IEnumerable//创建 IEnumerable 扩展方法
    {
        /// <summary>
        /// Safely foreach IEnumberable object
        /// </summary>
        public static void ForEachSafe<T>(this IEnumerable<T> source, Action<T> foreachCallBack)//为 IEnumerable 创建安全的 ForEach 扩展方法
        {
            foreach (T eachObject in source ?? Enumerable.Empty<T>())
                foreachCallBack(eachObject);
        }

        /// <summary>
        /// Find string for given string
        /// </summary>
        public static List<string> FindAll_Contains(this string[] source, string strToSearch, bool ignoreCase = false)///为 string 数组创建查找包含指定字符串的所有元素的扩展方法
        {
            List<string> findList = new List<string>();
            foreach (string eachStr in source)
            {
                if (!ignoreCase && eachStr.Contains(strToSearch))
                    findList.Add(eachStr);
                if (ignoreCase && eachStr.ToLower().Contains(strToSearch.ToLower()))
                    findList.Add(eachStr);
            }
            return findList;
        }

        /// <summary>
        /// Key value for each for dictionary if not null
        /// </summary>
        public static void ForEachKeyValuePairSafe<A, B>(this Dictionary<A, B> dicToForEach, Action<A, B> dicForEachAction)///为字典创建安全的键值对遍历扩展方法
        {
            if (dicToForEach == null) return;
            foreach (KeyValuePair<A, B> eachPair in dicToForEach)
                dicForEachAction(eachPair.Key, eachPair.Value);
        }

        /// <summary>
        /// Action for one item if not null or empty
        /// </summary>
        public static void ActionOneItemSafe<A>(this List<A> listToAction, Action<A> action)//为列表创建对第一个元素执行操作的扩展方法
        {
            if (listToAction != null && listToAction.Count > 0)
                action(listToAction[0]);
        }
    
        /// <summary>
        /// Get unique list
        /// </summary>
        /// <param name="listToUnique"></param>
        /// <returns></returns>
        public static List<string> GetUniqueList(this List<string> listToUnique)
        {
            return new HashSet<string>(listToUnique).ToList();
        }
    }
}
