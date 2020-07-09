using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common
{
	public interface IMatchCalculationProcessor : IProcessor<IPlayerResultEngineContext, IGameResults, ResultsEngineResponse>
	{
	}
	public sealed class MatchCalculationProcessor : IMatchCalculationProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;

		public MatchCalculationProcessor(ILogger<MatchCalculationProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
		}

		public bool IsSatisfiedBy(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			return true;
		}
		
		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IGameResults request, ResultsEngineResponse response)
		{
			context.PlayerFixture.CalculateMatches();

			return Task.FromResult<ResultsEngineStatuses>(ResultsEngineStatuses.Success);
		}
	}
}