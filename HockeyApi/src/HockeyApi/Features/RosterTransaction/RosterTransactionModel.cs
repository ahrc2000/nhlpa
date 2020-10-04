using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.RosterTransaction
{
    public class RosterTransactionModel
    {
        public RosterTransactionModel(int rtid, int rtTypeid, int pid, string tc, DateTime effDate)
        {
            rosterTranscationId = rtid;
            rosterTransactionTypeId = rtTypeid;
            player_id = pid;
            team_code = tc;
            effDate = effective_date;

        }


        public int rosterTranscationId { get; set; }
        public int rosterTransactionTypeId { get; set; }
        public int player_id { get; set; }
        public string team_code { get; set; }
        public DateTime effective_date { get; set; }


    }
}
