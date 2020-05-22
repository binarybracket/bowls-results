using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public class GameMap : AuditableEntityClassMap<Competition.Domain.Entities.Game.Game, int>
	{
		public GameMap()
		{
			Table("Game");
			LazyLoad();
			Map(x => x.GameVariationID).Column("GameVariationID");
			Map(x => x.HomeResultTypeID).Column("HomeResultTypeID");
			Map(x => x.AwayResultTypeID).Column("AwayResultTypeID");
			Map(x => x.AssociationID).Column("AssociationID").Not.Nullable();
			Map(x => x.SeasonID).Column("SeasonID").Not.Nullable();
			Map(x => x.GameFormatID).Column("GameFormatID").Not.Nullable();
			Map(x => x.GameCalculationEngineID).Column("GameCalculationEngineID").Not.Nullable();
			Map(x => x.Date).Column("Date");
			Map(x => x.VenueTypeID).Column("VenueTypeID");			
			Map(x => x.HomeHandicap).Column("HomeHandicap");
			Map(x => x.AwayHandicap).Column("AwayHandicap");
			Map(x => x.HomeScore).Column("HomeScore").Not.Nullable();
			Map(x => x.AwayScore).Column("AwayScore").Not.Nullable();
			Map(x => x.HomeWalkover).Column("HomeWalkover").Not.Nullable();
			Map(x => x.AwayWalkover).Column("AwayWalkover").Not.Nullable();
			Map(x => x.Completed).Column("Completed").Not.Nullable();
			Map(x => x.GameStatusID).Column("GameStatusID").Not.Nullable();
			
			References(x => x.Pitch).Column("PitchID").Not.Nullable();

			this.HasMany(x => x.Players)
				.Not.KeyUpdate()
				.Cascade.SaveUpdate().Access.CamelCaseField(Prefix.Underscore);
		}
	}
}