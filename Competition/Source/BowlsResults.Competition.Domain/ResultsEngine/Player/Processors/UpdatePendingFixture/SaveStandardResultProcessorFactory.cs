using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.UpdatePendingFixture
{
	public class UpdatePendingFixtureProcessorFactory : IUpdatePendingFixtureProcessorFactory
	{
		private readonly IUpdatePendingFixtureProcessor _updatePendingFixtureProcessor;

		public UpdatePendingFixtureProcessorFactory(IUpdatePendingFixtureProcessor updatePendingFixtureProcessor)
		{
			this._updatePendingFixtureProcessor = updatePendingFixtureProcessor;
		}

		public IList<IProcessor<IPlayerResultEngineContext, IUpdatePendingFixtureRequest, ResultsEngineResponse>> Create(IPlayerResultEngineContext context,
			IUpdatePendingFixtureRequest request)
		{
			var list = new List<IProcessor<IPlayerResultEngineContext, IUpdatePendingFixtureRequest, ResultsEngineResponse>>();
			switch (context.CompetitionStage.CompetitionStageFormatID)
			{
				case CompetitionStageFormats.SingleKnockout:
				{
					list.Add(this._updatePendingFixtureProcessor);
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