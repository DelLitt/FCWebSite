namespace FCCore.Extensions
{
    using System.Reflection;
    using Helpers;

    public static class ReflectionExtensions
    {
        public static bool IsSimple(this TypeInfo typeInfo)
        {
            return TypeHelper.CheckIfTypeSimple(typeInfo);
        }
    }
}
