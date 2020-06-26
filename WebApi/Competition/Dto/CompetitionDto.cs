using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class CompetitionDto
	{
		public int ID { get; set; }
		public DateTime StartDate { get; set; }
		public string Name { get; set; }
		public string GameVariation { get; set; }
		public ClubDto VenueClub { get; set; }
		public CompetitionRegistrationConfigurationDto RegistrationConfiguration { get; set; }
		public CompetitionRegistrationStatuses RegistrationStatus { get; set; }
		
	}
}