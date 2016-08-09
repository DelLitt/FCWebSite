namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IStadiumDal : IDalBase
    {
        bool FillCities { get; set; }

        Stadium GetStadium(int id);
        IEnumerable<Stadium> GetStadiums(IEnumerable<int> ids);
        IEnumerable<Stadium> GetAll();
        IEnumerable<Stadium> SearchByNameFull(string text);
        int SaveStadium(Stadium entity);
    }
}
