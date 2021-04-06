using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.Core.Domain2;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface IValidateGameResultsProcessor : IProcessor<IPlayerResultEngineContext, IGameResults, ResultsEngineResponse>
	{
	}

	public sealed class ValidateGameResultsProcessor : IValidateGameResultsProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;


		public ValidateGameResultsProcessor(ILogger<ValidateGameResultsProcessor> logger, IUnitOfWork unitOfWork)
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
			bool ok = true;

			if (request.GameResults != null && request.GameResults.Count > 0)
			{
				var matchModel = context.PlayerFixture.GetMatch(request.MatchID);

				var competition = context.Competition;

				HashSet<int> homePlayers = new HashSet<int>(); 
				HashSet<int> awayPlayers = new HashSet<int>();

				homePlayers.UnionWith(request.GameResults.SelectMany(x => x.HomePlayers));
				awayPlayers.UnionWith(request.GameResults.SelectMany(x => x.AwayPlayers));
				
				//foreach (var gameResult in request.GameResults)
				{
					if (!homePlayers.SequenceEqual(matchModel.Data.Home.GetPlayerIDs()))
					{
						response.ValidationResult.Errors.Add(new ValidationFailure(nameof(homePlayers),
							"Home players are different to the entrant players on the match."));
						ok = false;
					}

					if (!awayPlayers.SequenceEqual(matchModel.Data.Away.GetPlayerIDs()))
					{
						response.ValidationResult.Errors.Add(
							new ValidationFailure(nameof(awayPlayers), "Away players are different to the entrant players on the match"));
						ok = false;
					}
				}
			}

			if (ok)
			{
				return ResultsEngineStatuses.Success;
			}

			return ResultsEngineStatuses.UnknownError;
		}
	}
}