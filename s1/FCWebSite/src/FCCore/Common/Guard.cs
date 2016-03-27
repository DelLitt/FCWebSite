namespace FCCore.Common
{
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
    }
}
