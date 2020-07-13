using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Exceptions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor
{
	public class ProcessorExecutor : IProcessorExecutor
	{
		private readonly ILogger<ProcessorExecutor> _logger;

		public ProcessorExecutor(ILogger<ProcessorExecutor> logger)
		{
			this._logger = logger;
		}

		public ResultsEngineStatuses Execute<TContext, TRequest, TResponse>(TContext context, TRequest request, TResponse response,
			IList<IProcessor<TContext, TRequest, TResponse>> processors) where TContext : IResultsEngineContext
			where TRequest : IResultsEngineRequest
			where TResponse : IResultsEngineResponse
		{
			bool processorFound = false;

			var key = $"CompetitionID: '{request.CompetitionID}', FixtureID: '{request.FixtureID}', MatchID:'{request.MatchID}'";

			foreach (var processor in processors)
			{
				bool process = processor.IsSatisfiedBy(context, request, response).GetAwaiter().GetResult();
				if (process)
				{
					processorFound = true;
					this._logger.LogDebug($"{key}. Processor '{processor.GetType().FullName}' with Request Type '{request.GetType().FullName}' was satisfied.");

					ResultsEngineStatuses status;
					try
					{
						status = processor.Process(context, request, response).GetAwaiter().GetResult();
					}
					catch (Exception ex)
					{
						this._logger.LogCritical(ex, "Results Engine Exception");
						throw;
					}

					this._logger.LogDebug($"{key}. Processor '{processor.GetType().FullName}' with Request Type '{request.GetType().FullName}' returned status '{status}'.");

					if (status != ResultsEngineStatuses.Success)
					{
						return status;
					}
				}
				else
				{
					this._logger.LogDebug($"{key}. Processor '{processor.GetType().FullName}' with Request Type '{request.GetType().FullName}' was NOT satisfied.");
				}
			}

			if (processorFound)
			{
				return ResultsEngineStatuses.Success;
			}
			else
			{
				string msg = $"{key}. No processors satisfied.";
				this._logger.LogError(msg);
				throw new InvalidResultsEngineOperationException(msg);
			}
		}
	}
}