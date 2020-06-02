using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class PlayerMatchXGameMap : IdentityEntitySubclassMap<PlayerMatchXGame, int>
	{
		public PlayerMatchXGameMap()
		{
			this.DiscriminatorValue((byte)CompetitionScopes.Player);
			
			this.References(x => x.Match).Cascade.None();
		}
	}
}