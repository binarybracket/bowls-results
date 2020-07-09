using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public interface IPlayerResultEngineManager
	{
		Task<IPlayerResultEngine> GetEngine(IResultsEngineRequest request);
	}
}