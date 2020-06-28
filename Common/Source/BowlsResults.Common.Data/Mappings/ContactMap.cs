using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class ContactMap : IdentityEntityMap<Contact, int>
	{
		public ContactMap()
		{
			this.Table("Contact");
			this.LazyLoad();
			
			this.Map(x => x.ContactTypeID).Column("ContactTypeID").Not.Nullable();
			this.Map(x => x.Forename).Column("Forename").Not.Nullable();
			this.Map(x => x.Surname).Column("Surname").Not.Nullable();
			this.Map(x => x.EmailAddress).Column("EmailAddress");
			this.Map(x => x.Telephone).Column("Telephone");
		}
	}
}