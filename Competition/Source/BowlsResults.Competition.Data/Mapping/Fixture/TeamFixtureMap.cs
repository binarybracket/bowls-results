using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Fixture
{
	public class TeamFixtureMap : IdentityEntitySubclassMap<TeamFixture, short>
	{
		public TeamFixtureMap()
		{
//			this.KeyColumn("TeamFixtureID");

			this.References(x => x.Entrant1).Column("Entrant1ID").Cascade.None();
			this.References(x => x.Entrant2).Column("Entrant2ID").Cascade.None();
			
			this.DiscriminatorValue((int)CompetitionScopes.Team);
		}
	}
}