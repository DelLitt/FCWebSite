namespace FCCore.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EnumUtils
    {
        public static T FromString<T>(string value) where T : struct
        {
            return PrivateFromString<T>(value);
        }

        public static T FromString<T>(string value, T defaultValue = default(T)) where T : struct
        {
            return PrivateFromString(value, defaultValue);
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

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<int> GetDigitValues<T>()
        {
            IEnumerable<T> values = Enum.GetValues(typeof(T)).Cast<T>();

            return values.Select(v => Convert.ToInt32(v));
        }
    }
}
