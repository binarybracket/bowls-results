using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.Competition.Dto;
using BowlsResults.WebApi.CompetitionResult.Assembler;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition.Player
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition/player")]
	public class CompetitionPlayerController
	{
		public CompetitionPlayerController(ICompetitionRepository competitionRepository, IUnitOfWork unitOfWork, ISessionProvider sessionProvider, ICompetitionRegistrationRepository competitionRegistrationRepository, IRegistrationEmailManager registrationEmailManager, ICompetitionResultRepository competitionResultRepository, IPlayerFixtureRepository playerFixtureRepository)
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
		[HttpGet]
		public async Task<ApiResponse> Get()
		{
			List<Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition> competitions = await this._competitionRepository.GetPendingPlayerCompetitions();
			List<CompetitionDto> dto = competitions.AssembleDtoList();
			return ApiResponse.CreateSuccess(dto);
		}
		
		[ResponseCache(Duration = 60)]
		[Route("{id}")]
		[HttpGet]
		public async Task<ApiResponse> Get(int id)
		{
			Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition competition = await this._competitionRepository.GetWithRegistrationConfiguration(id);
			CompetitionDto dto = competition.AssembleDto();
			
			if (competition != null && competition.CompetitionScopeID == CompetitionScopes.Player)
			{
				List<PlayerFixture> fixtures = this._playerFixtureRepository.GetAllFullByCompetition(id).GetAwaiter().GetResult();
				foreach (var competitionRound in fixtures.GroupBy(x=>x.CompetitionRound))
				{
					PlayerCompetitionRoundDto competitionRoundDto = competitionRound.Key.AssemblePlayerDto();
					competitionRoundDto.Results = competitionRound.AssembleDtoList();
				}
			}
			
			return ApiResponse.CreateSuccess(dto);
		}
		
		[ResponseCache(Duration = 60)]
		[Route("{id}/fixture")]
		[HttpGet]
		public async Task<ApiResponse> GetAllPlayerCompetitionFixture(int id)
		{
			Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition competition = await this._competitionRepository.GetWithRegistrationConfiguration(id);
			List<CompetitionRoundDto> list = new List<CompetitionRoundDto>();

			if (competition != null && competition.CompetitionScopeID == CompetitionScopes.Player)
			{
				List<PlayerFixture> fixtures = this._playerFixtureRepository.GetAllFullByCompetition(id).GetAwaiter().GetResult();
				return ApiResponse.CreateSuccess(fixtures.AssembleDtoList());
				foreach (var competitionRound in fixtures.GroupBy(x => x.CompetitionRound))
				{
					PlayerCompetitionRoundDto competitionRoundDto = competitionRound.Key.AssemblePlayerDto();
					competitionRoundDto.Results = competitionRound.AssembleDtoList();

					list.Add(competitionRoundDto);
				}
			}

			return ApiResponse.CreateSuccess(list);
		}
		
		[Route("{id}/registration/summary")]
		[HttpPost]
		public async Task<ApiResponse> RegistrationSummary(int id)
		{
			var competition = await this._competitionRepository.GetWithRegistrationConfiguration(id);
			if (competition.RegistrationConfiguration != null)
			{
				List<CompetitionRegistration> registrations = await this._competitionRegistrationRepository.GetAll(competition.ID);
				await this._registrationEmailManager.SendSummaryEmail(registrations, competition);
			}

			return ApiResponse.CreateSuccess(new object());
		}
	}
}