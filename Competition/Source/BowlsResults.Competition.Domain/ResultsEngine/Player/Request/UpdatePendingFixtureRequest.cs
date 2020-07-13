using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Request;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Request
{
	public sealed class UpdatePendingFixtureRequest : ResultsEngineRequest, IUpdatePendingFixtureRequest
	{
		public sealed class Builder : BaseBuilder<UpdatePendingFixtureRequest, Builder>
		{
			internal Builder()
				: base(new UpdatePendingFixtureRequest())
			{
			}

			public Builder WithCompletedFixture(PlayerFixture fixture)
			{
				this.Instance.CompletedFixture = fixture;
				return this;
			}
		}
		private UpdatePendingFixtureRequest()
		{
		}

		public static Builder New()
		{
			return new Builder();
		}
		public PlayerFixture CompletedFixture { get; private set; }
	}
}