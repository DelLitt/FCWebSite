namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IStadiumBll
    {
        bool FillCities { get; set; }
        Stadium GetStadium(int id);
        IEnumerable<Stadium> GetAll();
        IEnumerable<Stadium> SearchByNameFull(string text);
        int SaveStadium(Stadium entity);
    }
}
