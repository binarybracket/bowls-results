using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace BowlsResults.WebApi
{
	public class TestAppUnitOfWork : UnitOfWork
	{
		private readonly ISessionProvider _sessionProvider;
		private readonly bool _managingSessionProvider;

		public TestAppUnitOfWork(ILoggerFactory logger, ISessionProvider sessionProvider) : base(logger)
		{
			this._sessionProvider = sessionProvider;
			this._managingSessionProvider = false;
		}

		protected override ITransaction BeginDatabaseTransaction()
		{
			return this._sessionProvider.Session.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
		}

		protected override void InnerDispose(bool disposing)
		{
			base.InnerDispose(disposing);
			if (disposing && !this.Disposed)
			{
				if (this._managingSessionProvider)
				{
					this._sessionProvider?.Dispose();
				}
			}
		}
	}
}