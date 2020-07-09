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
		private readonly IValidateMatchNotProcessedProcessor _validateMatchNotProcessedProcessor;
		private readonly IParseGamesProcessor _parseGamesProcessor;

		public SaveStandardResultProcessorFactory(IValidateMatchNotProcessedProcessor validateMatchNotProcessedProcessor, IParseGamesProcessor parseGamesProcessor)
		{
			this._validateMatchNotProcessedProcessor = validateMatchNotProcessedProcessor;
			this._parseGamesProcessor = parseGamesProcessor;
		}
		
		public IList<IProcessor<IPlayerResultEngineContext, ISaveStandardResultRequest, ResultsEngineResponse>> Create(IPlayerResultEngineContext context, ISaveStandardResultRequest request)
		{
			var list = new List<IProcessor<IPlayerResultEngineContext, ISaveStandardResultRequest, ResultsEngineResponse>>();
			switch (context.CompetitionStage.CompetitionStageFormatID)
			{
				case CompetitionStageFormats.SingleKnockout:
				{
					list.Add(this._validateMatchNotProcessedProcessor);
					list.Add(this._parseGamesProcessor);
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