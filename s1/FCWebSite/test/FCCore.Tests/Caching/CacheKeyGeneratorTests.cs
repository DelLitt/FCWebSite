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

            string methodNameKeyExpected = MethodKeyGeneratorTestsHelper.GetMethodNameKey(methodKeyGenerator, methodInfo);
            string parametersKeyExpected = "a=1_b=two_c=true";

            string expectedResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, methodNameKeyExpected, parametersKeyExpected);

            // Act
            var actualResult = fcCacheKeyGenerator.GetStringKey(methodInfo, 1, "two", true);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CacheKeyForIEnumerableTypesIsValid()
        {
            // Arrange
            MethodInfo methodInfo = typeof(MethodKeyGeneratorTestsHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(MethodKeyGeneratorTestsHelper.IEnumerableParametersMethod));

            var arrayInt = new int[] { 1, 2, 3 };
            IEnumerable<string> ienumerableString = new List<string>() { "a1", "b2", "c3" };
            IList<bool> listBool = new List<bool>() { true, false };

            string methodNameKeyExpected = MethodKeyGeneratorTestsHelper.GetMethodNameKey(methodKeyGenerator, methodInfo);
            string parametersKeyExpected = "d=123_e=a1b2c3_f=truefalse";

            string expectedResult = MethodKeyGeneratorTestsHelper.GetMethodCacheKey(fcCacheKeyGenerator, methodNameKeyExpected, parametersKeyExpected);

            // Act
            var actualResult = fcCacheKeyGenerator.GetStringKey(methodInfo, arrayInt, ienumerableString, listBool);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }


    }
}
