using HockeyApi.Features.RosterTransaction;

namespace HockeyApi.Features.RosterHistory
{
    public interface IRosterHistoryService
    {
        void InsertHistoryTransactionRecord(RosterTransactionModel rtmodel);
    }
}