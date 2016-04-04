﻿using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Bll
{
    public interface IRoundBll
    {
        bool FillTourneys { get; set; }
        IEnumerable<Round> GetRounds(IEnumerable<int> ids);
        IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null);
    }
}