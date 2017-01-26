namespace FCCore.Caching
{
    using System;
    using System.Threading.Tasks;

    public interface IFCCache
    {
        bool Enable { get; set; }
        void Add<TItem>(object key, TItem value);
        void Add<TItem>(object key, TItem value, int seconds);
        object Get(object key);
        TItem Get<TItem>(object key);
        TItem GetOrCreate<TItem>(object key, Func<TItem> getValue);
        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TItem GetOrCreate<TItem>(object key, int seconds, Func<TItem> getValue, params object[] parameters);
        TItem GetOrCreate<TItem>(object key, Func<TItem> getValue, int seconds);
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<TItem> getValue);
        /// <summary>
        /// Gets item from cache or create if it not found. Creates cache combaining key and parameters!
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <param name="getValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TItem> GetOrCreateAsync<TItem>(object key, int seconds, Func<TItem> getValue, params object[] parameters);
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<TItem> getValue, int seconds);
        void Remove(object key);
    }
}
