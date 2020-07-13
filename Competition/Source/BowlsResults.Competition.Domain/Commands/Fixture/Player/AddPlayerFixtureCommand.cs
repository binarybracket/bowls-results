using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Fixture;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Fixture.Player
{
	public class AddPlayerFixtureCommand : ICommand<DefaultIdentityCommandResponse>

	{
		public CompetitionLookupModel Competition { get; set; }
		public short CompetitionEventID { get; set; }
		public CompetitionRoundLookupModel Round { get; set; }
		public PlayerFixtureEntrantConfigurationModel Entrant1 { get; set; }
		public PlayerFixtureEntrantConfigurationModel Entrant2 { get; set; }
		public DateTime Date { get; set; }
		public byte TotalLegs { get; set; }
		public string Reference { get; set; }
	}
}