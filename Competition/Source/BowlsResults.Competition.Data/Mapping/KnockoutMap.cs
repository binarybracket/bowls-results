using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class KnockoutMap : IdentityEntitySubclassMap<Knockout, short>
	{
		public KnockoutMap()
		{
			this.Table("Knockout");
			this.KeyColumn("KnockoutID");
			this.LazyLoad();
			//TODO: check if cascade is required this.References(x => x.Competition).Cascade.None().Not.Update();
			this.References(x => x.Competition).Column("CompetitionID");
			this.References(x => x.Season);
			
			this.Map(x => x.KnockoutCalculationEngineID);
		//	this.HasMany(x => x.KnockoutDates).Cascade.None().KeyColumn("KnockoutID");
		}
	}
}