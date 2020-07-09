using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor
{
	public interface IProcessor<in TContext, in TRequest, in TResponse> : ISpecification<TContext, TRequest, TResponse>
		where TContext : IResultsEngineContext
		where TRequest : IResultsEngineRequest
		where TResponse : IResultsEngineResponse
	{
		Task<ResultsEngineStatuses> Process(TContext context, TRequest request, TResponse response);
	}
}