using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BowlsResults.WebApi.Jobs
{
	[DisallowConcurrentExecution]
	public class HelloWorldJob : IJob
	{
		private readonly IServiceProvider _provider;
		private readonly ILogger<HelloWorldJob> _logger;

		public HelloWorldJob(ILoggerFactory loggerFactory, IServiceProvider provider)
		{
			this._provider = provider;
			this._logger = loggerFactory.CreateLogger<HelloWorldJob>();
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (var scope = _provider.CreateScope())
			{
				// Resolve the Scoped service
				var competitionRepository = scope.ServiceProvider.GetService<ICompetitionRepository>();

				var competition = competitionRepository.GetWithRegistrationConfiguration(1).GetAwaiter().GetResult();

				this._logger.LogWarning($"Hello world! {competition.Name}");
				return Task.CompletedTask;
			}
		}
	}
}