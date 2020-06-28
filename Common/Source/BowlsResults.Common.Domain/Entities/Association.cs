using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Association : AuditableEntity<int>
	{
		public virtual string Name { get; set; }
		public virtual string ShortName { get; set; }

		public virtual IList<AssociationXContact> Contacts { get; set; }

		public virtual void AddContact(Contact contact)
		{
			this.RemoveAll(contact);
			if (!this.Contacts.Any(x => x.Contact.ContactTypeID == contact.ContactTypeID && x.Contact.ID == contact.ID))
			{
				var data = new AssociationXContact()
				{
					Contact = contact,
					Association = this
				};
				this.Contacts.Add(data);
			}
		}

		private void RemoveAll(Contact contact)
		{
			var itemsToRemove = this.Contacts.Where(x => x.Contact.ContactTypeID == contact.ContactTypeID && x.Contact.ID != contact.ID).ToArray();
			foreach (var item in itemsToRemove)
			{
				this.Contacts.Remove(item);
			}
		}
	}
}