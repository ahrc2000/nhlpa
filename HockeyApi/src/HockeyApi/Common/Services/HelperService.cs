using HockeyApi.Common.Enums;
using HockeyApi.Features;
using HockeyApi.Features.Player;
using HockeyApi.Features.RosterHistory;
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
        private readonly IRosterHistoryService _rhservice;

        public HelperService(IPlayerService pservic, IRosterTransactionService rtservice, ITeamService tservice, IRosterHistoryService rhservice)
        {
            _pservice = pservic;
            _tservice = tservice;
            _rtservice = rtservice;
            _rhservice = rhservice;
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


        public string ChangePlayerStatus(int playerId, int rttypeId, DateTime efdate)
        {

            int playerStatus = CheckPlayerStatus(playerId);
            string result = string.Empty;


            if (rttypeId == (int)TransactionType.Injured)
            {
                if (playerStatus == (int)TransactionType.Healthy || playerStatus == (int)TransactionType.Signed || playerStatus == (int)TransactionType.Traded)
                {
                    int resp = SetPlayerStatusToInjured(playerId, efdate);
                    if (resp == 1)
                    {
                        result = "Status changed successfuly";
                    }
                    else
                    {
                        result = "there was an error in the input please check again";
                    }
                }
                else
                {
                    result = "player status can not be changed due to set rules";
                }
            }
            else if (rttypeId == (int)TransactionType.Healthy)
            {
                if (playerStatus == (int)TransactionType.Injured)
                {
                    int resp = SetPlayerStatusToHealthy(playerId, efdate);
                    if (resp == 1)
                        result = "status changed successfuly";
                    else
                        result = "there was an error in the input please check again";
                }
                else
                {
                    result = "player status can not be changed due to set rules";
                }
            }

            return result;

        }

        public int SetPlayerStatusToHealthy(int playerId, DateTime efdate)
        {

            RosterTransactionModel rmodel = _rtservice.GetRtForPlayerById(playerId);
            _rhservice.InsertHistoryTransactionRecord(rmodel);
            int result = _rtservice.UpdatePlayerTType(playerId, (int)TransactionType.Healthy, efdate);
            return result;
        }

        public int SetPlayerStatusToInjured(int playerId, DateTime efdate)
        {
            RosterTransactionModel rmodel = _rtservice.GetRtForPlayerById(playerId);
            _rhservice.InsertHistoryTransactionRecord(rmodel);
            int result = _rtservice.UpdatePlayerTType(playerId, (int)TransactionType.Injured, efdate);
            return result;
        }


        public int CheckPlayerStatus(int playerId)
        {
            RosterTransactionModel rmodel = _rtservice.GetRtForPlayerById(playerId);
            return rmodel.rosterTransactionTypeId;
        }


        public string TradePlayer(int playerId, string teamCode, DateTime effdt)
        {
            int playerStatus = CheckPlayerStatus(playerId);
            string result = string.Empty;
            RosterTransactionModel rtmodel = _rtservice.GetRtForPlayerById(playerId);
            if (playerStatus != (int)TransactionType.Injured)
            {
                int resp = _rtservice.UpdatePlayerTeam(playerId, teamCode, effdt);
                if (resp == 1)
                {
                    result = $"player successufly traded to {teamCode}";
                    _rhservice.InsertHistoryTransactionRecord(rtmodel);
                }
                else
                    result = "please check the trade information";
            }

            return result;
        }


        public string SignPlayer(int playerId, DateTime effdate)
        {
            int playerStatus = CheckPlayerStatus(playerId);
            string result = string.Empty;
            RosterTransactionModel rtmodel = _rtservice.GetRtForPlayerById(playerId);
            if (playerStatus == (int)TransactionType.Healthy)
            {
                int resp = _rtservice.UpdatePlayerTType(playerId, (int)TransactionType.Signed, effdate);
                if (resp == 1)
                {
                    result = $"player successufly signed ";
                    _rhservice.InsertHistoryTransactionRecord(rtmodel);
                }
                else
                    result = "please check the trade information";
            }

            return result;
        }









    }
}
