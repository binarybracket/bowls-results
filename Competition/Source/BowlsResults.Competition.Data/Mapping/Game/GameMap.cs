using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Game
{
	public class GameMap : AuditableEntityClassMap<Competition.Domain.Entities.Game.Game, int>
	{
		public GameMap()
		{
			this.Table("Game");
			this.LazyLoad();
			this.DiscriminateSubClassesOnColumn("GameFormatID");
			
			this.Map(x => x.GameVariationID).Column("GameVariationID");
			this.Map(x => x.HomeResultTypeID).Column("HomeResultTypeID").CustomType<ResultType>();
			this.Map(x => x.AwayResultTypeID).Column("AwayResultTypeID").CustomType<ResultType>();
			this.Map(x => x.AssociationID).Column("AssociationID").Not.Nullable();
			this.Map(x => x.SeasonID).Column("SeasonID").Not.Nullable();
			this.Map(x => x.GameCalculationEngineID).Column("GameCalculationEngineID").Not.Nullable();
			this.Map(x => x.Date).Column("Date");
			this.Map(x => x.VenueTypeID).Column("VenueTypeID");			
			this.Map(x => x.HomeHandicap).Column("HomeHandicap");
			this.Map(x => x.AwayHandicap).Column("AwayHandicap");
			this.Map(x => x.HomeScore).Column("HomeScore").Not.Nullable();
			this.Map(x => x.AwayScore).Column("AwayScore").Not.Nullable();
			this.Map(x => x.HomeWalkover).Column("HomeWalkover").Not.Nullable();
			this.Map(x => x.AwayWalkover).Column("AwayWalkover").Not.Nullable();
			this.Map(x => x.Completed).Column("Completed").Not.Nullable();
			this.Map(x => x.GameStatusID).Column("GameStatusID").Not.Nullable();

			this.References(x => x.Pitch).Column("PitchID").Not.Nullable();

			this.HasMany(x => x.Players)
				.Not.KeyUpdate()
				.Cascade.SaveUpdate().Access.CamelCaseField(Prefix.Underscore);
		}
	}
}