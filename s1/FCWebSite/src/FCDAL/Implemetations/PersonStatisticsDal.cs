namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class PersonStatisticsDal : DalBase, IPersonStatisticsDal
    {
        public PersonStatistics GetPersonStatistics(int personId)
        {
            return Context.PersonStatistics.FirstOrDefault(ps => ps.personId == personId);
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId)
        {
            return Context.PersonStatistics.Where(ps => ps.teamId == temaId && ps.tourneyId == tourneyId);
        }

        public int SavePersonStatistics(PersonStatistics entity)
        {
            if (entity.Id > 0)
            {
                Context.PersonStatistics.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.PersonStatistics.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
