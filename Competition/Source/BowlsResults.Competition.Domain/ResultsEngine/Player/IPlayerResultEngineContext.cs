using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public interface IPlayerResultEngineContext : IResultsEngineContext
	{
		Entities.Competition Competition { get; }
		CompetitionStage CompetitionStage { get; }
		CompetitionEvent CompetitionEvent { get; }
		CompetitionRound CompetitionRound { get; }
		IPlayerFixtureModel PlayerFixture { get; }
	}

	public class PlayerResultEngineContext : IPlayerResultEngineContext
	{
		public static PlayerResultEngineContext Create(Entities.Competition competition, CompetitionStage stage, CompetitionEvent competitionEvent, CompetitionRound round,
			IPlayerFixtureModel playerFixture)
		{
			var context = new PlayerResultEngineContext();
			context.Competition = competition;
			context.CompetitionStage = stage;
			context.CompetitionEvent = competitionEvent;
			context.CompetitionRound = round;
			context.PlayerFixture = playerFixture;
			
			return context;
		}
		
		public Entities.Competition Competition { get; private set;}
		public CompetitionStage CompetitionStage { get; private set;}
		public CompetitionEvent CompetitionEvent { get; private set;}
		public CompetitionRound CompetitionRound { get; private set;}
		public IPlayerFixtureModel PlayerFixture { get; private set;}
	}
}