using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.UpdatePendingFixture
{
	public sealed class UpdatePendingFixtureProcessor : IUpdatePendingFixtureProcessor
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;

		public UpdatePendingFixtureProcessor(ILogger<MatchCalculationProcessor> logger, IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._logger = logger;
		}

		public Task<bool> IsSatisfiedBy(IPlayerResultEngineContext context, IUpdatePendingFixtureRequest request, ResultsEngineResponse response)
		{
			return Task.FromResult<bool>(context.PlayerFixture.IsPending());
		}

		public Task<ResultsEngineStatuses> Process(IPlayerResultEngineContext context, IUpdatePendingFixtureRequest request, ResultsEngineResponse response)
		{
			context.PlayerFixture.UpdatePendingFixture(request.CompletedFixture);
			return Task.FromResult(ResultsEngineStatuses.Success);
		}
	}
}