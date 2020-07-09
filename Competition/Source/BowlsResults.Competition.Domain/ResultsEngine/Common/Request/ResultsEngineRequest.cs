namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request
{
	/// <summary>
	/// Results Engine Request
	/// </summary>
	public abstract class ResultsEngineRequest : IResultsEngineRequest
	{
		public abstract class BaseBuilder<TRequest, TBuilder>
			where TRequest : ResultsEngineRequest
			where TBuilder : BaseBuilder<TRequest, TBuilder>
		{
			protected readonly TRequest Instance;

			protected BaseBuilder(TRequest instance)
			{
				this.Instance = instance;
			}

			public TRequest Build()
			{
				return this.Instance;
			}

			public TBuilder WithCompetitionID(int competitionID)
			{
				this.Instance.CompetitionID = competitionID;
				return (TBuilder) this;
			}

			public TBuilder WithCompetitionStageID(int id)
			{
				this.Instance.CompetitionStageLoadMode = CompetitionStageLoadModes.ByID;
				this.Instance.CompetitionStageValue = id;
				return (TBuilder) this;
			}

			public TBuilder WithCompetitionStageSequence(int id)
			{
				this.Instance.CompetitionStageLoadMode = CompetitionStageLoadModes.BySequence;
				this.Instance.CompetitionStageValue = id;
				return (TBuilder) this;
			}

			public TBuilder WithFixtureID(short fixtureID)
			{
				this.Instance.FixtureID = fixtureID;
				return (TBuilder) this;
			}
			
			public TBuilder WithMatchID(int matchID)
			{
				this.Instance.MatchID = matchID;
				return (TBuilder) this;
			}
		}

		public int CompetitionID { get; private set; }
		public CompetitionStageLoadModes CompetitionStageLoadMode { get; private set; }
		public int CompetitionStageValue { get; private set; }
		public short FixtureID { get; private set; }
		public int MatchID { get; private set; }
	}
}