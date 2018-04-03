namespace FCCore.Abstractions
{
    using System.Reflection;

    public interface IObjectKeyGenerator
    {
        int MaxStringKeyLenght { get; set; }
        string StringParametrizedKeyTemplate { get; set; }
        bool Lower { get; set; }
        string GetStringKey(string keyGroup, params object[] parameters);
        string GetStringKey(MethodInfo methodInfo, params object[] parameters);
    }
}
