namespace FCDAL.Implemetations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class TeamDal : DalBase, ITeamDal
    {
        public Team GetTeam(int id)
        {
            return Context.Team.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Team> GetTeams(IEnumerable<int> ids)
        {
            if(ids == null) { return new Team[0]; }

            return Context.Team.Where(t => ids.Contains(t.Id));
        }

        public IEnumerable<Team> SearchByName(string text)
        {
            return Context.Team.Where(v => v.Name.Contains(text));
        }

        public int SaveTeam(Team entity)
        {
            if (entity.Id > 0)
            {
                Context.Team.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Team.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
