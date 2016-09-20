﻿namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Bll;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;

    public class GameBll : IGameBll
    {
        public bool FillTeams
        {
            get
            {
                return DalGames.FillTeams;
            }

            set
            {
                DalGames.FillTeams = value;
            }
        }

        public bool FillRounds
        {
            get
            {
                return DalGames.FillRounds;
            }

            set
            {
                DalGames.FillRounds = value;
            }
        }

        private IGameDal dalGames;
        private IGameDal DalGames
        {
            get
            {
                if (dalGames == null)
                {
                    dalGames = DALFactory.Create<IGameDal>();
                }

                return dalGames;
            }
        }

        private IRoundDal dalRounds;
        private IRoundDal DalRounds
        {
            get
            {
                if (dalRounds == null)
                {
                    dalRounds = DALFactory.Create<IRoundDal>(DalGames);
                }

                return dalRounds;
            }
        }

        public IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date)
        {
            IEnumerable<int> roundIds = DalRounds.GetRoundIdsOfTourneys(tourneyIds, teamId);

            return GetTeamActualRoundGames(teamId, roundIds, date);
        }

        public IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date)
        {
            if(Guard.IsEmptyIEnumerable(roundIds)) { return new Game[0]; }

            Game nearestGame = DalGames.GetTeamNearestGame(teamId, roundIds, date);

            IEnumerable<Game> roundGames = null;

            if (nearestGame != null)
            {
                roundGames = DalGames.GetRoundGames(nearestGame.roundId).OrderBy(g => g.GameDate);
            }

            return roundGames ?? new Game[0];
        }

        public IEnumerable<Game> GetRoundGames(int roundId)
        {
            return DalGames.GetRoundGames(roundId);
        }

        public Game GetGame(int id)
        {
            return DalGames.GetGame(id);
        }

        public Game SaveGame(Game entity)
        {
            return DalGames.SaveGame(entity);
        }

        public IEnumerable<Game> GetGamesByTourney(int tourneyId)
        {
            return DalGames.GetGamesByTourney(tourneyId);
        }

        public IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds)
        {
            IEnumerable<int> roundIds = DalRounds.GetRoundIdsOfTourneys(tourneyIds);

            return GetGamesByRounds(roundIds);
        }

        public IEnumerable<Game> GetGamesByTourneyBetweenTeams(int tourneyId, IEnumerable<int> teamIds)
        {
            return DalGames.GetGamesByTourneyBetweenTeams(tourneyId, teamIds);
        }

        public IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds)
        {
            return DalGames.GetGamesByRounds(roundIds);
        }

        public int RemoveGame(int gameId, bool removeProtocol = true)
        {
            return DalGames.RemoveGame(gameId, removeProtocol);
        }

        public bool SaveGameNote(int gameId, string note)
        {
            Game game = DalGames.GetGame(gameId);

            if(game == null) { return false; }

            game.Note = note;

            return DalGames.SaveGame(game).Id > 0;
        }
    }
}
