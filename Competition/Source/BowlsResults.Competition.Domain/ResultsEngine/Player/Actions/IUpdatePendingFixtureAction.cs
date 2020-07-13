using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions
{
	public interface IUpdatePendingFixtureAction
	{
		ResultsEngineResponse Execute(IPlayerResultEngineContext context, IUpdatePendingFixtureRequest request);
	}
}