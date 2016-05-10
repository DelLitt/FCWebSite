namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonCareerBll
    {
        bool FillTeams { get; set; }
        IEnumerable<PersonCareer> GetPersonCareer(int personId);
        IEnumerable<int> SavePersonCareer(IEnumerable<PersonCareer> entities);
    }
}
