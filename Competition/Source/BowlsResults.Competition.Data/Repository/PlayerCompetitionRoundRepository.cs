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
	public class PlayerCompetitionRoundRepository : IdentityRepository<PlayerCompetitionRound, short>, IPlayerCompetitionRoundRepository
	{
		public PlayerCompetitionRoundRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<PlayerCompetitionRound>> GetAll(short competitionEventID)
		{
			return this.Session.Query<PlayerCompetitionRound>()
				.FetchMany(x => x.Fixtures)
				.Where(x => x.CompetitionEvent.ID == competitionEventID)
				.ToListAsync();
		}
	}
}