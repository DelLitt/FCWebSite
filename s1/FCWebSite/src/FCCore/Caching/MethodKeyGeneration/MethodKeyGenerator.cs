namespace FCCore.Caching.MethodKeyGeneration
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using Extensions;

    public class MethodKeyGenerator
    {
        public int MaxArrayLenght { get; set; } = 10;
        public int MaxParameterValueLenght { get; set; } = 100;
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

                parameterName = parameterInfo.Name;
                var iEnumerable = parameterPassed as IEnumerable;

                if (typeInfo.IsSimple())
                {
                    parameterValue = parameters[i].ToString();
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

                parametersKey += string.Format(
                    CultureInfo.InvariantCulture, 
                    ParameterTemplate, 
                    parameterName, 
                    parameterValue);

                parametersKey += (i == parameters.Length - 1 ? string.Empty : ParametersDelimeter);
            }

            return parametersKey;
        }

        private string GetIEnumerableValue(IEnumerable parameter)
        {
            string parameterValue = string.Empty;

            int arrayLenght = 0;
            string currentValue = string.Empty;
            IEnumerator enumerator = parameter.GetEnumerator();

            while (enumerator.MoveNext())
            {
                currentValue = enumerator.Current.ToString();

                if (parameterValue.Length + currentValue.Length > MaxParameterValueLenght) { break; }

                parameterValue += currentValue;
                arrayLenght++;

                if (arrayLenght > MaxArrayLenght) { break; }
            }

            return parameterValue;
        }
    }
}
