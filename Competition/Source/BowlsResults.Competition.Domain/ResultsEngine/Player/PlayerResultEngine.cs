using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Com.BinaryBracket.Core.Domain2;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public sealed class PlayerResultEngine : IPlayerResultEngine
	{
		private IPlayerResultEngineContext _context;
		private ILogger<PlayerResultEngine> _logger;
		private readonly ISaveStandardPlayerResultAction _saveStandardPlayerResultAction;

		public PlayerResultEngine(ILogger<PlayerResultEngine> logger, ISaveStandardPlayerResultAction saveStandardPlayerResultAction)
		{
			this._logger = logger;
			this._saveStandardPlayerResultAction = saveStandardPlayerResultAction;
		}

		public void SetContext(IPlayerResultEngineContext context)
		{
			if (this._context != null)
			{
				throw new InvalidOperationException("Context already set.");
			}

			this._context = context;
		}

		public ResultsEngineResponse SaveStandardResult(SaveStandardResultRequest request)
		{
			return this._saveStandardPlayerResultAction.SaveStandardResultRequest(this._context, request);
		}

		private void GuardCheckContext()
		{
			if (this._context == null)
			{
				throw new InvalidOperationException("Context has not been set.");
			}
		}
	}
}