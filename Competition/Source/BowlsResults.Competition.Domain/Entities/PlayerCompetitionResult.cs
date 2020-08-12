using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class PlayerCompetitionResult : CompetitionResult
	{
		public PlayerCompetitionResult()
		{
			this.CompetitionScopeID = CompetitionScopes.Player;
		}
		public virtual PlayerFixture Fixture { get; set; }
	}
}