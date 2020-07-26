using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	/// <summary>
	/// Create Competition Command
	/// </summary>
	public sealed class CreateCompetitionCommand : ICommand<DefaultIdentityCommandResponse>
	{
		public int? CompetitionHeaderID { get; set; }
		public int AssociationID { get; set; }
		public short SeasonID { get; set; }
		public string Name { get; set; }
		public CompetitionOrganisers Organiser { get; set; }
		public CompetitionFormats Format { get; set; }
		public CompetitionScopes Scope { get; set; }
		public AgeGroups AgeGroup { get; set; }
		public Genders Gender { get; set; }
		public int? OrganiserClubID { get; set; }
		public int? VenueClubID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public MeritCalculationEngines? PlayerMeritCalculationEngine { get; set; }
		public byte? GameVariationID { get; set; }
		public string Sponsor { get; set; }
	}
}