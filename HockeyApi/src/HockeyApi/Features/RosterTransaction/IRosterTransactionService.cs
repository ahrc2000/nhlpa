using System.Collections.Generic;

namespace HockeyApi.Features.RosterTransaction
{
    public interface IRosterTransactionService
    {
        int AddPlayerTransaction(RosterTransactionModel rmodel);
        IEnumerable<RosterTransactionModel> GetAll();
        RosterTransactionModel GetRtForPlayerById(int id);
        IEnumerable<RosterTransactionModel> getRtForTeamByTeamCode(string tcode);
    }
}