using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface IParseGamesProcessor : IProcessor<IPlayerResultEngineContext, IGameResults, ResultsEngineResponse>
	{
	}

	public sealed class ParseGamesProcessor : IParseGamesProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;


		public ParseGamesProcessor(ILogger<ParseGamesProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._logger = logger;
			this._unitOfWork = unitOfWork;
		}

		public Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			bool result = !context.PlayerFixture.IsMatchProcessed(request.MatchID);
			return Task.FromResult(result);
		}

		public async Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			if (request.GameResults != null && request.GameResults.Count > 0)
			{
				var matchModel = context.PlayerFixture.GetMatch(request.MatchID);

				var competition = context.Competition;
				foreach (var gameResult in request.GameResults)
				{
					if (matchModel.GameExists(gameResult.MatchFormatXGameVariationID))
					{
						await matchModel.UpdateGame(gameResult);
					}
					else
					{
						await matchModel.AddGame(gameResult);
					}
				}
			}

			return ResultsEngineStatuses.Success;
		}
	}
}