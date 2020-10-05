using HockeyApi.Common.DTO;
using HockeyApi.Common.Enums;
using HockeyApi.Common.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyApi.Features.Player
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController: Controller
    {
        private readonly IPlayerService _pservice;
        private readonly IHelperService _hservice;
        public PlayerController(IPlayerService pservice, IHelperService hservice)
        {
            _pservice = pservice;
            _hservice = hservice;
        }

        //[HttpGet]
        //public async Task<IEnumerable<PlayerModel>> Get()
        //{
        //    return  _pservice.getAllPlayers();
        //}

        [HttpGet("{id}")]
        public async Task<PlayerTransactionDto> Get(int id)
        {
            return _hservice.getPlayerDataById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerModel>> Get(string fname, string lname)
        {
            return _pservice.SearchPlayers(fname, lname);
        }

        [HttpPost]
        public async Task<string> CreateNewPlayer(PlayerDetailDto dto)
        {

            return _hservice.AddPlayerToTeam(dto.fname, dto.lname, dto.teamcode, dto.dt);


        }

        [HttpPut("{playerId}")]
        [Route("{playerId}/Injured")]
        public async Task<string> Injured(PlayerUpdateDTO dto)
        {
            return _hservice.ChangePlayerStatus(dto.playerId, (int)TransactionType.Injured,dto.effectiveDate);
        }

        [HttpPut("{playerId}")]
        [Route("{playerId}/healthy")]
        public async Task<string> healthy(PlayerUpdateDTO dto)
        {
            return _hservice.ChangePlayerStatus(dto.playerId, (int)TransactionType.Healthy,dto.effectiveDate);
        }

        [HttpPut("{playerId}")]
        [Route("{playerId}/trade")]
        public async Task<string> trade(PlayerUpdateDTO dto)
        {
            return _hservice.TradePlayer(dto.playerId, dto.teamCode,dto.effectiveDate);
        }



    }
}
