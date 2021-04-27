using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Views;
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

		public Task<Domain.Entities.Competition> GetWithStages(int competitionID)
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.Fetch(x => x.Stages)
				.Fetch(x => x.VenueClub)
				.ThenFetch(x => x.Pitch)
				.Fetch(x => x.GameVariation)
				.SingleOrDefaultAsync(x => x.ID == competitionID);
		}

		public Task<List<Domain.Entities.Competition>> GetPendingPlayerCompetitions()
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.Fetch(x => x.RegistrationConfiguration)
				.Fetch(x => x.VenueClub)
				.ThenFetch(x => x.Pitch)
				.Fetch(x => x.OrganisingClub)
				.FetchMany(x => x.Dates)
				.Where(x => x.CompetitionScopeID == CompetitionScopes.Player && x.StartDate > DateTime.UtcNow)
				.OrderBy(x => x.StartDate)
				.ToListAsync();
		}

		public Task<Domain.Entities.Competition> GetWithRegistrationConfiguration(int competitionID)
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.FetchMany(x => x.Stages)
				.Fetch(x => x.RegistrationConfiguration)
				.Fetch(x => x.VenueClub)
				.ThenFetch(x => x.Pitch)
				.Fetch(x => x.GameVariation)
				.SingleOrDefaultAsync(x => x.ID == competitionID);
		}

		public Task<List<Domain.Entities.Competition>> GetPastPlayerCompetitions()
		{
			return this.Session.Query<Domain.Entities.Competition>()
				.Fetch(x => x.RegistrationConfiguration)
				.Fetch(x => x.VenueClub)
				.ThenFetch(x => x.Pitch)
				.Fetch(x => x.OrganisingClub)
				.Fetch(x => x.Stages)
				.Where(x => x.CompetitionScopeID == CompetitionScopes.Player && x.StartDate < DateTime.UtcNow.Date)
				.ToListAsync();
		}

		public Task<Domain.Entities.Competition> GetTop()
		{
			return this.SessionQuery().FirstOrDefaultAsync();
		}

		public Task<List<Domain.Entities.Competition>> GetClosedOnlineCompetitions(DateTime start, DateTime end)
		{
			var registrationSubQuery = this.Session.Query<CompetitionRegistrationConfiguration>()
				.Where(x => x.CompetitionRegistrationModeID == CompetitionRegistrationModes.Online && (x.CloseDate > start && x.CloseDate <= end))
				.Select(x => x.Competition.ID);

			return this.SessionQuery()
				.Where(x => registrationSubQuery.Contains(x.ID))
				.ToListAsync();
		}

		public Task<List<CompetitionTeam>> GetCompetitionTeams(int competitionHeaderID, int season)
		{
			return this.Session.Query<CompetitionTeam>()
				.Where(x => x.CompetitionHeaderID == competitionHeaderID && x.SeasonID == season)
				.OrderBy(x=>x.Team.Name)
				.ToListAsync();
		}
	}
}