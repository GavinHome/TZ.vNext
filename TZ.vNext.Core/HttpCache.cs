//-----------------------------------------------------------------------------------
// <copyright file="HttpCache.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.Caching.Memory;
using TZ.vNext.Core.Cache;

namespace TZ.vNext.Core
{
    public class HttpCache
    {
        private static HttpCache _httpCache = new HttpCache();
        private static ICache CurrentCache;

        private HttpCache()
        {
        }

        public static HttpCache Current => _httpCache;

        public static void SetContext(ICache cache)
        {
            CurrentCache = cache;
        }

        public object Get(string key)
        {
            return CurrentCache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)CurrentCache.Get(key);
        }

        public void Insert(string key, object value)
        {
            CurrentCache.Set(key, value, new DateTimeOffset(DateTime.MaxValue));
        }

        public void Remove(string key)
        {
            CurrentCache.Remove(key);
        }

        public void RemoveAll()
        {
            CurrentCache.Clear();
        }
    }
}
