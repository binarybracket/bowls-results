using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Models
{
	public sealed class CompetitionRoundLookupModel
	{
		public CompetitionRoundTypes RoundType { get; set; }
		public int? GameNumber { get; set; }
	}
}