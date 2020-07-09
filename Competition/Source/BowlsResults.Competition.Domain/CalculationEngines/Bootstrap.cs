using Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines.Game;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CalculationEngines
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<IGameCalculationEngineFactory, GameCalculationEngineFactory>();
		}
	}
}