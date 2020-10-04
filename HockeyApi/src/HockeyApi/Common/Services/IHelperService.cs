using System;

namespace HockeyApi.Common.Services
{
    public interface IHelperService
    {
        int AddPlayerToTeam(string fname, string lname, string tcode, DateTime dt);
        string ChangePlayerStatus(int playerId, int rttypeId, DateTime efdate);
        int CheckPlayerStatus(int playerId);
        int SetPlayerStatusToHealthy(int playerId, DateTime efdate);
        int SetPlayerStatusToInjured(int playerId, DateTime efdate);
        string SignPlayer(int playerId, DateTime effdate);
        string TradePlayer(int playerId, string teamCode, DateTime effdt);
    }
}