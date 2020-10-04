using HockeyApi.Common.DTO;
using HockeyApi.Features.RosterTransaction;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyApi.Features {
	[Route("[controller]")]
	[ApiController]
	public class TeamController : Controller {
		private readonly ITeamService _service;
		private readonly IRosterTransactionService _rtService;

		public TeamController(ITeamService service, IRosterTransactionService rtService) {
			_service = service;
			_rtService = rtService;
		}

		[HttpGet]
		public async Task<IEnumerable<TeamModel>> Get()
		{
			return _service.List();
		}

        [HttpGet("{team_code}")]
        public async Task<IEnumerable<RosterTransactionModel>> Get(string team_code)
        {
			return _rtService.getRtForTeamByTeamCode(team_code);

        }

        public IActionResult Index() => 
			Json(_service.List());
	}
}
