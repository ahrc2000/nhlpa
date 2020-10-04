using HockeyApi.Common.Enums;
using HockeyApi.Features;
using HockeyApi.Features.Player;
using HockeyApi.Features.RosterTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.Services
{
    public class HelperService : IHelperService
    {

        private readonly IPlayerService _pservice;
        private readonly IRosterTransactionService _rtservice;
        private readonly ITeamService _tservice;

        public HelperService(IPlayerService pservic, IRosterTransactionService rtservice, ITeamService tservice)
        {
            _pservice = pservic;
            _tservice = tservice;
            _rtservice = rtservice;
        }



        public int AddPlayerToTeam(string fname, string lname, string tcode, DateTime dt)
        {
            if (TeamHasCapacity(tcode))
            {
                int pid = _pservice.AddNewPlayer(fname, lname);
                int playerId = _pservice.GetLatestPlayer();
                RosterTransactionModel rtm = new RosterTransactionModel()
                {
                    rosterTransactionTypeId = (int)TransactionType.Signed,
                    player_id = playerId,
                    effective_date = dt,
                    team_code = tcode
                };
                int rtId = _rtservice.AddPlayerTransaction(rtm);
                return rtId;
            }
            else
                return -1;
        }

        private bool TeamHasCapacity(string tcode)
        {
            List<RosterTransactionModel> teamDetail = new List<RosterTransactionModel>();
            teamDetail = (List<RosterTransactionModel>)_rtservice.getActivePlayersByTeamCode(tcode);

            return teamDetail.Count < 10 ? true : false;
        }


        public int ChangePlayerStatus(int playerId, int rttypeId) {
        

        }

        public int SetPlayerStatusToHealthy(int playerId) { }

        public int setPlayerStatusToInjured(int playerId) { }


        public int CheckPlayerStatus(int playerId) { }


        public int TradePlayer(int playerId, string teamCode, DateTime effdt) { }












    }
}
