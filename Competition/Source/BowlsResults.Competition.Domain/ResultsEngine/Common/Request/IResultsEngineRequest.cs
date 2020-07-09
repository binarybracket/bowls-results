namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request
{
	public interface IResultsEngineRequest
	{
		int CompetitionID { get; }
		CompetitionStageLoadModes CompetitionStageLoadMode { get; }
		int CompetitionStageValue { get; }
		short FixtureID { get; }
		int MatchID { get; }
	}
}