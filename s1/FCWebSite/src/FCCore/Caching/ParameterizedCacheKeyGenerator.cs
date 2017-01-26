namespace FCCore.Caching
{
    using System.Globalization;
    using Abstractions;
    using Common;

    public class ParameterizedCacheKeyGenerator : IObjectKeyGenerator
    {
        private const string StringKeyTemplate = "_cache_{0}_prms_{1}";

        private object key;
        private object[] parameters;

        public ParameterizedCacheKeyGenerator(object key, params object[] parameters)
        {
            Guard.CheckNull(key, nameof(key));

            this.key = key;
            this.parameters = parameters ?? new object[] {};
        }

        private string stringKey = string.Empty;
        public string StringKey
        {
            get
            {
                if(string.IsNullOrWhiteSpace(stringKey))
                {
                    stringKey = string.Format(CultureInfo.InvariantCulture, StringKeyTemplate, key, GetStringParametersKey());
                }

                return stringKey;
            }
        }

        private string GetStringParametersKey()
        {
            string parametersKey = string.Empty;

            for (int i = 0; i < parameters.Length; i++)
            {
                parametersKey += "p" + i.ToString() + "=" + parameters[i].ToString();
            }

            return parametersKey;
        }
    }
}
