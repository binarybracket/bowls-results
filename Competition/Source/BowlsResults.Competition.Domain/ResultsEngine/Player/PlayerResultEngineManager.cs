using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public sealed class PlayerResultEngineManager : IPlayerResultEngineManager
	{
		private readonly ILogger _logger;
		private readonly IServiceProvider _serviceProvider;
		private readonly ILoggerFactory _loggerFactory;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionRepository _competitionRepository;
		private readonly IPlayerFixtureRepository _playerFixtureRepository;

		public PlayerResultEngineManager(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, ICompetitionRepository competitionRepository, IPlayerFixtureRepository playerFixtureRepository)
		{
			this._logger = loggerFactory.CreateLogger<PlayerResultEngine>();
			this._serviceProvider = serviceProvider;
			this._loggerFactory = loggerFactory;
			this._unitOfWork = unitOfWork;
			this._competitionRepository = competitionRepository;
			this._playerFixtureRepository = playerFixtureRepository;
		}

		public async Task<IPlayerResultEngine> GetEngine(IResultsEngineRequest request)
		{
			await this._competitionRepository.GetForUpdate(request.CompetitionID);
			Entities.Competition competition = await this._competitionRepository.GetWithStages(request.CompetitionID);
			CompetitionStage stage = this.GetStage(competition, request.CompetitionStageLoadMode, request.CompetitionStageValue);
			PlayerFixture fixture = await this._playerFixtureRepository.GetFull(request.FixtureID);

			IPlayerFixtureModel playerFixtureModel = this._serviceProvider.GetService<IPlayerFixtureModel>();
			IPlayerResultEngineContext context = this._serviceProvider.GetService<IPlayerResultEngineContext>();
			context.Initialise(competition, stage, fixture.CompetitionRound.CompetitionEvent, fixture.CompetitionRound, playerFixtureModel);
			
			var engine = this._serviceProvider.GetService<IPlayerResultEngine>();
			engine.SetContext(context);
			playerFixtureModel.Initialise(fixture, context);
			
			return engine;
		}

		private CompetitionStage GetStage(Entities.Competition competition, CompetitionStageLoadModes loadMode,
			int? optionsCompetitionStageValue)
		{
			switch (loadMode)
			{
				case CompetitionStageLoadModes.ByID:
					return competition.GetStageByID(optionsCompetitionStageValue.Value);
					break;
				case CompetitionStageLoadModes.BySequence:
					return competition.GetStageBySequence(optionsCompetitionStageValue.Value);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loadMode), loadMode, null);
			}
		}
	}
}