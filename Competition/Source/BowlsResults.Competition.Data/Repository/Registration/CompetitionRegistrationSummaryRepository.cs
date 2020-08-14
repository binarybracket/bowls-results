using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public class CompetitionRegistrationSummaryRepository : IdentityRepository<CompetitionRegistrationSummary, int>, ICompetitionRegistrationSummaryRepository
	{
		public CompetitionRegistrationSummaryRepository(IRegistrationSessionProvider provider) : base(provider)
		{
		}

		public Task<CompetitionRegistrationSummary> GetByCompetition(int competitionID)
		{
			return this.SessionQuery()
				.SingleOrDefaultAsync(x => x.CompetitionID == competitionID);
		}
	}
}