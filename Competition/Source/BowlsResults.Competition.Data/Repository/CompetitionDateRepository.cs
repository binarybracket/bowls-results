using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionDateRepository : IdentityRepository<CompetitionDate, int>, ICompetitionDateRepository
	{
		public CompetitionDateRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<CompetitionDate>> GetByCompetition(int competitionID)
		{
			return this.SessionQuery()
				.Where(x => x.Competition.ID == competitionID || x.ID == -1)
				.ToListAsync();
		}
	}
}