namespace BowlsResults.WebApi.Common.Dto
{
	public sealed class PlayerDto
	{
		public int ID { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public string DisplayName { get; set; }
	}
}