using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface ICompetitionResultProcessor : IProcessor<IPlayerResultEngineContext, IGameResults, ResultsEngineResponse>
	{
		
	}
	public class CompetitionResultProcessor : ICompetitionResultProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompetitionResultRepository _competitionResultRepository;

		public CompetitionResultProcessor(ILogger<CompetitionResultProcessor> logger, IUnitOfWork unitOfWork, ICompetitionResultRepository competitionResultRepository)
		{
			this._unitOfWork = unitOfWork;
			this._competitionResultRepository = competitionResultRepository;
			this._logger = logger;
		}

		public Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			return Task.FromResult(context.PlayerFixture.IsComplete() && context.PlayerFixture.Data.CompetitionRound.CompetitionRoundTypeID == CompetitionRoundTypes.Final);
		}

		public async Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			var data = new PlayerCompetitionResult();
			data.Fixture = context.PlayerFixture.Data;
			data.Competition = context.Competition;
			data.SeasonID = context.Competition.Season.ID;

			await this._competitionResultRepository.Save(data);

			return ResultsEngineStatuses.Success;
		}
	}
}