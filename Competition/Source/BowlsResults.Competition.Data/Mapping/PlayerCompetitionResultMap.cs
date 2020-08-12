using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class PlayerCompetitionResultMap : IdentityEntitySubclassMap<PlayerCompetitionResult, int>
	{
		public PlayerCompetitionResultMap()
		{
			this.LazyLoad();

			this.References(x => x.Fixture).Column("FixtureID").Not.Nullable().Cascade.None();

			this.DiscriminatorValue((int) CompetitionScopes.Player);
		}
	}
}