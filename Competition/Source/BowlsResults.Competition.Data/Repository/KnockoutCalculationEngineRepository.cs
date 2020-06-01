using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public class KnockoutCalculationEngineRepository : IdentityRepository<KnockoutCalculationEngine, byte>, IKnockoutCalculationEngineRepository
	{
		public KnockoutCalculationEngineRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<KnockoutCalculationEngine> GetFull(KnockoutCalculationEngines engineID)
		{
			return this.Session
				.Query<KnockoutCalculationEngine>()
				.Fetch(x => x.MatchFormat)
				.ThenFetchMany(x => x.GameVariations).SingleAsync(x => x.ID == (byte) engineID);
		}
	}
}