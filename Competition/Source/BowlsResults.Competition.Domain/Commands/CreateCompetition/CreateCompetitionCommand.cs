using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Data;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	/// <summary>
	/// Create Competition Command
	/// </summary>
	public sealed class CreateCompetitionCommand : ICommand<DefaultCommandResponse>
	{
		public int CompetitionHeaderID { get; set; }
		public int AssociationID { get; set; }
		public short SeasonID { get; set; }
		public string Name { get; set; }
		public CompetitionOrganisers Organiser { get; set; }
		public CompetitionScopes Scope { get; set; }
		public AgeGroups AgeGroup { get; set; }
		public Genders Gender { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public MeritCalculationEngines? PlayerMeritCalculationEngine { get; set; }
		public List<CompetitionStageTemplate> Stages { get; set; }
	}
}