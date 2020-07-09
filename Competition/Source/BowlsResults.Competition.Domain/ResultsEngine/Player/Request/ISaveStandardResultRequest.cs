using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request
{
	public interface ISaveStandardResultRequest : IGameResults
	{
		Walkover Walkover { get; }
		Handicap Handicap { get; }		
	}
}