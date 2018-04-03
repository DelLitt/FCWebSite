namespace FCCore.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Abstractions;
    using MethodKeyGeneration;
    using Xunit;

    public class FCCacheKeyGeneratorTests
    {
        private readonly IObjectKeyGenerator fcCacheKeyGenerator = new CacheKeyGenerator();
        private readonly MethodKeyGenerator methodKeyGenerator = new MethodKeyGenerator();

        [Fact]
        public void CacheKeyForPrimitiveTypesIsValid()
        {
            // Arrange
            MethodInfo methodInfo = typeof(MethodKeyGeneratorTestsHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(MethodKeyGeneratorTestsHelper.PrimitiveParametersMethod));

            string keyGroupNameExpected = "keygroup";
            string methodNameKeyExpected = MethodKeyGeneratorTestsHelper.GetMethodNameKey(methodKeyGenerator, methodInfo);            
            string parametersKeyExpected = "p0=1_p1=two_p2=true";
            string parametersMethodExpected = "a=1_b=two_c=true";

            string expectedKeyGroupResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, keyGroupNameExpected, parametersKeyExpected);
            string expectedMethodResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, methodNameKeyExpected, parametersMethodExpected);

            // Act
            string actualKeyGroupResult = fcCacheKeyGenerator.GetStringKey(keyGroupNameExpected, 1, "two", true);
            string actualMethodResult = fcCacheKeyGenerator.GetStringKey(methodInfo, 1, "two", true);

            // Assert
            Assert.Equal(expectedKeyGroupResult, actualKeyGroupResult);
            Assert.Equal(expectedMethodResult, actualMethodResult);
        }

        [Fact]
        public void CacheKeyForIEnumerableTypesIsValid()
        {
            // Arrange
            MethodInfo methodInfo = typeof(MethodKeyGeneratorTestsHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(MethodKeyGeneratorTestsHelper.IEnumerableParametersMethod));

            var arrayInt = new int[] { 1, 23, 4 };
            IEnumerable<string> ienumerableString = new List<string>() { "a1", "b2", "c3" };
            IList<bool> listBool = new List<bool>() { true, false };

            string keyGroupNameExpected = "keygroup";
            string methodNameKeyExpected = MethodKeyGeneratorTestsHelper.GetMethodNameKey(methodKeyGenerator, methodInfo);            
            string parametersKeyExpected = "p0=cnt-3:1-23-4_p1=cnt-3:a1-b2-c3_p2=cnt-2:true-false";
            string parametersMethodExpected = "d=cnt-3:1-23-4_e=cnt-3:a1-b2-c3_f=cnt-2:true-false";

            string expectedKeyGroupResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, keyGroupNameExpected, parametersKeyExpected);
            string expectedMethodResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, methodNameKeyExpected, parametersMethodExpected);

            // Act
            string actualKeyGroupResult = fcCacheKeyGenerator.GetStringKey(keyGroupNameExpected, arrayInt, ienumerableString, listBool);
            string actualMethodResult = fcCacheKeyGenerator.GetStringKey(methodInfo, arrayInt, ienumerableString, listBool);

            // Assert
            Assert.Equal(expectedKeyGroupResult, actualKeyGroupResult);
            Assert.Equal(expectedMethodResult, actualMethodResult);
        }
    }
}
