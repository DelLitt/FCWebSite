namespace FCCore.Extensions
{
    using Model;

    public static class PersonExtensions
    {
        public static string GetNameDefault(this Person person)
        {
            return person.NameFirst + " " + person.NameLast;
        }
    }
}
