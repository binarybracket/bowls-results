using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition/")]
	public class CompetitionController	
	{
		public CompetitionController(ICompetitionRepository competitionRepository, IUnitOfWork unitOfWork, ISessionProvider sessionProvider, ICompetitionRegistrationRepository competitionRegistrationRepository, IRegistrationEmailManager registrationEmailManager)
		{
			this._competitionRepository = competitionRepository;
			this._unitOfWork = unitOfWork;
			this._sessionProvider = sessionProvider;
			this._competitionRegistrationRepository = competitionRegistrationRepository;
			this._registrationEmailManager = registrationEmailManager;
		}
		
		private ICompetitionRepository _competitionRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISessionProvider _sessionProvider;
		private readonly ICompetitionRegistrationRepository _competitionRegistrationRepository;
		private readonly IRegistrationEmailManager _registrationEmailManager;

		[ResponseCache(Duration = 60)]
		[Route("{id}")]
		[HttpGet]
		public async Task<ApiResponse> Get(int id)
		{
			Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition competition = await this._competitionRepository.GetWithRegistrationConfiguration(id);
			CompetitionDto dto = competition.AssembleDto();
			return ApiResponse.CreateSuccess(dto);
		}

		[ResponseCache(Duration = 60)]
		[HttpGet]
		public async Task<ApiResponse> Get()
		{
			List<Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition> competitions = await this._competitionRepository.GetPendingPlayerCompetitions();
			List<CompetitionDto> dto = competitions.AssembleDtoList();
			return ApiResponse.CreateSuccess(dto);
		}

		[ResponseCache(Duration = 60)]
		[Route("player/results")]
		[HttpGet]
		public async Task<ApiResponse> GetResults()
		{
			List<Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Competition> competitions = await this._competitionRepository.GetPastPlayerCompetitions();

			foreach (var competition in competitions)
			{
				if (competition.Stages.Count > 0)
				{
					var lastStage = competition.Stages.OrderByDescending(x => x.Sequence).First();
					if (lastStage.CompetitionStageFormatID == CompetitionStageFormats.SingleKnockout)
					{
						Knockout knockout = this._sessionProvider.Session
							.Query<Knockout>()
							.SingleOrDefault(x => x.CompetitionStage.ID == lastStage.ID);
						if (knockout != null)
						{
							PlayerCompetitionRound round = this._sessionProvider.Session.Query<PlayerCompetitionRound>()
								.SingleOrDefault(x => x.CompetitionEvent.ID == knockout.ID && x.CompetitionRoundTypeID == CompetitionRoundTypes.Final);

							if (round != null)
							{
								var fixture = round.Fixtures.SingleOrDefault();
								if (fixture != null)
								{
									string x = "";
								}
							}
						}
					}
				}
			}
			
			List<CompetitionDto> dto = competitions.AssembleDtoList();
			return ApiResponse.CreateSuccess(dto);
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