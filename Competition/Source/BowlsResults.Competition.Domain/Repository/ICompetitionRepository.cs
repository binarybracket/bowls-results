using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Views;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionRepository : IIdentityRepository<Entities.Competition, int>
	{
		Task<Entities.Competition> GetWithStages(int competitionID);
		Task<List<Entities.Competition>> GetPendingPlayerCompetitions();

		Task<Entities.Competition> GetWithRegistrationConfiguration(int competitionID);

		Task<List<Entities.Competition>> GetPastPlayerCompetitions();
		Task<Entities.Competition> GetTop();
		Task<List<Entities.Competition>> GetClosedOnlineCompetitions(DateTime start, DateTime end);
		Task<List<CompetitionTeam>> GetCompetitionTeams(int competitionHeaderID, int season);
	}
}