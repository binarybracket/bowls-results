using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Repository
{
	public interface IContactRepository : IIdentityRepository<Contact, int>
	{
		Task<Contact> GetByContactTypeAndEmail(ContactTypes contactType, string emailAddress);
	}
}