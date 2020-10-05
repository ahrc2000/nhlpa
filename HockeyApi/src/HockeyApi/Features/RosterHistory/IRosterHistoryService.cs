using HockeyApi.Features.RosterTransaction;
using System.Collections.Generic;

namespace HockeyApi.Features.RosterHistory
{
    public interface IRosterHistoryService
    {
        void InsertHistoryTransactionRecord(RosterTransactionModel rtmodel);
        List<RosterHistoryModel> getPlayerHistory(int player_id);
    }
}