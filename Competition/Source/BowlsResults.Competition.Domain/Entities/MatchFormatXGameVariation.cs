using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class MatchFormatXGameVariation : IdentityEntity<short>
	{
		public MatchFormatXGameVariation()
		{
		}

		public virtual MatchFormat MatchFormat { get; set; }
		public virtual GameVariation GameVariation { get; set; }
		public virtual GameCalculationEngines GameCalculationEngineID { get; set; }
		public virtual byte Sequence { get; set; }
		public virtual short? Handicap { get; set; }
	}
}