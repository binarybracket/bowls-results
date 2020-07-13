using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.SaveStandardResult
{
	public sealed class MatchWalkoverProcessor : IMatchWalkoverProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;

		public MatchWalkoverProcessor(ILogger<MatchCalculationProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
		}

		public Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, ISaveStandardResultRequest request, ResultsEngineResponse response)
		{
			return Task.FromResult(true);
		}

		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, ISaveStandardResultRequest request, ResultsEngineResponse response)
		{
			var matchModel = context.PlayerFixture.GetMatch(request.MatchID);
			matchModel.SetWalkover(request.Walkover);

			return Task.FromResult(ResultsEngineStatuses.Success);
		}
	}
}