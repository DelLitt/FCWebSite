namespace FCCore.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Guard
    {
        public static bool IsEmptyIEnumerable<T>(IEnumerable<T> obj)
        {
            if (obj == null)
            {
                obj = new T[0];
            }

            return !obj.Any();
        }

        public static void CheckNull(object argument, string name)
        {
            if(argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
