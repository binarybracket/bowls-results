using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.SaveStandardResult
{
	public interface IMatchWalkoverProcessor : IProcessor<IPlayerResultEngineContext, ISaveStandardResultRequest, ResultsEngineResponse>
	{
	}
}