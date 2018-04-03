namespace FCCore.Abstractions.Bll
{
    using Caching;

    public interface IFCBll
    {
        IFCCache Cache { get; }
        IObjectKeyGenerator ObjectKeyGenerator { get; }
    }
}
