namespace FCCore.Caching.MethodKeyGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class MethodKeyGeneratorTests
    {
        private readonly MethodKeyGenerator methodKeyGenerator = new MethodKeyGenerator();

        [Fact]
        public void MethodNameKeyCorrespondsToTemplate()
        {
            // Arrange
            MethodInfo methodInfo = typeof(MethodKeyGeneratorTestsHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(MethodKeyGeneratorTestsHelper.NamedMethod));

            string methodName = methodInfo.Name;
            string methodClassFullName = methodInfo.DeclaringType.FullName;

            // Act
            string expectedResult = MethodKeyGeneratorTestsHelper.GetMethodNameKey(methodKeyGenerator, methodInfo);
            string actualResult = methodKeyGenerator.GetMethodNameKey(methodInfo);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void MethodParametersKeyCorrespondsToTemplateAndDelimeter()
        {
            // Arrange
            MethodInfo methodInfo = typeof(MethodKeyGeneratorTestsHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(MethodKeyGeneratorTestsHelper.TwoParametersMethod));

            int a = 1;
            int b = 2;

            ParameterInfo[] parametersInfo = methodInfo.GetParameters();

            string parameter1 = string.Format(CultureInfo.InvariantCulture, 
                                              methodKeyGenerator.ParameterTemplate, 
                                              parametersInfo[0].Name, 
                                              a);

            string parameter2 = string.Format(CultureInfo.InvariantCulture,
                                              methodKeyGenerator.ParameterTemplate,
                                              parametersInfo[1].Name,
                                              b);

            // Act
            string expectedResult = $"{parameter1}{methodKeyGenerator.ParametersDelimeter}{parameter2}";

            string actualResult = methodKeyGenerator.GetMethodParametersKey(methodInfo, a, b);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
