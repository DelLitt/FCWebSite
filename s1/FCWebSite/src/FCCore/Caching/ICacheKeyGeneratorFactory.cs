namespace FCCore.Caching
{
    using Abstractions;

    public interface ICacheKeyGeneratorFactory
    {
        IObjectKeyGenerator Create(object key, params object[] parameters);
    }
}
