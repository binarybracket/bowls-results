using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Repository
{
	public interface IAssociationRepository : IIdentityRepository<Association, int>
	{
		Task<Association> GetWithContacts(int id);
	}
}