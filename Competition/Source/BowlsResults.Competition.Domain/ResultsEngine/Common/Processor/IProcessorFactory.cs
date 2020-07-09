using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor
{
	public interface IProcessorFactory<TContext, TRequest, TResponse>
		where TContext : IResultsEngineContext
		where TRequest : IResultsEngineRequest
		where TResponse : IResultsEngineResponse
	{
		IList<IProcessor<TContext, TRequest, TResponse>> Create(TContext context, TRequest request);
	}
}