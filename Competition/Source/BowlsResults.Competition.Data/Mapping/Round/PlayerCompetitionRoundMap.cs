using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Round
{
	public class PlayerCompetitionRoundMap : IdentityEntitySubclassMap<PlayerCompetitionRound, short>
	{
		public PlayerCompetitionRoundMap()
		{
			this.LazyLoad();

			this.DiscriminatorValue((int) CompetitionScopes.Player);

			this.HasMany(x => x.Fixtures).Cascade.All().Inverse().
				Access.CamelCaseField(Prefix.Underscore).AsSet().KeyColumn("CompetitionRoundID");
		}
	}
}