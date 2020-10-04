using System;
using System.Collections.Generic;

namespace HockeyApi.Features.RosterTransaction
{
    public interface IRosterTransactionService
    {
        int AddPlayerTransaction(RosterTransactionModel rmodel);
        List<RosterTransactionModel> getActivePlayersByTeamCode(string tcode);
        IEnumerable<RosterTransactionModel> GetAll();
        RosterTransactionModel GetRtForPlayerById(int id);
        IEnumerable<RosterTransactionModel> getRtForTeamByTeamCode(string tcode);
        int UpdatePlayerTeam(int playerId, string teamcode, DateTime effdate);
        int UpdatePlayerTType(int playerId, int rtranstype, DateTime efdate);
    }
}