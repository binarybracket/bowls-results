using Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.Registration.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			services.AddTransient<CreateSinglesRegistrationCommandHandler, CreateSinglesRegistrationCommandHandler>();
			services.AddTransient<CreateDoublesRegistrationCommandHandler, CreateDoublesRegistrationCommandHandler>();
			services.AddTransient<CreateTriplesRegistrationCommandHandler, CreateTriplesRegistrationCommandHandler>();

			services.AddTransient<CreateSinglesRegistrationCommandValidator, CreateSinglesRegistrationCommandValidator>();
			services.AddTransient<CreateDoublesRegistrationCommandValidator, CreateDoublesRegistrationCommandValidator>();
			services.AddTransient<CreateTriplesRegistrationCommandValidator, CreateTriplesRegistrationCommandValidator>();
		}
	}
}