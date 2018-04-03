namespace FCWeb.Core.Extensions.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels;
    using ViewModels.Team;

    public static class TeamViewModelExtensions
    {
        public static TeamViewModel FillCustomCoaches(this TeamViewModel team)
        {
            if(team == null) { return null; }
            if(team.descriptionData == null) { return team; }
            if(team.descriptionData.fakeInfo == null) { return team; }

            if(team.descriptionData.fakeInfo.coaches == null)
            {
                team.descriptionData.fakeInfo.coaches = new EntityLinkViewModel[] { };
                return team;
            }

            var personBll = MainCfg.ServiceProvider.GetService<IPersonBll>();

            IEnumerable<int> personsIds = team.descriptionData.fakeInfo.coaches.Select(c => int.Parse(c.id));
            IEnumerable<Person> persons = personBll.GetPersons(personsIds);

            foreach (EntityLinkViewModel entityLink in team.descriptionData.fakeInfo.coaches)
            {
                Person person = persons.FirstOrDefault(p => p.Id == int.Parse(entityLink.id));

                if(person == null) { continue; }

                entityLink.FromPerson(person);
            }

            return team;
        }
    }
}
