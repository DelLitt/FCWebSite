namespace FCCore.Helpers
{
    using System;
    using System.Reflection;

    public class TypeHelper
    {
        public static bool CheckIfTypeSimple(TypeInfo typeInfo)
        {
            if (typeInfo.IsGenericParameter && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return CheckIfTypeSimple(typeInfo.GetGenericArguments()[0].GetTypeInfo());
            }

            return typeInfo.IsPrimitive
              || typeInfo.IsEnum
              || typeInfo.Equals(typeof(string))
              || typeInfo.Equals(typeof(decimal))
              || typeInfo.Equals(typeof(DateTime));
        }
    }
}
