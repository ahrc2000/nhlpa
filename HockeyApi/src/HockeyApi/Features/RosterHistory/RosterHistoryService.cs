﻿using HockeyApi.Common;
using HockeyApi.Features.RosterTransaction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.RosterHistory
{
    public class RosterHistoryService : IRosterHistoryService
    {
        private readonly IDb _db;

        public RosterHistoryService(IDb db)
        {
            _db = db;
        }

        public List<RosterHistoryModel> getPlayerHistory(int player_id)
        {

            var players = new List<RosterHistoryModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT   *
                    FROM        roster_trans_history
                    where player_id =" + player_id.ToString();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        players.Add(
                            new RosterHistoryModel(
                                rd.GetInt32(0),
                                rd.GetInt32(1),
                                rd.GetInt32(2),
                                rd.GetInt32(3),
                                rd.GetString(4),
                                rd.GetDateTime(5)));
                    }
                }
            }

            return players;
        }

        public void InsertHistoryTransactionRecord(RosterTransactionModel rtmodel)
        {

            int result = -100;
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    Insert into roster_trans_history (roster_trans_id,roster_trans_type_id,player_id,team_code,effective_date) 
                    values(@rostertransid,@rosterTranscationTypeId ,@player_id , @team_code ,@effective_date)";

                SqlParameter rostertransid = new SqlParameter(); rostertransid.ParameterName = "@rostertransid"; rostertransid.Value = rtmodel.rosterTranscationId;
                SqlParameter rosterTranscationTypeId = new SqlParameter(); rosterTranscationTypeId.ParameterName = "@rosterTranscationTypeId"; rosterTranscationTypeId.Value = rtmodel.rosterTransactionTypeId;
                SqlParameter player_id = new SqlParameter(); player_id.ParameterName = "@player_id"; player_id.Value = rtmodel.player_id;
                SqlParameter team_code = new SqlParameter(); team_code.ParameterName = "@team_code"; team_code.Value = rtmodel.team_code;
                SqlParameter effective_date = new SqlParameter(); effective_date.ParameterName = "@effective_date"; effective_date.Value = rtmodel.effective_date;

                cmd.Parameters.Add(rostertransid);
                cmd.Parameters.Add(rosterTranscationTypeId);
                cmd.Parameters.Add(player_id);
                cmd.Parameters.Add(team_code);
                cmd.Parameters.Add(effective_date);

                result = cmd.ExecuteNonQuery();
            }
        }


    }
}
