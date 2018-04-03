namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonCareerDal : IDalBase
    {
        bool FillTeams { get; set; }

        IEnumerable<PersonCareer> GetPersonCareer(int personId);
        IEnumerable<PersonCareer> GetPersonCareer(IEnumerable<int> personId);
        IEnumerable<int> SavePersonCareer(IEnumerable<PersonCareer> entities);
    }
}
