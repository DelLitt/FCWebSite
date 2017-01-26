namespace FCCore.Caching
{
    using System.Linq;
    using Abstractions;

    public class CacheKeyGeneratorFactory : ICacheKeyGeneratorFactory
    {
        public IObjectKeyGenerator Create(object key, params object[] parameters)
        {
            if(parameters.Any())
            {
                return new ParameterizedCacheKeyGenerator(key, parameters);
            }

            return new SimpleCacheKeyGenerator(key);
        }
    }
}
