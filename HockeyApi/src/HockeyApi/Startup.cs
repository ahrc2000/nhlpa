using HockeyApi.Common;
using HockeyApi.Common.Services;
using HockeyApi.Features;
using HockeyApi.Features.Player;
using HockeyApi.Features.RosterHistory;
using HockeyApi.Features.RosterTransaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HockeyApi {
	public class Startup {
		readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
			_configuration = configuration;
			_environment = environment;
		}

		public void ConfigureServices(IServiceCollection services) {
			services
			  .AddRouting()
			  .AddControllers(o => {
				  o.EnableEndpointRouting = true;
			  });

			string connStr = _configuration.GetConnectionString("Default");
			services.AddScoped<IDb>(_ => new Db(_configuration.GetConnectionString("Default")));
			services.AddScoped<ITeamService, TeamService>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IRosterTransactionService, RosterTransactionService>();
			services.AddScoped<IHelperService, HelperService>();
			services.AddScoped<IRosterHistoryService,RosterHistoryService>();
		}

		public void Configure(IApplicationBuilder app) {
			if (_environment.IsDevelopment()) app.UseDeveloperExceptionPage();

			app.UseRouting()
			   .UseEndpoints(r => r.MapControllerRoute("default", "{controller=Team}/{action=Index}/{id?}"))
			   .Run(_notFoundHander);

		}

		private readonly RequestDelegate _notFoundHander = async ctx => {
			ctx.Response.StatusCode = 404;
			await ctx.Response.WriteAsync("Page not found.");
		};
	}
}
