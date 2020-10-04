using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.Player
{
    public class PlayerModel
    {
        public PlayerModel(int id, string fname, string lname)
        {
            player_id = id;
            first_name = fname;
            last_name = lname;
        }

        public int player_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }
}
