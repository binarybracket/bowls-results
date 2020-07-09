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
	public interface IValidateMatchNotProcessedProcessor : IProcessor<IPlayerResultEngineContext, IResultsEngineRequest, ResultsEngineResponse>
	{
	}

	public class ValidateMatchNotProcessedProcessor : IValidateMatchNotProcessedProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;
		
		public ValidateMatchNotProcessedProcessor(ILogger<ValidateMatchNotProcessedProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
		}

		public bool IsSatisfiedBy(IPlayerResultEngineContext context, IResultsEngineRequest request, ResultsEngineResponse response)
		{
			return context.PlayerFixture.IsMatchProcessed(request.MatchID);
		}
		
		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IResultsEngineRequest request, ResultsEngineResponse response)
		{
			throw new InvalidResultsEngineOperationException("Match has already been processed.  To make changes you will need to revert this match first.");
		}
	}
}