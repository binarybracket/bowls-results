using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Dto
{
	public class ContactDto
	{
		public int ID { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public string DisplayName { get; set; }
		public string EmailAddress { get; set; }
		public string Telephone { get; set; }
		public ContactTypes ContactTypeID { get; set; }
	}
}