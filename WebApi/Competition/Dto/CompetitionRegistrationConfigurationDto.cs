using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class CompetitionRegistrationConfigurationDto
	{
		public DateTime OpenDate { get; set; }
		public DateTime CloseDate { get; set; }
		public decimal? Amount { get; set; }
		public ContactDto OrganiserContact { get; set; }
		public GameFormats? EntryGameFormatID { get; set; }
	}
}