namespace FCWeb.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            var infoView = !string.IsNullOrWhiteSpace(person.Info)
                ? JsonConvert.DeserializeObject<PersonInfoView>(person.Info)
                : new PersonInfoView();

            return new PersonViewModel()
            {
                id = person.Id,
                active = person.Active,
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
                number = person.Number,
                personStatusId = person.personStatusId,
                roleId = person.roleId,
                teamId = person.teamId,
                weight = person.Weight,
                tempGuid = tempGuid
            };
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
