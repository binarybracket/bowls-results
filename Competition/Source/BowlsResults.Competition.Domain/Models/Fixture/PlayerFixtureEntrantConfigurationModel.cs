using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Models.Fixture
{
	public class PlayerFixtureEntrantConfigurationModel
	{
		public enum PendingEntrantModes
		{
			Entrant,
			Fixture
		}
		
		public PendingEntrantModes Mode { get; set; }
		public int? EntrantID { get; set; }
		public short? FixtureID { get; set; }
		public ResultType? FixtureResultType { get; set; }
	}
}