using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public static class FixtureCalculationEngineFactory
	{
		public static IPlayerFixtureCalculationEngine CreatePlayerFixtureCalculationEngine(FixtureCalculationEngines engine)
		{
			switch (engine)
			{
				case FixtureCalculationEngines.SingleMatch:
					return new SingleMatchFixtureCalculationEngine();
				default:
					throw new ArgumentOutOfRangeException("engine");
			}
		}
	}
}