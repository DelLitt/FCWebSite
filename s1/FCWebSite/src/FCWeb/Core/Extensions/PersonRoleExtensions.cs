namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class PersonRoleExtensions
    {
        public static PersonRoleViewModel ToViewModel(this PersonRole personRole)
        {
            if (personRole == null) { return null; }

            return new PersonRoleViewModel()
            {
                id = personRole.Id,
                name = personRole.Name,
                nameFull = personRole.NameFull,
                personRoleGroupId = personRole.personRoleGroupId
            };
        }

        public static IEnumerable<PersonRoleViewModel> ToViewModel(this IEnumerable<PersonRole> personRoles)
        {
            if (Guard.IsEmptyIEnumerable(personRoles)) { return new PersonRoleViewModel[0]; }

            return personRoles.Select(v => v.ToViewModel()).ToList();
        }
    }
}
