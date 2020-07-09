using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor
{
	public interface IProcessorExecutor
	{
		ResultsEngineStatuses Execute<TContext, TRequest, TResponse>(TContext context, TRequest request, TResponse response,
			IList<IProcessor<TContext, TRequest, TResponse>> processors)
			where TContext : IResultsEngineContext
			where TRequest : IResultsEngineRequest
			where TResponse : IResultsEngineResponse;
	}
}