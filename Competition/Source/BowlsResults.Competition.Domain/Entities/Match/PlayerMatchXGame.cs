using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class PlayerMatchXGame : MatchXGame
	{
		public PlayerMatchXGame()
		{
			this.ScopeID = CompetitionScopes.Player;
		}

		public virtual PlayerMatch Match { get; set; }
	}
}