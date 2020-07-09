using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Processor;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Processors.SaveStandardResult;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions
{
	public sealed class SaveStandardPlayerResultAction : ISaveStandardPlayerResultAction
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISaveStandardResultProcessorFactory _processorFactory;
		private readonly IProcessorExecutor _processorExecutor;

		public SaveStandardPlayerResultAction(ILogger<SaveStandardPlayerResultAction> logger, IUnitOfWork unitOfWork, ISaveStandardResultProcessorFactory processorFactory, IProcessorExecutor processorExecutor)
		{
			this._unitOfWork = unitOfWork;
			this._processorFactory = processorFactory;
			this._processorExecutor = processorExecutor;
		}
		
		public ResultsEngineResponse SaveStandardResultRequest(IPlayerResultEngineContext context, ISaveStandardResultRequest request)
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