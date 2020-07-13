using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Entities
{
	public class Club : AuditableEntity<int>
	{
		public Club()
		{
		}

		public virtual int AssociationID { get; set; }
		public virtual string Name { get; set; }
		public virtual Pitch Pitch { get; set; }
		public virtual double? Longitude { get; set; }
		public virtual double? Latitude { get; set; }
		public virtual bool Active { get; set; }
		
		public virtual IList<ClubXContact> Contacts { get; set; }

		public virtual Contact GetContactByType(ContactTypes contactType)
		{
			return this.Contacts.Single(x => x.Contact.ContactTypeID == contactType).Contact;
		}
		
		public virtual void AddContact(Contact contact)
		{
			this.RemoveAll(contact);
			if (!this.Contacts.Any(x => x.Contact.ContactTypeID == contact.ContactTypeID && x.Contact.ID == contact.ID))
			{
				var data = new ClubXContact
				{
					Contact = contact,
					Club = this
				};
				this.Contacts.Add(data);
			}
		}

		private void RemoveAll(Contact contact)
		{
			var itemsToRemove = this.Contacts.Where(x => x.Contact.ContactTypeID == contact.ContactTypeID && x.Contact.ID != contact.ID).ToArray();
			foreach(var item in itemsToRemove)
			{
				this.Contacts.Remove(item);
			}
		}
	}
}