using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyApi.Features {
	[Route("api/[controller]")]
	[ApiController]
	public class TeamController : Controller {
		private readonly ITeamService _service;

		public TeamController(ITeamService service) {
			_service = service;
		}

		[HttpGet]
		public async Task<IEnumerable<TeamModel>> Get()
        {
			return _service.List();
        }


		public IActionResult Index() => 
			Json(_service.List());
	}
}
