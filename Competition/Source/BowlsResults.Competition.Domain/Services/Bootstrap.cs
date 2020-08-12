using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Services.Game;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Services
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<IGameServiceFactory, GameServiceFactory>();
			services.AddTransient<IGameService<SinglesGame>, SingleGameService>();
			services.AddTransient<IGameService<DoublesGame>, DoublesGameService>();
			services.AddTransient<IGameService<ThreesomesGame>, ThreesomesGameService>();
		}
	}
}