namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonCareerDal : IDalBase
    {
        bool FillTeams { get; set; }
        IEnumerable<PersonCareer> GetPersonCareer(int personId);
        IEnumerable<int> SavePersonCareer(IEnumerable<PersonCareer> entities);
    }
}
