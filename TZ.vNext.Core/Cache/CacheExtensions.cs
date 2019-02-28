//-----------------------------------------------------------------------------------
// <copyright file="CacheExtensions.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/22 16:39:59</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Core.Cache
{
    public static class CacheExtensions
    {
        public static T Set<T>(this ICache cache, object key, T value)
        {
            GuardUtils.NotNull(cache, nameof(cache));
            var entry = cache.CreateEntry(key);
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        public static T Set<T>(this ICache cache, object key, T value, CacheItemPriority priority)
        {
            GuardUtils.NotNull(cache, nameof(cache));
            var entry = cache.CreateEntry(key);
            entry.Priority = priority;
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        public static T Set<T>(this ICache cache, object key, T value, DateTimeOffset absoluteExpiration)
        {
            GuardUtils.NotNull(cache, nameof(cache));
            var entry = cache.CreateEntry(key);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        public static T Set<T>(this ICache cache, object key, T value, TimeSpan absoluteExpirationRelativeToNow)
        {
            GuardUtils.NotNull(cache, nameof(cache));
            var entry = cache.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            entry.Value = value;
            entry.Dispose();

            return value;
        }

        public static T Set<T>(this ICache cache, object key, T value, MemoryCacheEntryOptions options)
        {
            GuardUtils.NotNull(cache, nameof(cache));
            using (var entry = cache.CreateEntry(key))
            {
                if (options != null)
                {
                    entry.SetOptions(options);
                }

                entry.Value = value;
            }

            return value;
        }

        public static TItem GetOrCreate<TItem>(this ICache cache, object key, Func<ICacheEntry, TItem> factory)
        {
            if (!cache.TryGetValue(key, out var result))
            {
                var entry = cache.CreateEntry(key);
                result = factory(entry);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }

        public static async Task<TItem> GetOrCreateAsync<TItem>(this ICache cache, object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!cache.TryGetValue(key, out object result))
            {
                var entry = cache.CreateEntry(key);
                result = await factory(entry);
                entry.SetValue(result);
                entry.Dispose();
            }

            return (TItem)result;
        }

        public static T Get<T>(this ICache cache, object key)
        {
            var entry = (T)cache.Get(key);
            return entry;
        }
    }
}
