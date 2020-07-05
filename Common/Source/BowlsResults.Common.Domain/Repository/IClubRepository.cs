using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Repository
{
	public interface IClubRepository : IIdentityRepository<Club, int>
	{
		Task<Club> GetWithContacts(int id);
		Task<List<Club>> GetAllActiveByAssociation(int associationId);
	}
}