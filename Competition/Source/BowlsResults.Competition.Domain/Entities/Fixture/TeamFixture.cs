using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture
{
	public class TeamFixture : Fixture
	{
		public TeamFixture()
		{
			this.CompetitionScopeID = CompetitionScopes.Player;
		}
		
		public virtual Team Entrant1 { get; set; }
		public virtual Team Entrant2 { get; set; }
	}
}