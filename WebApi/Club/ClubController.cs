using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi.Club.Result;
using BowlsResults.WebApi.Competition.Assembler;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Club
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/club/")]
	public class ClubController
	{
		public ClubController(IClubRepository clubRepository, ICompetitionRepository competitionRepository)
		{
			this._clubRepository = clubRepository;
			this._competitionRepository = competitionRepository;
		}
		
		private readonly IClubRepository _clubRepository;
		private readonly ICompetitionRepository _competitionRepository;

		[ResponseCache(Duration = 60)]
		[HttpGet]
		public async Task<ApiResponse> Get(int associationID, bool active = false)
		{
			IList<Com.BinaryBracket.BowlsResults.Common.Domain.Entities.Club> clubs = await this._clubRepository.GetAllActiveByAssociation(associationID);
			var listDo = ClubDtoAssembler.AssembleDtoList(clubs);
			return ApiResponse.CreateSuccess(listDo);
		}
		
		[ResponseCache(Duration = 60)]
		[Route("{id}")]
		[HttpGet]
		public async Task<ApiResponse> Get(int id)
		{
			Com.BinaryBracket.BowlsResults.Common.Domain.Entities.Club club = await this._clubRepository.GetWithContacts(id);
			var competitions = await this._competitionRepository.GetPendingPlayerCompetitions();
			
			var result = new GetClubResult();			
			result.Club= club.AssembleDto();
			result.Contacts = club.Contacts.Select(x => x.Contact).AssembleDtoList();
			result.PendingPlayerCompetitions = competitions.Where(x => x.VenueClub != null && x.VenueClub.Equals(club)).AssembleDtoList();
			return ApiResponse.CreateSuccess(result);
		}
	}
}