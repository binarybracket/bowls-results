using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Exceptions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface IValidateMatchStatusProcessor : IProcessor<IPlayerResultEngineContext, IResultsEngineRequest, ResultsEngineResponse>
	{
	}

	public class ValidateMatchStatusProcessor : IValidateMatchStatusProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;

		public ValidateMatchStatusProcessor(ILogger<ValidateMatchStatusProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
		}

		public Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, IResultsEngineRequest request, ResultsEngineResponse response)
		{
			return Task.FromResult(true);
		}

		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IResultsEngineRequest request, ResultsEngineResponse response)
		{
			// TODO - use validation result
			if (context.PlayerFixture.IsMatchPending(request.MatchID))
			{
				throw new InvalidResultsEngineOperationException("Match is pending.  To make changes you will need to confirm the match details first.");
			}

			if (context.PlayerFixture.IsMatchProcessed(request.MatchID))
			{
				throw new InvalidResultsEngineOperationException("Match has already been processed.  To make changes you will need to revert this match first.");
			}

			return Task.FromResult(ResultsEngineStatuses.Success);
		}
	}
}