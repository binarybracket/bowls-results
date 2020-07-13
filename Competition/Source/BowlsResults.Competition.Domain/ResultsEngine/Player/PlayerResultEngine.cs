using System;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Actions;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request;
using Microsoft.Extensions.Logging;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player
{
	public sealed class PlayerResultEngine : IPlayerResultEngine
	{
		private IPlayerResultEngineContext _context;
		private ILogger<PlayerResultEngine> _logger;
		private readonly ISaveStandardPlayerResultAction _saveStandardPlayerResultAction;
		private readonly IUpdatePendingFixtureAction _updatePendingFixtureAction;

		public PlayerResultEngine(ILogger<PlayerResultEngine> logger, ISaveStandardPlayerResultAction saveStandardPlayerResultAction, IUpdatePendingFixtureAction updatePendingFixtureAction)
		{
			this._logger = logger;
			this._saveStandardPlayerResultAction = saveStandardPlayerResultAction;
			this._updatePendingFixtureAction = updatePendingFixtureAction;
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

		public ResultsEngineResponse UpdatePendingFixture(IUpdatePendingFixtureRequest request)
		{
			return this._updatePendingFixtureAction.Execute(this._context, request);
		}

		public Task Save()
		{
			return this._context.Save();
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