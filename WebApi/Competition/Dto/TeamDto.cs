using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class TeamDto
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Suffix { get; set; }
		public Genders Gender { get; set; }
		public AgeGroups AgeGroup { get; set; }
		public ContactDto Captain { get; set; }
	}
}