using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi.CompetitionResult.Assembler;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.CompetitionResult
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/competition-result/")]
	public class CompetitionResultController
	{
		public ICompetitionResultRepository CompetitionResultRepository { get; }

		public CompetitionResultController(IUnitOfWork unitOfWork, ISessionProvider sessionProvider, ICompetitionResultRepository competitionResultRepository)
		{
			this._unitOfWork = unitOfWork;
			this._sessionProvider = sessionProvider;
			this._competitionResultRepository = competitionResultRepository;
		}

		private readonly IUnitOfWork _unitOfWork;
		private readonly ISessionProvider _sessionProvider;
		private readonly ICompetitionResultRepository _competitionResultRepository;

		[ResponseCache(Duration = 60)]
		[HttpGet]
		public async Task<ApiResponse> Get(int? clubID)
		{
			var season = DateTime.UtcNow.Year;
			season = 2020;
			var competitions = await this._competitionResultRepository.GetPlayerCompetitionResultsBySeason(season);

			if (clubID.HasValue)
			{
				competitions = competitions.Where(x => x.Competition.VenueClub.ID == clubID).ToList();
			}
			
			List<PlayerCompetitionResultDto> dtoList = competitions.AssembleDtoList();

			return ApiResponse.CreateSuccess(dtoList);
		}
		
		[ResponseCache(Duration = 60)]
		[Route("club/{id}")]
		[HttpGet]
		public async Task<ApiResponse> Get(int id)
		{
			var competitions = await this._competitionResultRepository.GetPlayerCompetitionResults(id, 15);

			competitions = competitions.Where(x => x.Competition.VenueClub.ID == id).ToList();
			
			var dtoList = competitions.AssembleDtoList();

			return ApiResponse.CreateSuccess(dtoList);
		}
	}
}