namespace FCCore.Caching
{
    using System.Globalization;
    using Abstractions;
    using Common;

    public class SimpleCacheKeyGenerator : IObjectKeyGenerator
    {
        private const string StringKeyTemplate = "_cache_{0}";

        private object key;

        public SimpleCacheKeyGenerator(object key)
        {
            Guard.CheckNull(key, nameof(key));

            this.key = key;
        }

        public string StringKey
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, StringKeyTemplate, key);
            }
        }
    }
}
