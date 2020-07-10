using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public class CompetitionRoundRepository : IdentityRepository<CompetitionRound, short>, ICompetitionRoundRepository
	{
		public CompetitionRoundRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<CompetitionRound>> GetAll(short competitionEventID)
		{
			return this.Session.Query<CompetitionRound>()
				.Where(x => x.CompetitionEvent.ID == competitionEventID)
				.ToListAsync();
		}
	}
}