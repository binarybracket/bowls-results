using System;
using System.Collections.Generic;
using Antlr.Runtime.Misc;
using BowlsResults.WebApi.Common.Dto;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class CompetitionDto
	{
		public CompetitionDto()
		{
			this.Dates = new List<CompetitionDateDto>();
			this.Stages = new ListStack<CompetitionStageDto>();
		}
		
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
		public List<CompetitionStageDto> Stages { get; set; }
	}
}