using System.Collections.Generic;

namespace SI.Infrastructure.Service
{
    /// <summary>
    /// Redis帮助
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisHelper<T> where T : new()
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Set(string keyName, List<T> list)
        {
            using (RedisClient rc = new RedisClient("127.0.0.1", 6379))
            {
                bool exists = rc.Exists(keyName) == 1;
                if (!exists)
                {
                    rc.Set<List<T>>(keyName, list);
                }
                else
                {
                    rc.Del(keyName);
                    rc.Set<List<T>>(keyName, list);
                }
                return true;
            }
        }
        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static List<T> Get(string keyName)
        {
            using (RedisClient rc = new RedisClient("127.0.0.1", 6379))
            {
                bool exists = rc.Exists(keyName) == 1;
                if (exists)
                {
                    return rc.Get<List<T>>(keyName);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

