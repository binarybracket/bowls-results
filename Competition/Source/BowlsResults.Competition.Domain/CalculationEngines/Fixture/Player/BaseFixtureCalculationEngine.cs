using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player
{
	public abstract class BasePlayerFixtureCalculationEngine : IPlayerFixtureCalculationEngine
	{
		public void Calculate(PlayerFixture fixture)
		{
			this.InnerCalculate(fixture);
		}

		protected abstract void InnerCalculate(PlayerFixture fixture);
	}
}