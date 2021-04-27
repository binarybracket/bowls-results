using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Commands.Contact
{
	public sealed class CreateContactCommand : ICommand<DefaultIdentityCommandResponse>
	{
		public int? AssociationID { get; set; }
		public int? ClubID { get; set; }
		public int? TeamID { get; set; }
		public ContactTypes ContactTypeID { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public string EmailAddress { get; set; }
		public string Telephone { get; set; }
	}
}