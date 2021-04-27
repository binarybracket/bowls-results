using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class ClubRepository : IdentityRepository<Club, int>, IClubRepository
	{
		public ClubRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<Club> GetWithContacts(int id)
		{
			return this.SessionQuery()
				.FetchMany(x => x.Contacts)
				.ThenFetch(x => x.Contact)
				.FetchMany(x => x.Contacts)
				.ThenFetch(x => x.Team)
				.ThenFetch(x => x.Club)
				.SingleOrDefaultAsync(x => x.ID == id);
		}

		public Task<List<Club>> GetAllActiveByAssociation(int associationId)
		{
			return this.SessionQuery()
				.FetchMany(x => x.Contacts)
				.ThenFetch(x => x.Contact)
				.Where(x => x.AssociationID == associationId && x.Active == true)
				.OrderBy(x => x.Name)
				.ToListAsync();
		}
	}
}