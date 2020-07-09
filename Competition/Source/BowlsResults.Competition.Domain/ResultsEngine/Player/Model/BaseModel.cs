using System;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public abstract class BaseModel<TData, TContext>
		where TContext : IResultsEngineContext
	{
		public TData Data { get; private set; }
		public TContext Context { get; private set; }

		public void Initialise(TData data, TContext context)
		{
			this.SetData(data);
			this.SetContext(context);
		}

		private void SetData(TData data)
		{
			if (this.Data != null)
			{
				throw new InvalidOperationException("Data has already been set.");
			}

			this.Data = data;
		}

		private void SetContext(TContext context)
		{
			if (this.Context != null)
			{
				throw new InvalidOperationException("Context has already been set.");
			}

			this.Context = context;
		}
	}
}