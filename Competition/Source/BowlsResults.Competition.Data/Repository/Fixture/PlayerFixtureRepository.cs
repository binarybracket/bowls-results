using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Fixture
{
	public class PlayerFixtureRepository : IdentityRepository<PlayerFixture, short>, IPlayerFixtureRepository
	{
		public PlayerFixtureRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<PlayerFixture> GetFull(short id)
		{
			return this.Session.Query<PlayerFixture>()
				.Fetch(x => x.CompetitionRound)
				.ThenFetch(x => x.CompetitionEvent)
				.ThenFetch(x => x.CompetitionStage)
				.FetchMany(x => x.Matches)
				.ThenFetchMany(f => f.Games)
				.ThenFetch(f => f.Game)
				.ThenFetchMany(f => f.Players)
				.ThenFetch(f => f.Player)
				.SingleOrDefaultAsync(x => x.ID == id);
		}

		public Task<List<PlayerFixture>> GetAll(short competitionRoundID)
		{
			return this.Session.Query<PlayerFixture>()
				.Where(x => x.CompetitionRound.ID == competitionRoundID)
				.ToListAsync();
		}

		public Task<List<PlayerFixture>> GetAllFullByCompetition(int competitionID)
		{
			return this.Session.Query<PlayerFixture>()
				.Fetch(x => x.CompetitionRound)
				.ThenFetch(x => x.CompetitionEvent)
				.ThenFetch(x => x.CompetitionStage)
				.FetchMany(x => x.Matches)
				.ThenFetchMany(f => f.Games)
				.ThenFetch(f => f.Game)
				.ThenFetchMany(f => f.Players)
				.ThenFetch(f => f.Player)
				.Where(x => x.CompetitionRound.Competition.ID == competitionID)
				.ToListAsync();
		}

		public Task<List<PlayerFixture>> GetPendingFixtures(short relatedFixtureID)
		{
			return this.Session.Query<PlayerFixture>().Where(
				x =>
					x.PendingPlayer1Fixture.ID == relatedFixtureID ||
					x.PendingPlayer2Fixture.ID == relatedFixtureID).ToListAsync();
		}
	}
}