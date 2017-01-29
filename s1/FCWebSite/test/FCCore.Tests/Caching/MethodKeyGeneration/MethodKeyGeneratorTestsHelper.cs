namespace FCCore.Caching.MethodKeyGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Abstractions;

    public class MethodKeyGeneratorTestsHelper
    {
        public static void NamedMethod()
        {
        }

        public static void TwoParametersMethod(int a, int b)
        {
        }

        public static void PrimitiveParametersMethod(int a, string b, bool c)
        {
        }

        public static void IEnumerableParametersMethod(int[] d, IEnumerable<string> e, IList<bool> f)
        {
        }

        public static string GetMethodNameKey(MethodKeyGenerator methodKeyGenerator, MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;
            string methodClassFullName = methodInfo.DeclaringType.FullName;

            return string.Format(CultureInfo.InvariantCulture, methodKeyGenerator.MethodNameKeyTemplate, methodClassFullName, methodName);
        }

        public static string GetMethodCacheKey(IObjectKeyGenerator objectKeyGenerator,  string methodNameKey, string parametersKey)
        {
            return string.Format(CultureInfo.InvariantCulture, 
                                 objectKeyGenerator.StringParametrizedKeyTemplate, 
                                 methodNameKey, 
                                 parametersKey)
                         .ToLowerInvariant();
        }
    }
}
