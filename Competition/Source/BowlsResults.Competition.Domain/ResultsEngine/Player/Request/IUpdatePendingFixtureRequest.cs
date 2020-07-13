using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request
{
	public interface IUpdatePendingFixtureRequest : IResultsEngineRequest
	{
		PlayerFixture CompletedFixture { get; }
	}
}