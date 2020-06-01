using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class KnockoutCalculationEngine : IdentityEntity<byte>
	{
		public KnockoutCalculationEngine()
		{
		}

		public virtual MatchFormat MatchFormat { get; set; }
		public virtual FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual MeritCalculationEngines MatchCalculationEngineID { get; set; }
	}
}