namespace FCCore.Caching
{
    using System.Globalization;
    using System.Reflection;
    using Abstractions;
    using MethodKeyGeneration;

    public class CacheKeyGenerator : IObjectKeyGenerator
    {
        private MethodKeyGenerator methodKeyGenerator = new MethodKeyGenerator();

        public int MaxStringKeyLenght { get; set; } = 150;
        public string StringParametrizedKeyTemplate { get; set; } = "_key_{0}_prms_{1}";
        public bool Lower { get; set; } = true;

        public string GetStringKey(string keyGroup, params object[] parameters)
        {
            string parametersKey = string.Empty;

            for(int i = 0; i < parameters.Length; i++)
            {
                parametersKey += $"p{i}={parameters[i]}";
                parametersKey += (i == parameters.Length - 1 ? string.Empty : "_");
            }

            return GetParametrizedKey(keyGroup, parametersKey);
        }

        public string GetStringKey(MethodInfo methodInfo, params object[] parameters)
        {
            string methodNameKey = methodKeyGenerator.GetMethodNameKey(methodInfo);
            string methodParametersKey = methodKeyGenerator.GetMethodParametersKey(methodInfo, parameters);

            return GetParametrizedKey(methodNameKey, methodParametersKey);
        }

        private string GetParametrizedKey(string keyGroup, string parametersKey)
        {
            string key = string.Format(CultureInfo.InvariantCulture, StringParametrizedKeyTemplate, keyGroup, parametersKey);
            string result = key.Length > MaxStringKeyLenght ? key.Substring(key.Length - MaxStringKeyLenght) : key;

            return Lower ? result.ToLowerInvariant() : result;
        }
    }
}
