using HockeyApi.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace HockeyApi.Features.RosterTransaction
{
    public class RosterTransactionService : IRosterTransactionService
    {
        private readonly IDb _db;

        public RosterTransactionService(IDb db)
        {
            _db = db;
        }

        public IEnumerable<RosterTransactionModel> GetAll()
        {

            var rtransactions = new HashSet<RosterTransactionModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT    *
                    FROM        roster_transaction";

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rtransactions.Add(
                                new RosterTransactionModel(
                                    rd.GetInt32(0),
                                    rd.GetInt32(1),
                                    rd.GetInt32(2),
                                    rd.GetString(3),
                                    rd.GetDateTime(4)));
                    }
                }
            }

            return rtransactions;

        }


        public RosterTransactionModel GetRtForPlayerById(int id)
        {
            var rtransactions = new List<RosterTransactionModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT    *
                    FROM        roster_transaction where player_id =" + id.ToString() + " order by effective_date desc";

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rtransactions.Add(
                            new RosterTransactionModel(
                                rd.GetInt32(0),
                                rd.GetInt32(1),
                                rd.GetInt32(2),
                                rd.GetString(3),
                                rd.GetDateTime(4)));
                    }
                }
            }

            return rtransactions[0];
        }

        public IEnumerable<RosterTransactionModel> getRtForTeamByTeamCode(string tcode)
        {
            var rtransactions = new List<RosterTransactionModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT roster_transaction_id,roster_transaction_type_id,player_id,team_code,effective_date 
                    FROM  roster_transaction 
                    where team_code = @tcode";

                SqlParameter teamcode = new SqlParameter();
                teamcode.ParameterName = "@tcode";
                teamcode.Value = tcode;

                cmd.Parameters.Add(teamcode);


                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rtransactions.Add(
                            new RosterTransactionModel(
                                rd.GetInt32(0),
                                rd.GetInt32(1),
                                rd.GetInt32(2),
                                rd.GetString(3),
                                rd.GetDateTime(4)));
                    }
                }
            }

            return rtransactions;
        }

        private int getLastTransactionId()
        {
            int result = 0;
            var rtransactions = new List<RosterTransactionModel>();

            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT    top 1 roster_transaction_id
                    FROM        roster_transaction order by roster_transaction_id desc";

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rtransactions.Add(
                            new RosterTransactionModel(
                                rd.GetInt32(0),
                                rd.GetInt32(1),
                                rd.GetInt32(2),
                                rd.GetString(3),
                                rd.GetDateTime(4)));
                    }
                }
            }

            return result;
        }



        public int AddPlayerTransaction(RosterTransactionModel rmodel)
        {
            int result = -100;
            int lastRosterId = getLastTransactionId();
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    Insert into roster_transaction (roster_transaction_id,roster_transaction_type_id,player_id,team_code,effective_date) 
                    values(" + lastRosterId.ToString() + "," + rmodel.rosterTranscationId + "," + rmodel.player_id + "," + rmodel.team_code + "," + rmodel.effective_date + ")";

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }



    }
}
