using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class PitchRepository : IdentityRepository<Pitch, int>, IPitchRepository
	{
		public PitchRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}