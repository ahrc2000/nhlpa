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

        [HttpGet]
        public async Task<IEnumerable<PlayerModel>> Get()
        {
            return  _pservice.getAllPlayers();
        }

        [HttpGet("{id}")]
        public async Task<PlayerModel> Get(int id)
        {
            return _pservice.getPlayerBYId(id);
        }

        [HttpPost]
        public async Task<int> CreateNewPlayer(PlayerDetailDto dto)
        {

            return _hservice.AddPlayerToTeam(dto.fname, dto.lname, dto.teamcode, dto.dt);


        }

        [HttpPut("{playerId}")]
        public async Task<string> Injured(int playerId, DateTime effectiveDate)
        {
            return _hservice.ChangePlayerStatus(playerId, (int)TransactionType.Injured, effectiveDate);
        }

        [HttpPut("{playerId}")]
        public async Task<string> healthy(int playerId, DateTime effectiveDate)
        {
            return _hservice.ChangePlayerStatus(playerId, (int)TransactionType.Healthy, effectiveDate);
        }

        [HttpPut("{playerId}")]
        [Route("{playerId}/trade")]
        public async Task<string> trade(PlayerUpdateDTO dto)
        {
            return _hservice.TradePlayer(dto.playerId, dto.teamCode,dto.effectiveDate);
        }



    }
}
