using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Heartbeat
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/heartbeat/")]
	public class HeartbeatController
	{
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ICompetitionRegistrationAttemptRepository _competitionRegistrationAttemptRepository;

		public HeartbeatController(ICompetitionRepository competitionRepository, ICompetitionRegistrationAttemptRepository competitionRegistrationAttemptRepository)
		{
			this._competitionRepository = competitionRepository;
			this._competitionRegistrationAttemptRepository = competitionRegistrationAttemptRepository;
		}
		
		[HttpGet]
		public async Task<ApiResponse> Get()
		{
			var attempt = this._competitionRegistrationAttemptRepository.GetTop().GetAwaiter().GetResult();
			var competition = this._competitionRepository.GetTop().GetAwaiter().GetResult();
			
			return ApiResponse.CreateSuccess(new object());
		}
	}
}