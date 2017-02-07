namespace FCCore.Caching.MethodKeyGeneration
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using Extensions;

    public class MethodKeyGenerator
    {
        public int MaxArrayLenght { get; set; } = 50;
        public int MaxParameterValueLenght { get; set; } = 500;
        public string MethodNameKeyTemplate { get; set; } = "{0}.{1}";
        public string ParameterTemplate { get; set; } = "{0}={1}";
        public string ParametersDelimeter { get; set; } = "_";

        public string GetMethodNameKey(MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;
            string methodClassFullName = methodInfo.DeclaringType.FullName;

            return string.Format(CultureInfo.InvariantCulture, MethodNameKeyTemplate, methodClassFullName, methodName);
        }

        public string GetMethodParametersKey(MethodInfo methodInfo, params object[] parameters)
        {
            ParameterInfo[] parametersInfo = methodInfo.GetParameters();
            string methodName = methodInfo.Name;
            string methodClassFullName = methodInfo.DeclaringType.FullName;
            string methodNameKey = GetMethodNameKey(methodInfo);

            if (parametersInfo.Length != parameters.Length)
            {
                throw new InvalidOperationException($"Number of parameters of the method '{methodClassFullName}' is not equal to the number of passed parameters!");
            }

            TypeInfo typeInfo = null;
            TypeInfo typeInfoPassed = null;
            ParameterInfo parameterInfo = null;
            object parameterPassed = null;
            string parametersKey = string.Empty;
            string parameterName = string.Empty;
            string parameterValue = string.Empty;

            for (int i = 0; i < parametersInfo.Length; i++)
            {
                parameterValue = string.Empty;
                parameterInfo = parametersInfo[i];
                parameterPassed = parameters[i];
                typeInfo = parameterInfo.ParameterType.GetTypeInfo();
                typeInfoPassed = parameterPassed.GetType().GetTypeInfo();

                if (!typeInfo.IsAssignableFrom(typeInfoPassed))
                {
                    throw new InvalidCastException($"Passed type '{typeInfoPassed}' of the parameter '{parameterInfo.Name}' is not equal to '{typeInfo}'.");
                }

                parametersKey += GetParameterValue(parameterInfo.Name, parameterPassed, typeInfo);
                parametersKey += (i == parameters.Length - 1 ? string.Empty : ParametersDelimeter);
            }

            return parametersKey;
        }

        public string GetParameterValue(string parameterName, object parameterPassed, TypeInfo typeInfo)
        {
            string parameterValue = string.Empty;
            var iEnumerable = parameterPassed as IEnumerable;

            if (typeInfo.IsSimple())
            {
                parameterValue = parameterPassed.ToString();
            }
            else if (iEnumerable != null)
            {
                parameterValue = GetIEnumerableValue(iEnumerable);
            }
            else
            {
                parameterValue = typeInfo.Name;
            }

            parameterValue =
                parameterValue.Length < MaxParameterValueLenght
                ? parameterValue
                : parameterValue.Substring(0, MaxParameterValueLenght);

            if (!string.IsNullOrWhiteSpace(parameterValue))
            {
                parameterValue = parameterValue.Replace(" ", string.Empty);
            }

            string parametersKey = string.Format(
                                            CultureInfo.InvariantCulture,
                                            ParameterTemplate,
                                            parameterName,
                                            parameterValue);

            return parametersKey;
        }

        private string GetIEnumerableValue(IEnumerable parameter)
        {
            const char delimeter = '-';
            string parameterValue = string.Empty;

            int arrayLenght = 0;
            string currentValue = string.Empty;
            IEnumerator enumerator = parameter.GetEnumerator();

            while (enumerator.MoveNext())
            {
                currentValue = enumerator.Current.ToString();
                arrayLenght++;

                if (parameterValue.Length + currentValue.Length > MaxParameterValueLenght || arrayLenght > MaxArrayLenght)
                {                    
                    continue;
                }

                parameterValue += currentValue + delimeter;                
            }

            parameterValue = "cnt-" + arrayLenght + ":" + parameterValue;

            return parameterValue.Length > 1 ? parameterValue.Substring(0, parameterValue.Length - 1) : parameterValue;
        }
    }
}
