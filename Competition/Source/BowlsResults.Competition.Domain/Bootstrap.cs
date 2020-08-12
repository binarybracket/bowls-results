using Com.BinaryBracket.BowlsResults.Competition.Domain.Email.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			CommandHandlers.Bootstrap.Wire(services);
			
			services.AddTransient<IRegistrationEmailManager, RegistrationEmailManager>();
		}
	}
}