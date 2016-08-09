namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class StadiumDal : DalBase, IStadiumDal
    {
        public bool FillCities { get; set; } = false;

        public Stadium GetStadium(int id)
        {
            Stadium stadium = Context.Stadium.FirstOrDefault(p => p.Id == id);

            if(stadium != null)
            {
                FillRelations(new Stadium[] { stadium });
            }

            return stadium;
        }

        public IEnumerable<Stadium> GetStadiums(IEnumerable<int> ids)
        {
            if (Guard.IsEmptyIEnumerable(ids)) { return new Stadium[0]; }

            IEnumerable<Stadium> stadiums = Context.Stadium.Where(t => ids.Contains(t.Id));

            FillRelations(stadiums);

            return stadiums;
        }

        public IEnumerable<Stadium> GetAll()
        {
            IEnumerable<Stadium> stadiums = Context.Stadium.ToList();

            FillRelations(stadiums);

            return stadiums;
        }

        public IEnumerable<Stadium> SearchByNameFull(string text)
        {
            IEnumerable<Stadium> stadiums = Context.Stadium.Where(v => v.NameFull.Contains(text) || v.city.NameFull.Contains(text));

            FillRelations(stadiums);

            return stadiums;
        }

        public int SaveStadium(Stadium entity)
        {
            if (entity.Id > 0)
            {
                Context.Stadium.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Stadium.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<Stadium> stadiums)
        {
            IEnumerable<City> cities = new City[0];

            if (FillCities)
            {
                var citiesDal = new CityDal();
                citiesDal.SetContext(Context);

                var citiesIds = new List<int>();
                citiesIds.AddRange(stadiums.Select(r => (int)r.cityId).Distinct());

                cities = citiesDal.GetCities(citiesIds).ToList();
            }

            if (cities.Any())
            {
                foreach (Stadium stadium in stadiums)
                {
                    if (FillCities && cities.Any())
                    {
                        stadium.city = cities.FirstOrDefault(t => t.Id == stadium.cityId);

                        if (stadium.cityId.HasValue && stadium.city == null)
                        {
                            throw new DalMappingException(nameof(stadium.city), typeof(Stadium));
                        }
                    }
                }
            }
        }
    }
}
