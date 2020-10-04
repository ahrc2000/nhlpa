using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class PlayerDetailDto
    {
        public int playerId { get; set; }
        public int rosterTransactionId { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
