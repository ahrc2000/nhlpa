using HockeyApi.Common;
using HockeyApi.Common.Enums;
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
                    SELECT    roster_transaction_id,roster_transaction_type_id,player_id,team_code,effective_date
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


        public List<RosterTransactionModel> getActivePlayersByTeamCode(string tcode)
        {
            {
                var rtransactions = new List<RosterTransactionModel>();

                using (var conn = _db.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT roster_transaction_id,roster_transaction_type_id,player_id,team_code,effective_date 
                    FROM  roster_transaction 
                    where team_code = @tcode and roster_transaction_type_id != @injured and roster_transaction_type_id != @healthy";

                    SqlParameter teamcode = new SqlParameter(); teamcode.ParameterName = "@tcode"; teamcode.Value = tcode;
                    SqlParameter injured = new SqlParameter(); injured.ParameterName = "@injured"; injured.Value = (int)TransactionType.Injured;
                    SqlParameter healthy = new SqlParameter(); healthy.ParameterName = "@healthy"; healthy.Value = (int)TransactionType.Healthy;
                    
                    cmd.Parameters.Add(teamcode);
                    cmd.Parameters.Add(injured);
                    cmd.Parameters.Add(healthy);

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
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    Insert into roster_transaction (roster_transaction_type_id,player_id,team_code,effective_date) 
                    values(@rosterTranscationTypeId ,@player_id , @team_code ,@effective_date)";

                SqlParameter rosterTranscationTypeId = new SqlParameter(); rosterTranscationTypeId.ParameterName = "@rosterTranscationTypeId"; rosterTranscationTypeId.Value = rmodel.rosterTransactionTypeId;
                SqlParameter player_id = new SqlParameter(); player_id.ParameterName = "@player_id"; player_id.Value = rmodel.player_id;
                SqlParameter team_code = new SqlParameter(); team_code.ParameterName = "@team_code"; team_code.Value = rmodel.team_code;
                SqlParameter effective_date = new SqlParameter(); effective_date.ParameterName = "@effective_date"; effective_date.Value = DateTime.Now;


                cmd.Parameters.Add(rosterTranscationTypeId);
                cmd.Parameters.Add(player_id);
                cmd.Parameters.Add(team_code);
                cmd.Parameters.Add(effective_date);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int UpdatePlayerTeam(int playerId, string teamcode, DateTime effdate)
        {
            int result = -100;
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    update roster_transaction 
                    set team_code= @tcode , effective_date = @effective_date 
                    where player_id = @player_id";


                SqlParameter tcode = new SqlParameter(); tcode.ParameterName = "@tcode"; tcode.Value = teamcode;
                SqlParameter player_id = new SqlParameter(); player_id.ParameterName = "@player_id"; player_id.Value = playerId;
                SqlParameter effective_date = new SqlParameter(); effective_date.ParameterName = "@effective_date"; effective_date.Value = effdate;

                cmd.Parameters.Add(tcode);
                cmd.Parameters.Add(player_id);
                cmd.Parameters.Add(effective_date);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
        public int UpdatePlayerTType(int playerId, int rtranstype , DateTime effdate)
        {
            int result = -100;
            using (var conn = _db.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    update roster_transaction 
                    set roster_transaction_type_id = @rttype , effective_date =@effective_date 
                    where player_id = @player_id";


                SqlParameter rttype = new SqlParameter(); rttype.ParameterName = "@rttype"; rttype.Value = rtranstype;
                SqlParameter player_id = new SqlParameter(); player_id.ParameterName = "@player_id"; player_id.Value = playerId;
                SqlParameter effective_date = new SqlParameter(); effective_date.ParameterName = "@effective_date"; effective_date.Value = effdate;

                cmd.Parameters.Add(rttype);
                cmd.Parameters.Add(player_id);
                cmd.Parameters.Add(effective_date);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }



    }
}
