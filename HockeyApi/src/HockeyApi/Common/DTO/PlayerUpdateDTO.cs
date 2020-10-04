using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Common.DTO
{
    public class PlayerUpdateDTO
    {
        public int playerId { get; set; }
        public string teamCode { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
