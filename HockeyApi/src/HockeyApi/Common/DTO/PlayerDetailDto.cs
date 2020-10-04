using HockeyApi.Features.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class PlayerDetailDto
    {

        public string fname { get; set; }
        public string lname { get; set; }
        public string teamcode { get; set; }
        public DateTime dt { get; set; }
        
    }
}
