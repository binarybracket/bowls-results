namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class ClubDto
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public bool Active { get; set; }
	}
}