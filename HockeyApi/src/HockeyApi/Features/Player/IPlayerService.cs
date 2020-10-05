using System;
using System.Collections.Generic;

namespace HockeyApi.Features.Player
{
    public interface IPlayerService
    {
        IEnumerable<PlayerModel> getAllPlayers();
        PlayerModel getPlayerBYId(int pId);
        int AddNewPlayer(string fname, string lname);
        int GetLatestPlayer();
        IEnumerable<PlayerModel> SearchPlayers(string fname, string lname);
    }
}