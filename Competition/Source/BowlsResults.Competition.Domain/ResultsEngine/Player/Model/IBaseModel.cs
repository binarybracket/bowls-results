using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public interface IBaseModel<TData, TContext>
		where TContext : IResultsEngineContext
	{
		void Initialise(TData data, TContext context);

		TData Data { get; }
		
		TContext Context { get; }
	}
}