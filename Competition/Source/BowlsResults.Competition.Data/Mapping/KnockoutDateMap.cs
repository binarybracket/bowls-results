using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class KnockoutDateMap : IdentityEntitySubclassMap<KnockoutDate, int>
	{
		public KnockoutDateMap()
		{
			this.Map(x => x.CompetitionID);
			this.Map(x => x.CompetitionRoundID);
			this.Map(x => x.KnockoutID);
			this.Map(x => x.Date);
		}
	}
}