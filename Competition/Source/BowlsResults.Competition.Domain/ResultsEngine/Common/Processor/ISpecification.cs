using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor
{
    public interface ISpecification<in TContext, in TRequest, in TResponse>
        where TContext : IResultsEngineContext
        where TRequest : IResultsEngineRequest
        where TResponse : IResultsEngineResponse
    {
        bool IsSatisfiedBy(TContext context, TRequest request, TResponse response);
    }
}