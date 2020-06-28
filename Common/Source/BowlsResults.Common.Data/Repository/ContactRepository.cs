using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public class ContactRepository : IdentityRepository<Contact, int>, IContactRepository
	{
		public ContactRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<Contact> GetByContactTypeAndEmail(ContactTypes contactType, string emailAddress)
		{
			return this.SessionQuery()
				.SingleOrDefaultAsync(x => x.ContactTypeID == contactType && x.EmailAddress == emailAddress);
		}
	}
}