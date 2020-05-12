using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionHeaderRepository : IdentityRepository<CompetitionHeader, int>, ICompetitionHeaderRepository
	{
		public CompetitionHeaderRepository(ISessionProvider provider) : base(provider)
		{
		}


		public Task<CompetitionHeader> Get2(int id)
		{
			//Thread.Sleep(5000);
			return this.Session.GetAsync<CompetitionHeader>(id);
		}
	}
}