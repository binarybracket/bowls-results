using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.SaveStandardResult;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.UpdatePendingFixture;
using Microsoft.Extensions.DependencyInjection;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public static class Bootstrap
	{
		public static void Wire(IServiceCollection services)
		{
			// COMMON
			services.AddTransient<IProcessorExecutor, ProcessorExecutor>();
			
			// ENGINE
			services.AddTransient<IPlayerResultEngineManager, PlayerResultEngineManager>();
			services.AddTransient<IPlayerResultEngine, PlayerResultEngine>();
			
			// Processor Factories
			services.AddTransient<ISaveStandardResultProcessorFactory, SaveStandardResultProcessorFactory>();
			services.AddTransient<IUpdatePendingFixtureProcessorFactory, UpdatePendingFixtureProcessorFactory>();
			
			// Processors
			services.AddTransient<IValidateMatchStatusProcessor, ValidateMatchStatusProcessor>();
			services.AddTransient<IParseGamesProcessor, ParseGamesProcessor>();
			services.AddTransient<IMatchCalculationProcessor, MatchCalculationProcessor>();
			services.AddTransient<IMatchWalkoverProcessor, MatchWalkoverProcessor>();
			services.AddTransient<IFixtureCalculationProcessor, FixtureCalculationProcessor>();
			services.AddTransient<IPendingFixtureProcessor, PendingFixtureProcessor>();
			services.AddTransient<IUpdatePendingFixtureProcessor, UpdatePendingFixtureProcessor>();
			services.AddTransient<IValidateGameResultsProcessor, ValidateGameResultsProcessor>();
			services.AddTransient<ICompetitionResultProcessor, CompetitionResultProcessor>();
			
			// Actions
			services.AddTransient<ISaveStandardPlayerResultAction, SaveStandardPlayerResultAction>();
			services.AddTransient<IUpdatePendingFixtureAction, UpdatePendingFixtureAction>();
			
			// Models
			services.AddTransient<IPlayerMatchModel, PlayerMatchModel>();
			services.AddTransient<IPlayerFixtureModel, PlayerFixtureModel>();
			
			// Context
			services.AddTransient<IPlayerResultEngineContext, PlayerResultEngineContext>();
		}
	}
}