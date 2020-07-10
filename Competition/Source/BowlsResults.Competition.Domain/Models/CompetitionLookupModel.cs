using System.Runtime.InteropServices.ComTypes;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Models
{
	public sealed class CompetitionLookupModel
	{
		public enum CompetitionStageLookupModes
		{
			Auto,
			ByID,
			BySequence
		}

		public int CompetitionID { get; set; }
		public CompetitionStageLookupModes CompetitionStageLookupMode { get; set; }
		public int CompetitionStageValue { get; set; }
	}
}