using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.RosterHistory
{
    public class RosterHistoryModel
    {

        public RosterHistoryModel(int rthid, int rtid, int rttid, int pid, string tc, DateTime efd)
        {
            roster_trans_history_id = rthid;
            roster_trans_id = rtid;
            roster_trans_type_id = rttid;
            player_id = pid;
            team_code = tc;
            effective_date = efd;
        }

        public int roster_trans_history_id { get; set; }
        public int roster_trans_id { get; set; }

        public int roster_trans_type_id { get; set; }
        public int player_id { get; set; }
        public string team_code { get; set; }
        public DateTime effective_date {get;set;}


    }
}
