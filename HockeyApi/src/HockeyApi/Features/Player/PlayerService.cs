using HockeyApi.Common;
using HockeyApi.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IDb _db;

        public PlayerService(IDb db)
        {
            _db = db;
        }

        public IEnumerable<PlayerModel> getAllPlayers()
        {
            var players = new HashSet<PlayerModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT    player_id,
                              first_name,last_name 
                    FROM        player";

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        players.Add(
                            new PlayerModel(
                                rd.GetInt32(0),
                                rd.GetString(1),
                                rd.GetString(2)));
                    }
                }
            }

            return players;
        }

        public PlayerModel getPlayerBYId(int pId)
        {
            var players = new List<PlayerModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT    player_id,
                              first_name,last_name 
                    FROM        player
                    where player_id =" + pId.ToString();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        players.Add(
                            new PlayerModel(
                                rd.GetInt32(0),
                                rd.GetString(1),
                                rd.GetString(2)));
                    }
                }
            }

            return players[0];
        }


        //public PlayerDetailDto getPlayerDetail(int pId)
        //{
        //    var players = new List<PlayerDetailDto>();




        //}




    }
}
