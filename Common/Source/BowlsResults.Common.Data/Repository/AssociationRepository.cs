using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class AssociationRepository : IdentityRepository<Association, int>, IAssociationRepository
	{
		public AssociationRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<Association> GetWithContacts(int id)
		{
			return this.SessionQuery()
				.FetchMany(x => x.Contacts)
				.ThenFetch(x => x.Contact)
				.SingleOrDefaultAsync(x => x.ID == id);
		}
	}
}