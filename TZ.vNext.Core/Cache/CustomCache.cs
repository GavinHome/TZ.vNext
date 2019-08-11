//-----------------------------------------------------------------------------------
// <copyright file="CustomCache.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace TZ.vNext.Core.Cache
{
    public sealed class CustomCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<object, ICacheEntry> _cacheEntries = new ConcurrentDictionary<object, ICacheEntry>();

        public CustomCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }
        
        /// <summary>
        /// Gets keys of all items in MemoryCache.
        /// </summary>
        public IEnumerator<object> Keys => this._cacheEntries.Keys.GetEnumerator();

        public void Dispose()
        {
            this._memoryCache.Dispose();
        }

        /// <inheritdoc cref="IMemoryCache.TryGetValue"/>
        public bool TryGetValue(object key, out object value)
        {
            return this._memoryCache.TryGetValue(key, out value);
        }

        /// <summary>
        /// Create or overwrite an entry in the cache and add key to Dictionary.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <returns>The newly created <see cref="T:Microsoft.Extensions.Caching.Memory.ICacheEntry" /> instance.</returns>
        public ICacheEntry CreateEntry(object key)
        {
            var entry = this._memoryCache.CreateEntry(key);
            entry.RegisterPostEvictionCallback(this.PostEvictionCallback);
            this._cacheEntries.AddOrUpdate(
                key,
                entry,
                (o, cacheEntry) =>
                {
                    cacheEntry.Value = entry;
                    return cacheEntry;
                });

            return entry;
        }

        /// <inheritdoc cref="IMemoryCache.Remove"/>
        public void Remove(object key)
        {
            this._memoryCache.Remove(key);
        }

        /// <inheritdoc cref="IMyCache.Clear"/>
        public void Clear()
        {
            foreach (var cacheEntry in this._cacheEntries.Keys.ToList())
            {
                this._memoryCache.Remove(cacheEntry);
            }
        }

        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return this._cacheEntries.Select(pair => new KeyValuePair<object, object>(pair.Key, pair.Value.Value)).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void PostEvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            if (reason != EvictionReason.Replaced)
            {
                this._cacheEntries.TryRemove(key, out var _);
            }
        }
    }
}
