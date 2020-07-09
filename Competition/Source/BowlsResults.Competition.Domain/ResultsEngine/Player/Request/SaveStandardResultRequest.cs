using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request
{
	/// <summary>
	/// Save Standard Result Request
	/// </summary>
	public sealed class SaveStandardResultRequest : ResultsEngineRequest, ISaveStandardResultRequest
	{
		public sealed class Builder : BaseBuilder<SaveStandardResultRequest, Builder>
		{
			internal Builder()
				: base(new SaveStandardResultRequest())
			{
			}

			public Builder WithGameResults(List<GameResult> gameResults)
			{
				this.Instance.GameResults = gameResults;
				return this;
			}

			public Builder WithWalkover(Walkover walkover)
			{
				this.Instance.Walkover = walkover;
				return this;
			}

			public Builder NotPersistGames()
			{
				this.Instance.PersistGames = false;
				return this;
			}

			public Builder NotUpdateCareerStatistics()
			{
				this.Instance.UpdateCareerStatistics = false;
				return this;
			}

			public Builder NotUpdatePlayerCompetitionStatistics()
			{
				this.Instance.UpdatePlayerCompetitionStatistics = false;
				return this;
			}
		}

		private SaveStandardResultRequest()
		{
			this.PersistGames = true;
			this.UpdateCareerStatistics = true;
			this.UpdatePlayerCompetitionStatistics = true;
		}

		public static Builder New()
		{
			return new Builder();
		}


		public List<GameResult> GameResults { get; private set; }
		public Walkover Walkover { get; private set; }
		public bool PersistGames { get; private set; }
		public bool UpdateCareerStatistics { get; private set; }
		public bool UpdatePlayerCompetitionStatistics { get; private set; }
	}
}