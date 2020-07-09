using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public interface IFixtureCalculationEngineFactory
	{
		IPlayerFixtureCalculationEngine Create(FixtureCalculationEngines engine);
	}

	public sealed class FixtureCalculationEngineFactory : IFixtureCalculationEngineFactory
	{
		public IPlayerFixtureCalculationEngine Create(FixtureCalculationEngines engine)
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