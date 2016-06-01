namespace FCWeb.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using Newtonsoft.Json;
    using ViewModels;

    public static class PersonExtensions
    {
        public static PersonViewModel ToViewModel(this Person person)
        {
            return PersonToViewModel(person, null);
        }

        public static PersonViewModel ToViewModel(this Person person, IEnumerable<PersonCareerViewModel> personCareers)
        {
            return PersonToViewModel(person, personCareers);
        }

        private static PersonViewModel PersonToViewModel(Person person, IEnumerable<PersonCareerViewModel> personCareers)
        {
            if (person == null) { return null; }

            Guid? tempGuid = person.Id == 0 ? Guid.NewGuid() : (Guid?)null;

            PersonInfoView infoView;
            if (!string.IsNullOrWhiteSpace(person.Info))
            {
                try
                {
                    infoView = JsonConvert.DeserializeObject<PersonInfoView>(person.Info);
                }
                catch (JsonReaderException)
                {
                    // TODO: Add item to event log
                    infoView = new PersonInfoView();
                }
            }
            else
            {
                infoView = new PersonInfoView();
            }

            return new PersonViewModel()
            {
                id = person.Id,
                active = person.Active,
                age = person.BirthDate.HasValue ? DateTimeHelper.GetAge(person.BirthDate.Value) : 0,
                birthDate = person.BirthDate,
                career = personCareers ?? new PersonCareerViewModel[0],
                cityId = person.cityId,
                customIntValue = person.CustomIntValue,
                height = person.Height,
                image = person.Image,
                info = infoView,
                nameFirst = person.NameFirst,
                nameLast = person.NameLast,
                nameMiddle = person.NameMiddle,
                nameNick = person.NameNick,
                nameDefault = person.NameFirst + " " + person.NameLast,
                number = person.Number,
                personStatusId = person.personStatusId,
                roleId = person.roleId,
                teamId = person.teamId,
                weight = person.Weight,
                tempGuid = tempGuid
            };
        }

        public static IEnumerable<PersonViewModel> ToViewModel(this IEnumerable<Person> persons)
        {
            if (Guard.IsEmptyIEnumerable(persons)) { return new PersonViewModel[0]; }

            return persons.Select(v => v.ToViewModel()).ToList();
        }

        public static Person ToBaseModel(this PersonViewModel personView)
        {
            if (personView == null) { return null; }

            string info = personView.info != null
                ? JsonConvert.SerializeObject(personView.info)
                : string.Empty;

            return new Person()
            {
                Id = personView.id,
                Active = personView.active,
                BirthDate = personView.birthDate,
                cityId = personView.cityId,
                CustomIntValue = personView.customIntValue,
                Height = personView.height,
                Image = personView.image,
                Info = info,
                NameFirst = personView.nameFirst,
                NameLast = personView.nameLast,
                NameMiddle = personView.nameMiddle,
                NameNick = personView.nameNick,
                Number = personView.number,
                personStatusId = personView.personStatusId,
                roleId = personView.roleId,
                teamId = personView.teamId,
                Weight = personView.weight
            };
        }
    }
}
