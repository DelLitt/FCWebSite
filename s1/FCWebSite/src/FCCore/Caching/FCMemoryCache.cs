namespace FCCore.Caching
{
    using System;
    using System.Threading.Tasks;
    using Abstractions;
    using Configuration;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class FCMemoryCache : IFCCache
    {
        private IMemoryCache cache = MainCfg.ServiceProvider.GetService<IMemoryCache>();
        private ILogger logger = MainCfg.ServiceProvider.GetService<ILogger<FCMemoryCache>>();
        private IObjectKeyGenerator objectKeyGenerator = MainCfg.ServiceProvider.GetService<IObjectKeyGenerator>();

        private bool enable = MainCfg.CacheEnabled;
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                if(!value.Equals(enable))
                {
                    logger.LogTrace("Cache was {0}.", value ? "enabled" : "disabled");
                }

                enable = value;
            }
        }

        public void Add<TItem>(object key, TItem value)
        {
            Add(key, value, MainCfg.CacheDefaultSeconds);
        }

        public void Add<TItem>(object key, TItem value, int seconds)
        {
            if (value == null)
            {
                logger.LogTrace("Couldn't add the item with key '{0}' to cache due to null value!", key);
                return;
            }

            DateTime expirationTime = DateTime.Now.AddSeconds(seconds);
            cache.Set(key, value, new DateTimeOffset(expirationTime));

            logger.LogTrace(
                "The item with key '{0}' was added to cache for '{1}' seconds until '{3}'",
                key,
                seconds,
                expirationTime);
        }

        public object Get(object key)
        {
            if (!Enable)
            {
                return null;
            }

            return cache.Get(key);
        }

        public TItem Get<TItem>(object key)
        {
            if (!Enable)
            {
                return default(TItem);
            }

            return cache.Get<TItem>(key);
        }

        public TItem GetOrCreate<TItem>(object key, Func<TItem> getValue)
        {
            return GetOrCreate(key, getValue, MainCfg.CacheDefaultSeconds);
        }

        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TItem GetOrCreate<TItem>(object key, Func<TItem> getValue, params object[] parameters)
        {
            return GetOrCreate(key, MainCfg.CacheDefaultSeconds, () => { return getValue(); }, parameters);
        }

        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TItem GetOrCreate<TItem>(object key, int seconds, Func<TItem> getValue, params object[] parameters)
        {
            string parametrizedKey = objectKeyGenerator.GetStringKey(key.ToString(), parameters);
            return GetOrCreate(parametrizedKey, () => { return getValue(); }, seconds);
        }

        public TItem GetOrCreate<TItem>(object key, Func<TItem> getValue, int seconds)
        {
            if (!Enable)
            {
                return default(TItem);
            }

            return cache.GetOrCreate(key, entry =>
            {
                PrepareNewCacheEntry(key, getValue, seconds, ref entry);
                return (TItem)entry.Value;
            });
        }

        public Task<TItem> GetOrCreateAsync<TItem>(object key, Func<TItem> getValue)
        {
            return GetOrCreateAsync(key, getValue, MainCfg.CacheDefaultSeconds);
        }

        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<TItem> GetOrCreateAsync<TItem>(object key, Func<TItem> getValue, params object[] parameters)
        {
            return GetOrCreateAsync(key, MainCfg.CacheDefaultSeconds, () => { return getValue(); }, parameters);
        }

        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<TItem> GetOrCreateAsync<TItem>(object key, int seconds, Func<TItem> getValue, params object[] parameters)
        {
            string parametrizedKey = objectKeyGenerator.GetStringKey(key.ToString(), parameters);
            return GetOrCreateAsync(parametrizedKey, () => { return getValue(); }, seconds);
        }

        public Task<TItem> GetOrCreateAsync<TItem>(object key, Func<TItem> getValue, int seconds)
        {
            if (!Enable)
            {
                return null;
            }

            return cache.GetOrCreateAsync(key, entry =>
            {
                PrepareNewCacheEntry(key, getValue, seconds, ref entry);
                return Task.FromResult((TItem)entry.Value);
            });
        }

        public void Remove(object key)
        {
            cache.Remove(key);
        }

        private void PrepareNewCacheEntry<TItem>(object key, Func<TItem> getValue, int seconds, ref ICacheEntry entry)
        {
            DateTime expirationTime = DateTime.Now.AddSeconds(seconds);

            entry.AbsoluteExpiration = new DateTimeOffset(expirationTime);
            entry.Value = getValue();

            logger.LogTrace(
                    "The item with key '{0}' was not found in cahce and was added for '{1}' seconds until '{3}'",
                    key,
                    seconds,
                    expirationTime);
        }
    }
}
