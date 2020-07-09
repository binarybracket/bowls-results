using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Fixture.Player;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Match.Player;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<IGameCalculationEngineFactory, GameCalculationEngineFactory>();
			services.AddTransient<IPlayerMatchCalculationEngineFactory, PlayerMatchCalculationEngineFactory>();
			services.AddTransient<IFixtureCalculationEngineFactory, FixtureCalculationEngineFactory>();
		}
	}
}