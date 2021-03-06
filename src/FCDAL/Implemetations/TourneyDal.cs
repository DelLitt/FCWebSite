﻿namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class TourneyDal : DalBase, ITourneyDal
    {
        public bool FillRounds { get; set; }
        public bool FillGames { get; set; }
        public bool FillProtocols { get; set; }

        public Tourney GetTourney(int tourneyId)
        {
            Tourney tourney = Context.Tourney.FirstOrDefault(t => t.Id == tourneyId);

            FillRelations(new Tourney[] { tourney });

            return tourney;
        }

        public Tourney GetTourneyByRoundId(int roundId)
        {
            Tourney tourney = Context.Tourney.FirstOrDefault(t => t.Round.Any(r => r.Id == roundId));

            FillRelations(new Tourney[] { tourney });

            return tourney;
        }

        public IEnumerable<Tourney> GetAll()
        {
            return Context.Tourney;
        }

        public IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids)
        {
            if (ids == null) { return new Tourney[0]; }

            return Context.Tourney.Where(r => ids.Contains(r.Id));
        }

        public IEnumerable<Tourney> SearchByNameFull(string text)
        {
            return Context.Tourney.Where(v => v.NameFull.Contains(text));
        }

        public Tourney SaveTourney(Tourney entity)
        {
            if (entity.Id > 0)
            {
                Context.Tourney.Update(entity);
            }
            else
            {
                Context.Tourney.Add(entity);
            }

            Context.SaveChanges();

            return entity;
        }

        private void FillRelations(IEnumerable<Tourney> tourneys)
        {
            if (Guard.IsEmptyIEnumerable(tourneys)) { return; }

            RoundDal dalRounds = null;

            if(FillProtocols)
            {
                FillGames = true;
            }

            if(FillGames)
            {
                FillRounds = true;
            }

            if (FillRounds)
            {
                dalRounds = new RoundDal();
                dalRounds.FillGames = FillGames;
                dalRounds.FillProtocols = FillProtocols;
                dalRounds.SetContext(Context);
            }

            if(dalRounds != null)
            {
                foreach(Tourney tourney in tourneys)
                {
                    if(dalRounds != null)
                    {
                        tourney.Round = dalRounds.GetRoundsByTourney(tourney.Id).ToArray();
                    }
                }
            }
        }
    }
}
