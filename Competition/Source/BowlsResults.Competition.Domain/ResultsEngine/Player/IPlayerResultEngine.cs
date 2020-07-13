using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public interface IPlayerResultEngine
	{
		void SetContext(IPlayerResultEngineContext context);
		ResultsEngineResponse SaveStandardResult(SaveStandardResultRequest request);
		ResultsEngineResponse UpdatePendingFixture(IUpdatePendingFixtureRequest request);
		Task Save();
	}
}