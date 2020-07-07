using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.Competition.Result
{
	public sealed class GetClubResult
	{
		public ClubDto Club { get; set; }
		public IList<ContactDto> Contacts { get; set; }
		public IList<CompetitionDto> PendingPlayerCompetitions { get; set; }
	}
}