using System;
using System.Collections.Generic;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class CompetitionDto
	{
		public int ID { get; set; }
		public DateTime StartDate { get; set; }
		public string Name { get; set; }
		public string GameVariation { get; set; }
		public GameFormats GameFormatID { get; set; }
		public ClubDto VenueClub { get; set; }
		public CompetitionRegistrationConfigurationDto RegistrationConfiguration { get; set; }
		public CompetitionRegistrationStatuses RegistrationStatus { get; set; }
		public string Sponsor { get; set; }
		public List<CompetitionDateDto> Dates { get; set; }
	}
}