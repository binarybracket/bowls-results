using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionRepository : IdentityRepository<Domain.Entities.Competition, int>, ICompetitionRepository
	{
		public CompetitionRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<Domain.Entities.Competition>> GetPendingPlayerCompetitions()
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.Fetch(x => x.RegistrationConfiguration)
				.Fetch(x => x.VenueClub)
				.Fetch(x => x.OrganisingClub)
				.Where(x => x.CompetitionScopeID == CompetitionScopes.Player && x.StartDate > DateTime.UtcNow.Date)
				.ToListAsync();
		}

		public Task<Domain.Entities.Competition> GetWithRegistrationConfiguration(int competitionID)
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.Fetch(x => x.RegistrationConfiguration)
				.Fetch(x => x.VenueClub)
				.Fetch(x => x.GameVariation)
				.SingleOrDefaultAsync(x => x.ID == competitionID);
		}
	}
}