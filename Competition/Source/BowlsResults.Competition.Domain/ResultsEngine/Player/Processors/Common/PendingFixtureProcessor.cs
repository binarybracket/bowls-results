using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface IPendingFixtureProcessor : IProcessor<IPlayerResultEngineContext, IGameResults, ResultsEngineResponse>
	{
	}

	public sealed class PendingFixtureProcessor : IPendingFixtureProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPlayerFixtureRepository _playerFixtureRepository;
		private readonly IPlayerResultEngineManager _playerResultEngineManager;
		private List<PlayerFixture> _pendingFixtures;

		public PendingFixtureProcessor(ILogger<PendingFixtureProcessor> logger, IUnitOfWork unitOfWork, IPlayerFixtureRepository playerFixtureRepository, IPlayerResultEngineManager playerResultEngineManager)
		{
			this._unitOfWork = unitOfWork;
			this._playerFixtureRepository = playerFixtureRepository;
			this._playerResultEngineManager = playerResultEngineManager;
			this._logger = logger;
		}

		public async Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			if (context.PlayerFixture.IsComplete())
			{
				this._pendingFixtures = await this._playerFixtureRepository.GetPendingFixtures(context.PlayerFixture.Data.ID);
				return this._pendingFixtures.Count > 0;
			}

			return false;
		}

		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			var status = ResultsEngineStatuses.Success;
			
			foreach (PlayerFixture pendingFixture in this._pendingFixtures)
			{
				var updateRequest = UpdatePendingFixtureRequest.New()
					.WithCompetitionID(pendingFixture.CompetitionID)
					.WithFixtureID(pendingFixture.ID)
					.WithCompetitionStageID(pendingFixture.CompetitionRound.CompetitionEvent.CompetitionStage.ID)
					.WithCompletedFixture(context.PlayerFixture.Data)
					.Build();
				IPlayerResultEngine engine = this._playerResultEngineManager.GetEngine(updateRequest).GetAwaiter().GetResult();
				var updateResponse = engine.UpdatePendingFixture(updateRequest);
				status = updateResponse.Status;
				if (status != ResultsEngineStatuses.Success)
				{
					break;
				}
			}

			return Task.FromResult(status);
		}
	}
}