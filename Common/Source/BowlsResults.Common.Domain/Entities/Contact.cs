using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Contact : IdentityEntity<int>
	{
		public virtual ContactTypes ContactTypeID { get; set; }
		public virtual string Forename { get; set; }
		public virtual string Surname { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string Telephone { get; set; }
	}
}