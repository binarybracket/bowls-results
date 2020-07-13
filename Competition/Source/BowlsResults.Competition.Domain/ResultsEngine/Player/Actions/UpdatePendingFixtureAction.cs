using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.UpdatePendingFixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions
{
	public sealed class UpdatePendingFixtureAction : IUpdatePendingFixtureAction
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUpdatePendingFixtureProcessorFactory _processorFactory;
		private readonly IProcessorExecutor _processorExecutor;

		public UpdatePendingFixtureAction(ILogger<UpdatePendingFixtureAction> logger, IUnitOfWork unitOfWork, IUpdatePendingFixtureProcessorFactory processorFactory, IProcessorExecutor processorExecutor)
		{
			this._unitOfWork = unitOfWork;
			this._processorFactory = processorFactory;
			this._processorExecutor = processorExecutor;
		}
		
		public ResultsEngineResponse Execute(IPlayerResultEngineContext context, IUpdatePendingFixtureRequest request)
		{
			this._unitOfWork.GuardCheckInTransaction();
			
			var response = new ResultsEngineResponse();
			var processors = this._processorFactory.Create(context, request);
			var status = this._processorExecutor.Execute(context, request, response, processors);
			response.Status = status;

			return response;
		}
	}
}