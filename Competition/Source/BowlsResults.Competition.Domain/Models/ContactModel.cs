namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Models
{
	public sealed class ContactModel
	{
		public int ID { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public string EmailAddress { get; set; }
		public string Telephone { get; set; }
	}
}