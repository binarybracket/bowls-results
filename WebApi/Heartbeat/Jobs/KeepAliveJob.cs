using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.CommandHandlers.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CheckClosedCompetitionRegistrations;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BowlsResults.WebApi.Jobs
{
	[DisallowConcurrentExecution]
	public class KeepAliveJob : IJob
	{
		private readonly IServiceProvider _provider;
		private readonly ILogger<KeepAliveJob> _logger;

		public KeepAliveJob(ILoggerFactory loggerFactory, IServiceProvider provider)
		{
			this._provider = provider;
			this._logger = loggerFactory.CreateLogger<KeepAliveJob>();
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (var scope = _provider.CreateScope())
			{
				// Resolve the Scoped service
				var competitionRepository = scope.ServiceProvider.GetService<ICompetitionRepository>();
				var competitionRegistrationAttemptRepository = scope.ServiceProvider.GetService<ICompetitionRegistrationAttemptRepository>();

				var attempt = competitionRegistrationAttemptRepository.GetTop().GetAwaiter().GetResult();
				var competition = competitionRepository.GetTop().GetAwaiter().GetResult();

				this._logger.LogDebug($"Keep Alive Complete");

				//var commandHandler = scope.ServiceProvider.GetService<CheckClosedCompetitionRegistrationsCommandHandler>();
				//CheckClosedCompetitionRegistrationsCommand command = new CheckClosedCompetitionRegistrationsCommand();
				//var response = commandHandler.Handle(command).GetAwaiter().GetResult();
				
				return Task.CompletedTask;
			}
		}
	}
}