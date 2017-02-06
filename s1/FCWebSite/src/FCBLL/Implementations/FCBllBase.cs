namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using System.Reflection;
    using FCCore.Abstractions;
    using FCCore.Abstractions.Bll;
    using FCCore.Caching;
    using FCCore.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class FCBllBase : IFCBll
    {
        private TypeInfo typeInfo;

        private IObjectKeyGenerator objectKeyGenerator;
        public IObjectKeyGenerator ObjectKeyGenerator
        {
            get
            {
                if (objectKeyGenerator == null)
                {
                    objectKeyGenerator = MainCfg.ServiceProvider.GetService<IObjectKeyGenerator>();
                }

                return objectKeyGenerator;
            }
        }

        private IFCCache cache;
        public IFCCache Cache
        {
            get
            {
                if(cache == null)
                {
                    cache = MainCfg.ServiceProvider.GetService<IFCCache>();
                }

                return cache;
            }
        }

        public FCBllBase()
        {
            this.typeInfo = this.GetType().GetTypeInfo();
        }

        public string GetStringMethodKey(string methodName, params object[] parameters)
        {            
            MethodInfo methodInfo = typeInfo.GetMethod(methodName);

            if(methodInfo == null)
            {
                throw new KeyNotFoundException($"Unable to get string method key! Couldn't find method '{methodName}' of the type '{typeInfo}'");
            }

            return ObjectKeyGenerator.GetStringKey(methodInfo, parameters);
        }

        public string GetStringKey(string keyGroup, params object[] parameters)
        {
            return ObjectKeyGenerator.GetStringKey(keyGroup, parameters);
        }
    }
}
