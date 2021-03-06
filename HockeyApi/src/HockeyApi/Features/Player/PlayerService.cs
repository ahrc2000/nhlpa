﻿using HockeyApi.Common;
using HockeyApi.Common.DTO;
using HockeyApi.Features.RosterTransaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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


        public int AddNewPlayer(string fname, string lname)
        {
            int result = -100;
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    Insert into player (last_name,first_name) 
                    values(@last_name ,@first_name )";

                
                SqlParameter last_name = new SqlParameter(); last_name.ParameterName = "@last_name"; last_name.Value = lname;
                SqlParameter first_name = new SqlParameter(); first_name.ParameterName = "@first_name"; first_name.Value = fname;
                
                cmd.Parameters.Add(last_name);
                cmd.Parameters.Add(first_name);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int GetLatestPlayer()
        {
            var players = new List<PlayerModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT  top 1  player_id,
                              first_name,last_name 
                    FROM        player order by player_id desc";

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

            return players[0].player_id;
        }

        public IEnumerable<PlayerModel> SearchPlayers(string fname, string lname)
        {
            StringBuilder builder = new StringBuilder();
            var players = new HashSet<PlayerModel>();
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                string query = "Select top 10 * from Player where ";
                builder.Append(query);

                cmd.CommandText = string.Empty;

                if (fname != null)
                {
                    query = "first_name = @firstname";
                    builder.Append(query);
                    SqlParameter firstname = new SqlParameter(); firstname.ParameterName = "@firstname"; firstname.Value = fname;
                    cmd.Parameters.Add(firstname);
                    
                }
                if (lname != null)
                {
                    if (fname != null )
                    {
                        query = " and ";
                        builder.Append(query);

                    }
                    query = " last_name = @lastname";
                    builder.Append(query);
                    SqlParameter lastname = new SqlParameter(); lastname.ParameterName = "@lastname"; lastname.Value = lname;
                    cmd.Parameters.Add(lastname);
                    
                }

                query = " order by player_id";
                builder.Append(query);
                cmd.CommandText = builder.ToString();

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





    }
}
