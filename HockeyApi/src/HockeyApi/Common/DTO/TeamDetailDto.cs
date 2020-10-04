using HockeyApi.Features.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class TeamDetailDto
    {

        public TeamDetailDto( int tId , string tcode, List<PlayerDetailDto> playerDetails)
        {
            teamId = tId;
            teamCode = tcode;
            playerDetailList = playerDetails;

        }

        public int teamId { get; set; }
        public string teamCode { get; set; }
        public List<PlayerDetailDto> playerDetailList {get;set;}


    }
}
