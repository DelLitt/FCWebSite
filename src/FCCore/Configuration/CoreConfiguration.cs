namespace FCCore.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class CoreConfiguration : ICoreConfiguration
    {
        public CoreConfiguration(IConfigurationRoot configurationRoot)
        {
            this.Current = configurationRoot;
        }

        public IConfigurationRoot Current { get; private set; }
    }
}
