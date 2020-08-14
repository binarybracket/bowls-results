using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlsResults.WebApi;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public sealed class CompetitionRegistrationRepository : IdentityRepository<CompetitionRegistration, int>, ICompetitionRegistrationRepository
	{
		public CompetitionRegistrationRepository(IRegistrationSessionProvider provider) : base(provider)
		{
		}

		public Task<List<CompetitionRegistration>> GetAll(int competitionID)
		{
			return this.SessionQuery()
				.Where(x => x.CompetitionID == competitionID)
				.FetchMany(x => x.Entrants)
				.ThenFetchMany(x => x.Players)
				.ToListAsync();
		}
	}
}