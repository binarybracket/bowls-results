using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public abstract class BaseResultFixtureDto
	{
		public int ID { get; set; }
		public int Legs { get; set; }
		public FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
		public FixtureStatuses FixtureStatusID { get; set; }
	}
}