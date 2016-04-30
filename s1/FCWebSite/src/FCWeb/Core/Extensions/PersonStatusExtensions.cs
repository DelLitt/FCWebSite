namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class PersonStatusExtensions
    {
        public static PersonStatusViewModel ToViewModel(this PersonStatus personStatus)
        {
            if (personStatus == null) { return null; }

            return new PersonStatusViewModel()
            {
                id = personStatus.Id,
                name = personStatus.Name,
                nameFull = personStatus.NameFull
            };
        }

        public static IEnumerable<PersonStatusViewModel> ToViewModel(this IEnumerable<PersonStatus> personStatuses)
        {
            if (Guard.IsEmptyIEnumerable(personStatuses)) { return new PersonStatusViewModel[0]; }

            return personStatuses.Select(v => v.ToViewModel()).ToList();
        }
    }
}
