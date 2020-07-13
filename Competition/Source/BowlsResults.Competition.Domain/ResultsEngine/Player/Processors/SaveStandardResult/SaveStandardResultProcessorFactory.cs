using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.SaveStandardResult
{
	public class SaveStandardResultProcessorFactory : ISaveStandardResultProcessorFactory
	{
		private readonly IValidateMatchStatusProcessor _validateMatchStatusProcessor;
		private readonly IParseGamesProcessor _parseGamesProcessor;
		private readonly IMatchCalculationProcessor _matchCalculationProcessor;
		private readonly IMatchWalkoverProcessor _walkoverProcessor;
		private readonly IFixtureCalculationProcessor _fixtureCalculationProcessor;
		private readonly IPendingFixtureProcessor _pendingFixtureProcessor;

		public SaveStandardResultProcessorFactory(IValidateMatchStatusProcessor validateMatchStatusProcessor, IParseGamesProcessor parseGamesProcessor,
			IMatchCalculationProcessor matchCalculationProcessor, IMatchWalkoverProcessor walkoverProcessor, IFixtureCalculationProcessor fixtureCalculationProcessor,
			IPendingFixtureProcessor pendingFixtureProcessor)
		{
			this._validateMatchStatusProcessor = validateMatchStatusProcessor;
			this._parseGamesProcessor = parseGamesProcessor;
			this._matchCalculationProcessor = matchCalculationProcessor;
			this._walkoverProcessor = walkoverProcessor;
			this._fixtureCalculationProcessor = fixtureCalculationProcessor;
			this._pendingFixtureProcessor = pendingFixtureProcessor;
		}

		public IList<IProcessor<IPlayerResultEngineContext, ISaveStandardResultRequest, ResultsEngineResponse>> Create(IPlayerResultEngineContext context,
			ISaveStandardResultRequest request)
		{
			var list = new List<IProcessor<IPlayerResultEngineContext, ISaveStandardResultRequest, ResultsEngineResponse>>();
			switch (context.CompetitionStage.CompetitionStageFormatID)
			{
				case CompetitionStageFormats.SingleKnockout:
				{
					list.Add(this._validateMatchStatusProcessor);
					list.Add(this._parseGamesProcessor);
					list.Add(this._matchCalculationProcessor);
					list.Add(this._walkoverProcessor);
					list.Add(this._fixtureCalculationProcessor);
					list.Add(this._pendingFixtureProcessor);
				}
					break;
				case CompetitionStageFormats.SingleLeague:
				case CompetitionStageFormats.Groups:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return list;
		}
	}
}