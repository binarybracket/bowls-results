using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace BowlsResults.WebApi.PlayerCompetition
{
	public abstract class BaseFixtureDto
	{
		public int ID { get; set; }
		public int Legs { get; set; }
		public FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
		public FixtureStatuses FixtureStatusID { get; set; }
		public string Reference { get; set; }
	}
}