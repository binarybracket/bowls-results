using System.Collections.Generic;
using System.Threading.Tasks;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BowlsResults.WebApi.Competition
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/{v:apiVersion}/club/")]
	public class ClubController
	{
		public ClubController(IClubRepository clubRepository)
		{
			this._clubRepository = clubRepository;
		}
		
		private readonly IClubRepository _clubRepository;

		[HttpGet]
		public async Task<ApiResponse> Get(int associationID)
		{
			IList<Club> clubs = await this._clubRepository.GetAllActiveByAssociation(associationID);
			var listDo = clubs.AssembleDtoList();
			return ApiResponse.CreateSuccess(listDo);
		}
	}
}