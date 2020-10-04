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
        int UpdatePlayerTeam(int playerId, string teamcode);
        int UpdatePlayerTType(int playerId, int rtranstype);
    }
}