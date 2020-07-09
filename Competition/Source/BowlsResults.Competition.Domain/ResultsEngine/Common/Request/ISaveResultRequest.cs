using System.Collections.Generic;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request
{
	public interface IGameResults : IResultsEngineRequest
	{
		List<GameResult> GameResults { get; }
		bool PersistGames { get; }
		bool UpdateCareerStatistics { get; }
		bool UpdatePlayerCompetitionStatistics { get; }
	}
}