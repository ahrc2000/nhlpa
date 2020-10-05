using HockeyApi.Features.Player;
using HockeyApi.Features.RosterHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class PlayerTransactionDto
    {
        public PlayerTransactionDto()
        {
        }

        public PlayerTransactionDto(PlayerModel pmodel, List<RosterHistoryModel> rmodel)
        {
            player = pmodel;
            Rosterhistory = rmodel;
        }

        public PlayerModel player { get; set; }
        public List<RosterHistoryModel> Rosterhistory { get; set; }
    }
}
