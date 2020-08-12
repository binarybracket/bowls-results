using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionResultRepository : IdentityRepository<Domain.Entities.CompetitionResult, int>, ICompetitionResultRepository
	{
		public CompetitionResultRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<PlayerCompetitionResult>> GetPlayerCompetitionResults(int season)
		{
			// return this.Session.QueryOver<PlayerCompetitionResult>()
			// 	.Fetch(x => x.Competition).Eager
			// 	.Fetch(x => x.Fixture).Eager
			// 	//.Fetch(x => x.Fixture.Matches).Eager
			// 	// .Fetch(x => x.Fixture.Matches[0].Games).Eager
			// 	// .Fetch(x => x.Fixture.Matches[0].Games.First().Game).Eager
			// 	// .Fetch(x => x.Fixture.Matches[0].Games.First().Game.Players).Eager
			// 	// .Fetch(x => x.Fixture.Matches[0].Games.First().Game.Players[0].Player).Eager
			// 	.Where(x => x.SeasonID == season)
			// 	.TransformUsing(Transformers.DistinctRootEntity)
			// 	.ListAsync();


			return this.Session.Query<PlayerCompetitionResult>()
				.Fetch(x => x.Competition)
				.Fetch(x => x.Fixture)
				.ThenFetchMany(x => x.Matches)
				.ThenFetchMany(f => f.Games)
				.ThenFetch(f => f.Game)
				.ThenFetchMany(f => f.Players)
				.ThenFetch(f => f.Player)
				.Where(x => x.SeasonID == season)
				.OrderByDescending(x => x.Competition.StartDate)
				.ToListAsync();
		}
	}
}