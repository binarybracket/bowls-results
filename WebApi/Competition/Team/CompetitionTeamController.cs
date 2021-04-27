using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi.Competition.Assembler;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Views;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition.Team
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition/team")]
	public class CompetitionTeamController
	{
		public CompetitionTeamController(ICompetitionRepository competitionRepository, IUnitOfWork unitOfWork, ISessionProvider sessionProvider, ICompetitionRegistrationRepository competitionRegistrationRepository, IRegistrationEmailManager registrationEmailManager, ICompetitionResultRepository competitionResultRepository, IPlayerFixtureRepository playerFixtureRepository)
		{
			this._competitionRepository = competitionRepository;
			this._unitOfWork = unitOfWork;
			this._sessionProvider = sessionProvider;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
			this._registrationEmailManager = registrationEmailManager;
			this._competitionResultRepository = competitionResultRepository;
			this._playerFixtureRepository = playerFixtureRepository;
		}

		private ICompetitionRepository _competitionRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISessionProvider _sessionProvider;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;
		private readonly IRegistrationEmailManager _registrationEmailManager;
		private readonly ICompetitionResultRepository _competitionResultRepository;
		private readonly IPlayerFixtureRepository _playerFixtureRepository;

		[ResponseCache(Duration = 60)]
		[Route("{headerid}/team")]
		[HttpGet]
		public async Task<ApiResponse> Get(int headerID, int? seasonID = null)
		{
			if (!seasonID.HasValue)
			{
				seasonID = DateTime.UtcNow.Year;
			}

			List<CompetitionTeam> competitionTeams = await this._competitionRepository.GetCompetitionTeams(headerID, seasonID.Value);
			var teamDtos = competitionTeams.Select(x => x.Team).AssembleDtoList();
			return ApiResponse.CreateSuccess(teamDtos);
		}
	}
}