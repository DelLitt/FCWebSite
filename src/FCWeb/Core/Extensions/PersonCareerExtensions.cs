namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class PersonCareerExtensions
    {
        public static PersonCareerViewModel ToViewModel(this PersonCareer personCareer)
        {
            if (personCareer == null) { return null; }

            return new PersonCareerViewModel()
            {               
                id = personCareer.Id,
                dateFinish = personCareer.DateFinish,
                dateStart = personCareer.DateStart,
                personId = personCareer.personId,
                teamId = personCareer.teamId,
                teamName = personCareer?.team?.Name ?? string.Empty
            };
        }

        public static IEnumerable<PersonCareerViewModel> ToViewModel(this IEnumerable<PersonCareer> personCareers)
        {
            if (Guard.IsEmptyIEnumerable(personCareers)) { return new PersonCareerViewModel[0]; }

            return personCareers.Select(v => v.ToViewModel()).ToList();
        }

        public static PersonCareer ToBaseModel(this PersonCareerViewModel personCareerView)
        {
            if (personCareerView == null) { return null; }

            return new PersonCareer()
            {
                Id = personCareerView.id,
                DateFinish = personCareerView.dateFinish,
                DateStart = personCareerView.dateStart,
                personId = personCareerView.personId,
                teamId = personCareerView.teamId
            };
        }

        public static IEnumerable<PersonCareer> ToBaseModel(this IEnumerable<PersonCareerViewModel> personCareerViews)
        {
            if (Guard.IsEmptyIEnumerable(personCareerViews)) { return new PersonCareer[0]; }

            return personCareerViews.Select(v => v.ToBaseModel()).ToList();
        }
    }
}
