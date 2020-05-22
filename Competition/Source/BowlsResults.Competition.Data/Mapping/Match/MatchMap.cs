using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class MatchMap : IdentityEntityMap<Domain.Entities.Match.Match, short>
	{
		public MatchMap()
		{
			this.Map(x => x.MatchFormatID);
			this.Map(x => x.MatchCalculationEngineID);
			this.Map(x => x.MatchStatusID);

			this.Map(x => x.Sequence);

			this.Map(x => x.DataString1);
			this.Map(x => x.DataString2);
		}
	}
}