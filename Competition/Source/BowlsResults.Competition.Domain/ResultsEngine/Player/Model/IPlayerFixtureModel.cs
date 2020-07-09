using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Player.Model
{
	public interface IPlayerFixtureModel : IBaseModel<PlayerFixture, IPlayerResultEngineContext>
	{
		IPlayerMatchModel GetMatch(int id);

		bool IsMatchProcessed(int id);
	}
}