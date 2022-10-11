using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BLL
{
    public class Cache_BLL
    {
        /// <summary>
        /// 数据缓存处理方法
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="bool">out 参数判断缓存是否存在</param>
        /// <param name="list">要缓存的数据 list<T>类型（也可以设置其他类型）</param>
        /// <returns></returns>
        public static List<Cache_readPLC> Cache<Cache_readPLC>(string key, out bool b, List<Cache_readPLC> list = null)
        {
            List<Cache_readPLC> listnew = null;
            string CacheKeyUserList = key;
            b = false;
            if (!CacheHelp.Exists(CacheKeyUserList))
            {
                #region 增加缓存

                //造数据（可以直接数据库访问获得数据）
                bool SetCacheStatus = CacheHelp.Set(
                     key: CacheKeyUserList,
                     value: list,
                     Seconds: 300 //缓存时长，单位“秒”
                     );
                if (SetCacheStatus)
                {
                    string str = "增加缓存成功";
                }
                else
                {
                    string str = "增加缓存失败";
                }
                #endregion
            }
            else
            {
                #region 获取缓存
                listnew = (List<Cache_readPLC>)CacheHelp.Get(CacheKeyUserList);
                #endregion
                b = true;
            }
            return listnew;
        }
        /// <summary>
        /// 数据缓存处理方法
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="bool">out 参数判断缓存是否存在</param>
        /// <param name="list">要缓存的数据 list<T>类型（也可以设置其他类型）</param>
        /// <returns></returns>
        public static List<YJ_PLC_list> CachePLClist<Cache_readPLC>(string key, out bool b, List<YJ_PLC_list> list = null)
        {
            List<YJ_PLC_list> listnew = null;
            string CacheKeyUserList = key;
            b = false;
            if (!CacheHelp.Exists(CacheKeyUserList))
            {
                #region 增加缓存

                //造数据（可以直接数据库访问获得数据）
                bool SetCacheStatus = CacheHelp.Set(
                     key: CacheKeyUserList,
                     value: list,
                     Seconds: 300 //缓存时长，单位“秒”
                     );
                if (SetCacheStatus)
                {
                    string str = "增加缓存成功";
                }
                else
                {
                    string str = "增加缓存失败";
                }
                #endregion
            }
            else
            {
                #region 获取缓存
                listnew = (List<YJ_PLC_list>)CacheHelp.Get(CacheKeyUserList);
                #endregion
                b = true;
            }
            return listnew;
        }
    }
}
