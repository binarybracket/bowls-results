using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class GameVariationDto
	{
		public int ID { get; set; }
		public GameFormats GameFormatID { get; set; }
		public Genders GenderID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}