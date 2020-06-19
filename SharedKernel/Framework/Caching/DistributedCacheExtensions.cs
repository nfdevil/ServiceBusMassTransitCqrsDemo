﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;

namespace SharedKernel.Framework.Caching
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
            where T : class, new()
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, value);
                await cache.SetAsync(key, stream.ToArray(), options);
            }
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
            where T : class, new()
        {
            byte[] data = await cache.GetAsync(key);
            using (var stream = new MemoryStream(data))
            {
                {
                    return (T) new BinaryFormatter().Deserialize(stream);
                }
            }
        }

        public static async Task SetAsync(this IDistributedCache cache, string key, string value, DistributedCacheEntryOptions options)
        {
            await cache.SetAsync(key, Encoding.UTF8.GetBytes(value), options);
        }

        public static async Task<string> GetAsync(this IDistributedCache cache, string key)
        {
            byte[] data = await cache.GetAsync(key);
            return data != null ? Encoding.UTF8.GetString(data) : null;
        }
    }
}