using System.Collections.Generic;
using System.Threading.Tasks;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Registration.Players;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition")]
	public class CompetitionController
	{
		public CompetitionController(ICompetitionRepository competitionRepository)
		{
			this._competitionRepository = competitionRepository;
		}
		
		private ICompetitionRepository _competitionRepository;
		
		[HttpGet]
		public async Task<List<CompetitionDto>> Get()
		{
			List<Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition> competitions = await this._competitionRepository.GetPendingPlayerCompetitions();
			List<CompetitionDto> dto = competitions.AssembleDtoList();
			return dto;
		}
	}
}