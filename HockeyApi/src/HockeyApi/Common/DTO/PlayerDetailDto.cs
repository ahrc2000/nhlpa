using HockeyApi.Features.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class PlayerDetailDto
    {

        public PlayerDetailDto(PlayerModel pmodel, int rtid, DateTime createTime)
        {
            player = pmodel;
            rosterTransactionId = rtid;
            createTime = transactionDate;
        }

        public PlayerModel player { get; set; }
        public int rosterTransactionId { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
