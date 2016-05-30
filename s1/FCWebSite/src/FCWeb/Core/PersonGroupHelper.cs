namespace FCWeb.Core
{
    using FCCore.Common;

    public class PersonGroupHelper
    {
        public static PersonGroup FromString(string personGroup)
        {
            return EnumUtils.FromString<PersonGroup>(personGroup);
        }
    }
}
