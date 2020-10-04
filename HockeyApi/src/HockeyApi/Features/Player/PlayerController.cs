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
        IPlayerService _pservice;

        public PlayerController(IPlayerService pservice)
        {
            _pservice = pservice;
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


    }
}
