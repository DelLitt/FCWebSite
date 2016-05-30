namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EnumUtils
    {
        public static T FromString<T>(string value) where T : struct
        {
            return PrivateFromString<T>(value);
        }

        public static T FromString<T>(string value, T defaultValue = default(T)) where T : struct
        {
            return PrivateFromString<T>(value, defaultValue);
        }

        private static T PrivateFromString<T>(string value, T defaultValue = default(T)) where T: struct
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }
    }
}
